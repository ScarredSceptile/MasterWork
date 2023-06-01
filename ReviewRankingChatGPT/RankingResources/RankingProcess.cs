using ReviewRanking.Models;
using System.Diagnostics;

namespace ReviewRanking.RankingResources
{
    public class RankingProcess
    {
        private string DbConnection;
        private Ranking _ranking;
        private static Random rng = new Random();
        private int _course;
        private int clicks = 0;

        public string choice = null;

        public RankingProcess(string dbConnection, Ranking form, int course)
        {
            DbConnection = dbConnection;
            _ranking = form;
            _course = course;
        }

        public async void CompleteRanking()
        {
            var watch = new Stopwatch();
            watch.Start();

            using (var _context = new AppDbContext(DbConnection))
            {
                var reviews = _context.TextReviews.Where(n => n.Course == _course).ToArray();
                _ranking.StartLoading();
                await Task.Delay(1);
                reviews = await ImportOldComparisons(reviews);
                await Task.Delay(1);
                _ranking.StopLoading();
                var groups = reviews.GroupBy(n => n.Grouping).ToArray();
                await RankByGroup(groups, reviews);
                await RankBetweenGroup(groups, reviews);
                await FinishRanking(reviews);
                await ScoreRankings(reviews);
            }

            watch.Stop();
            _ranking.SetTime(watch.Elapsed.ToString("g"));
        }

        public static TextReview[] AddOldComparisons(TextReview[] reviews, List<RankingInfo> rankings)
        {
            var rev = reviews.ToList();
            foreach (var review in rev)
            {
                var comparisons = rankings.Where(c => c.ReviewID == review.Id);
                foreach (var comparison in comparisons)
                {
                    if (comparison.Comparison == "Worse")
                        review.BetterReviews.Add(comparison.OtherReviewID);
                    else if (comparison.Comparison == "Equal")
                        review.EqualReviews.Add(comparison.OtherReviewID);
                    else if (comparison.Comparison == "Better")
                        review.WorseReviews.Add(comparison.OtherReviewID);
                }
            }
            return rev.ToArray();
        }

        public async Task<TextReview[]> ImportOldComparisons(TextReview[] reviews)
        {
            var folder = Path.GetDirectoryName(DbConnection) + "/Data";
            if (Directory.Exists(folder) == false)
                Directory.CreateDirectory(folder);

            var rev = reviews.ToList();
            Parallel.ForEach(rev, review =>
            {
                if (File.Exists(folder + $"/{review.Id}.txt") == false)
                    File.Create(folder + $"/{review.Id}.txt").Close();
                else
                {
                    var comparisons = File.ReadAllLines(folder + $"/{review.Id}.txt");
                    foreach (var comparison in comparisons)
                    {
                        var comp = comparison.Split(" ");
                        if (comp[0] == "Worse")
                            review.BetterReviews.Add(int.Parse(comp[1]));
                        else if (comp[0] == "Equal")
                            review.EqualReviews.Add(int.Parse(comp[1]));
                        else if (comp[0] == "Better")
                            review.WorseReviews.Add(int.Parse(comp[1]));
                    }
                }
            });
            return rev.ToArray();
        }

        private async Task RankByGroup(IGrouping<int, TextReview>[] groups, TextReview[] revs)
        {
            _ranking.SetCompareMethod("RankByGroup");
            _ranking.SetGroupTotal(groups.Length);
            int i = 0;
            foreach (var group in groups)
            {
                var count = group.Count();
                _ranking.SetTotal(count * (count-1));
                _ranking.SetGroupProgress(++i);

                while (CheckGroupRankingComplete(group, count) == false)
                {
                    var min = group.Min(c => c.Comparisons());
                    var mins = group.Where(n => n.Comparisons() == min).ToArray();
                    var r = rng.Next(mins.Length);
                    _ranking.SetTest(group.Sum(c => c.Comparisons()));
                    var left = mins[r];

                    //Remove the left review and any review that already has a comparison with the left review
                    mins = mins.Where(n => n.Id != left.Id && n.IsCompared(left.Id) == false).ToArray();
                    TextReview right;
                    if (mins.Length > 0)
                    {
                        r = rng.Next(mins.Length);
                        right = mins[r];
                    }
                    else
                    {
                        var reviews = group.Where(n => n.Id != left.Id && n.IsCompared(left.Id) == false).ToArray();
                        r = rng.Next(reviews.Length);
                        right = reviews[r];
                    }

                    _ranking.SetClicks(clicks.ToString());
                    await RankingPair(left, right, revs);
                    clicks++;
                }
            }
            _ranking.HideGroupProgress();
        }

        private async Task RankBetweenGroup(IGrouping<int, TextReview>[] groups, TextReview[] revs)
        {
            _ranking.SetCompareMethod("RankBetweenGroup");
            List<TextReview> significantReviews = new();

            foreach (var group in groups)
                significantReviews.AddRange(GetSignificantReviews(group));

            _ranking.SetTotal(significantReviews.Count * (significantReviews.Count - 1));

            while (true)
            {
                var reviews = CheckSignificantReviewComplete(significantReviews);
                if (reviews.All(n => n.Count >= reviews.Length - 1)) break;

                var min = reviews.Min(n => n.Count);
                var mins = reviews.Where(n => n.Count == min).ToArray();
                var r = rng.Next(mins.Length);
                _ranking.SetTest(reviews.Sum(n => n.Count));
                var left = mins[r].review;

                mins = mins.Where(n => n.review.Id != left.Id && n.review.IsCompared(left.Id) == false).ToArray();
                TextReview right;
                if (mins.Length > 0)
                {
                    r = rng.Next(mins.Length);
                    right = mins[r].review;
                }
                else
                {
                    var remaining = reviews.Where(n => n.review.Id != left.Id && n.review.IsCompared(left.Id) == false).ToArray();
                    r = rng.Next(remaining.Length);
                    right = remaining[r].review;
                }
                _ranking.SetClicks(clicks.ToString());
                await RankingPair(left, right, revs);
                clicks++;
            }
        }

        private async Task FinishRanking(TextReview[] reviews)
        {
            _ranking.SetCompareMethod("FinishRanking");
            var totalRankings = reviews.Length * (reviews.Length-1);

            while (true)
            {
                var curRankings = reviews.Sum(n => n.Comparisons());
                _ranking.SetTotal(totalRankings);
                _ranking.SetTest(curRankings);

                if (curRankings >= totalRankings) break;
                var min = reviews.Min(n => n.Comparisons());

                var mins = reviews.Where(n => n.Comparisons() == min).ToArray();
                var r = rng.Next(mins.Length);
                var left = mins[r];

                mins = mins.Where(n => n.Id != left.Id && n.IsCompared(left.Id) == false).ToArray();
                TextReview right;
                if (mins.Length > 0)
                {
                    r = rng.Next(mins.Length);
                    right = mins[r];
                }
                else
                {
                    var remaining = reviews.Where(n => n.Id != left.Id && n.IsCompared(left.Id) == false).ToArray();
                    r = rng.Next(remaining.Length);
                    right = remaining[r];
                }
                _ranking.SetClicks(clicks.ToString());
                await RankingPair(left, right, reviews);
                clicks++;
            }
        }

        private async Task ScoreRankings(TextReview[] reviews)
        {

        }

        //TODO: add test? In case it stops functioning?
        private bool CheckGroupRankingComplete(IGrouping<int, TextReview>? group, int count)
        {
            //Check if the grouping has completed the ranking. Each item will have at least count - 1 rankings
            var complete = true;
            foreach (var item in group)
            {
                if (item.Comparisons() < count - 1)
                {
                    return false;
                }
            }
            return true;
        }

        public static List<TextReview> GetSignificantReviews(IGrouping<int, TextReview>? group)
        {
            List<TextReview> reviews = new();
            var g = group.OrderBy(n => n.BetterReviews.Count()).ToArray();
            reviews.Add(g[0]);
            reviews.Add(g[^1]);
            reviews.Add(g[g.Length / 2]);
            return reviews;
        }

        public static List<int> GetSignificantIDs(IGrouping<int, TextReview>? group, RankingInfo[] ranks)
        {
            List<int> IDs = new List<int>();

            var t = ranks.Where(n => group.Any(c => c.Id == n.ReviewID) && group.Any(c => c.Id == n.OtherReviewID)).GroupBy(n => n.ReviewID).ToList();
            var g = t.OrderBy(n => n.Where(n => n.Comparison == Values.Better).Count()).Select(n => n.Key).ToList();
            IDs.Add(g[0]);
            IDs.Add(g[^1]);
            IDs.Add(g[g.Count / 2]);
            return IDs;
        }


        /// <summary>
        /// Returns a touple of reviews and the amount of other significant reviews it has been compared to
        /// </summary>
        /// <param name="reviews"></param>
        /// <returns></returns>
        public static (TextReview review, int Count)[] CheckSignificantReviewComplete(List<TextReview> reviews)
        {
            return reviews.Select(n => (n, reviews.Where(c => n.IsCompared(c.Id)).Count())).ToArray();
        }

        public static (int ID, int Count)[] CheckSignificantIDComplete(List<int> IDs, RankingInfo[] ranks)
        {
            return IDs.Select(n => (n, ranks.Where(c => c.ReviewID == n && IDs.Contains(c.OtherReviewID)).Count())).ToArray();
        }

        private async Task RankingPair(TextReview left, TextReview right, TextReview[] reviews)
        {
            _ranking.SetReviews(left, right);
            await Task.Delay(1);

            /*
            var c = new[] { Values.Left, Values.Right, Values.Equal };
            var r = rng.Next(c.Length);
            choice = c[r];
            */

            var t = _ranking.CallChatGPT();

            while (choice == null)
            {
                await Task.Delay(1);
            }
            List<RankingInfo> newData;

            if (choice == Values.Equal)
            {
                newData = NewEqualComparison(left, right, reviews);
            }
            else if (choice == Values.Left)
            {
                newData = NewDifferentComparison(left, right, reviews);
            }
            else if (choice == Values.Right)
            {
                newData = NewDifferentComparison(right, left, reviews);
            }
            else
            {
                newData = new();
                MessageBox.Show("Something went wrong, choice does not exist");
            }
            /*
            //TODO: Change to file-based for speed
            using (var _context = new AppDbContext(DbConnection))
            {
                _context.RankingInfo.AddRange(newData);
                _context.SaveChangesAsync();
            }*/

            var files = newData.GroupBy(n => n.ReviewID).ToList();
            var folder = Path.GetDirectoryName(DbConnection) + "/Data";

            Parallel.ForEach(files, file =>
            {
                var output = file.Select(n => $"{n.Comparison} {n.OtherReviewID}").ToList();
                File.AppendAllLines(folder + $"/{file.Key}.txt", output);
            });
            /*
            foreach (var file in files)
            {
                var output = file.Select(n => $"{n.Comparison} {n.OtherReviewID}").ToList();
                File.AppendAllLines(folder + $"/{file.Key}.txt", output);
            }*/

            choice = null;
        }

        /*
         * NOT DECIDED IF THIS IS WORTH THE EFFORT
        private List<RankingInfo> FileDifferentComparison(TextReview better, TextReview worse)
        {
            List<RankingInfo> newComparisons = new()
            {
                new RankingInfo { ReviewID = better.Id, OtherReviewID = worse.Id, Comparison = Values.Better },
                new RankingInfo { ReviewID = worse.Id, OtherReviewID = better.Id, Comparison = Values.Worse }
            };

            var folder = Path.GetDirectoryName(DbConnection) + "/Data";
            var betterReviews = File.ReadAllLines(folder + $"{better.Id}.txt").Where(n => n.StartsWith(Values.Better) == false).Select(n => (n.Split(' ')[0], int.Parse(n.Split(' ')[1]))).ToArray();
            var worseReviews = File.ReadAllLines(folder + $"{worse.Id}.txt").Where(n => n.StartsWith(Values.Worse) == false).Select(n => (n.Split(' ')[0], int.Parse(n.Split(' ')[1]))).ToArray();
        }*/

        public static List<RankingInfo> NewDifferentComparison(TextReview better, TextReview worse, TextReview[] reviews)
        {
            List<RankingInfo> newComparisons = new()
            {
                new RankingInfo { ReviewID = better.Id, OtherReviewID = worse.Id, Comparison = Values.Better },
                new RankingInfo { ReviewID = worse.Id, OtherReviewID = better.Id, Comparison = Values.Worse }
            };

            better.WorseReviews.Add(worse.Id);
            worse.BetterReviews.Add(better.Id);

            var betterReviews = better.EqualReviews.ToList();
            betterReviews.AddRange(better.BetterReviews);
            betterReviews.Add(better.Id);
            var worseReviews = worse.EqualReviews.ToList();
            worseReviews.AddRange(worse.WorseReviews);
            worseReviews.Add(worse.Id);

            foreach (var review in betterReviews)
            {
                var betRev = reviews.Where(n => n.Id == review).First();
                foreach (var worseReview in worseReviews)
                {
                    if (betRev.IsCompared(worseReview) || review == worseReview)
                        continue;
                    var rev = reviews.Where(n => n.Id == worseReview).First();
                    betRev.WorseReviews.Add(rev.Id);
                    newComparisons.Add(new RankingInfo { ReviewID = betRev.Id, OtherReviewID = rev.Id, Comparison = Values.Better });
                    rev.WorseReviews.Add(betRev.Id);
                    newComparisons.Add(new RankingInfo { ReviewID = rev.Id, OtherReviewID = betRev.Id, Comparison = Values.Worse });
                }
            }
            return newComparisons;
        }

        public static List<RankingInfo> DifferentComparison(int id, int other, string comparison, RankingInfo[] ranks)
        {
            var r = comparison == Values.Better ? Values.Worse : Values.Better;
            List<RankingInfo> list = new()
            {
                new RankingInfo { ReviewID = id, OtherReviewID = other, Comparison = comparison },
                new RankingInfo { ReviewID = other, OtherReviewID = id, Comparison = r }
            };
            var relevantIDs = ranks.Where(n => n.ReviewID == id && n.Comparison != comparison).Select(n => n.OtherReviewID).ToList();
            var otherIDs = ranks.Where(n => n.ReviewID == other && (n.Comparison == comparison || n.Comparison == Values.Equal)).Select(n => n.OtherReviewID).ToList();
            relevantIDs.Add(id);
            otherIDs.Add(other);

            foreach (var relevantID in relevantIDs)
            {
                foreach (var otherID in otherIDs)
                {
                    if (ranks.Where(n => n.ReviewID == relevantID && n.OtherReviewID == otherID).Count() == 0 && relevantID != otherID)
                    {
                        if (list.Where(n => n.ReviewID == relevantID && n.OtherReviewID == otherID).Count() == 0)
                        {
                            list.Add(new RankingInfo { ReviewID = relevantID, OtherReviewID = otherID, Comparison = comparison });
                            list.Add(new RankingInfo { ReviewID = otherID, OtherReviewID = relevantID, Comparison = r });
                        }
                    }
                }
            }
            return list;
        }

        // This function also finds any comparisons within left or right that aren't somehow made and fixes them? No clue how
        public static List<RankingInfo> NewEqualComparison(TextReview left, TextReview right, TextReview[] reviews)
        {
            List<RankingInfo> newComparisons = new()
            {
                new RankingInfo { ReviewID = left.Id, OtherReviewID = right.Id, Comparison = Values.Equal },
                new RankingInfo { ReviewID = right.Id, OtherReviewID = left.Id, Comparison = Values.Equal }
            };

            left.EqualReviews.Add(right.Id);
            right.EqualReviews.Add(left.Id);

            var worseChars = right.EqualReviews.ToList();
            worseChars.AddRange(right.WorseReviews);
            worseChars.Add(right.Id);

            foreach (var cha in left.BetterReviews)
            {
                var leftRev = reviews.Where(n => n.Id == cha).First();
                foreach (var chb in worseChars)
                {
                    if (leftRev.IsCompared(chb) || cha == chb)
                        continue;
                    var rightRev = reviews.Where(n => n.Id == chb).First();
                    leftRev.WorseReviews.Add(rightRev.Id);
                    newComparisons.Add(new RankingInfo { ReviewID = leftRev.Id, OtherReviewID = rightRev.Id, Comparison = Values.Better });
                    rightRev.WorseReviews.Add(leftRev.Id);
                    newComparisons.Add(new RankingInfo { ReviewID = rightRev.Id, OtherReviewID = leftRev.Id, Comparison = Values.Worse });
                }
            }

            var lEquals = left.EqualReviews.ToList();
            lEquals.Add(left.Id);
            var rEquals = right.EqualReviews.ToList();
            rEquals.Add(right.Id);

            foreach (var cha in lEquals)
            {
                var leftRev = reviews.Where(n => n.Id == cha).First();


                foreach (var chb in right.BetterReviews)
                {
                    if (leftRev.IsCompared(chb) || cha == chb)
                        continue;
                    var rightRev = reviews.Where(n => n.Id == chb).First();
                    leftRev.WorseReviews.Add(rightRev.Id);
                    newComparisons.Add(new RankingInfo { ReviewID = leftRev.Id, OtherReviewID = rightRev.Id, Comparison = Values.Worse });
                    rightRev.WorseReviews.Add(leftRev.Id);
                    newComparisons.Add(new RankingInfo { ReviewID = rightRev.Id, OtherReviewID = leftRev.Id, Comparison = Values.Better });
                }
                foreach (var chb in rEquals)
                {
                    if (leftRev.IsCompared(chb) || cha == chb)
                        continue;
                    var rightRev = reviews.Where(n => n.Id == chb).First();
                    leftRev.WorseReviews.Add(rightRev.Id);
                    newComparisons.Add(new RankingInfo { ReviewID = leftRev.Id, OtherReviewID = rightRev.Id, Comparison = Values.Equal });
                    rightRev.WorseReviews.Add(leftRev.Id);
                    newComparisons.Add(new RankingInfo { ReviewID = rightRev.Id, OtherReviewID = leftRev.Id, Comparison = Values.Equal });
                }
                foreach (var chb in right.WorseReviews)
                {
                    if (leftRev.IsCompared(chb) || cha == chb)
                        continue;
                    var rightRev = reviews.Where(n => n.Id == chb).First();
                    leftRev.WorseReviews.Add(rightRev.Id);
                    newComparisons.Add(new RankingInfo { ReviewID = leftRev.Id, OtherReviewID = rightRev.Id, Comparison = Values.Better });
                    rightRev.WorseReviews.Add(leftRev.Id);
                    newComparisons.Add(new RankingInfo { ReviewID = rightRev.Id, OtherReviewID = leftRev.Id, Comparison = Values.Worse });
                }
            }

            var betterChar = right.EqualReviews.ToList();
            betterChar.AddRange(right.BetterReviews);
            betterChar.Add(right.Id);

            foreach (var cha in left.WorseReviews)
            {
                var leftRev = reviews.Where(n => n.Id == cha).First();

                foreach (var chb in betterChar)
                {
                    if (leftRev.IsCompared(chb) || cha == chb)
                        continue;
                    var rightRev = reviews.Where(n => n.Id == chb).First();
                    leftRev.WorseReviews.Add(rightRev.Id);
                    newComparisons.Add(new RankingInfo { ReviewID = leftRev.Id, OtherReviewID = rightRev.Id, Comparison = Values.Worse });
                    rightRev.WorseReviews.Add(leftRev.Id);
                    newComparisons.Add(new RankingInfo { ReviewID = rightRev.Id, OtherReviewID = leftRev.Id, Comparison = Values.Better });
                }
            }
            return newComparisons;
        }

        public static List<RankingInfo> EqualComparison(int id, int other, RankingInfo[] ranks)
        {
            List<RankingInfo> list = new()
            {
                new RankingInfo { ReviewID = id, OtherReviewID = other, Comparison = Values.Equal },
                new RankingInfo { ReviewID = other, OtherReviewID = id, Comparison = Values.Equal }
            };

            var relBetterIDs = ranks.Where(n => n.ReviewID == id && n.Comparison == Values.Worse).Select(n => n.OtherReviewID).ToArray();
            var relEqualIDs = ranks.Where(n => n.ReviewID == id && n.Comparison == Values.Equal).Select(n => n.OtherReviewID).ToList();
            var relWorseIDs = ranks.Where(n => n.ReviewID == id && n.Comparison == Values.Better).Select(n => n.OtherReviewID).ToArray();

            relEqualIDs.Add(id);

            var otherBetterIDs = ranks.Where(n => n.ReviewID == other && n.Comparison == Values.Worse).Select(n => n.OtherReviewID).ToArray();
            var otherEqualIDs = ranks.Where(n => n.ReviewID == other && n.Comparison == Values.Equal).Select(n => n.OtherReviewID).ToList();
            var otherWorseIDs = ranks.Where(n => n.ReviewID == other && n.Comparison == Values.Better).Select(n => n.OtherReviewID).ToArray();

            otherEqualIDs.Add(other);

            list.AddRange(EqualBetterComparison(relBetterIDs, otherEqualIDs.ToArray(), otherWorseIDs, list, ranks));
            list.AddRange(EqualEqualComparison(relEqualIDs.ToArray(), otherBetterIDs, otherEqualIDs.ToArray(), otherWorseIDs, list, ranks));
            list.AddRange(EqualWorseComparison(relWorseIDs, otherBetterIDs, otherEqualIDs.ToArray(), list, ranks));

            return list;
        }

        private static List<RankingInfo> EqualBetterComparison(int[]? relBetterIDs, int[]? otherEqualIDs, int[]? otherWorseIDs, List<RankingInfo> otherList, RankingInfo[] ranks)
        {
            List<RankingInfo> list = new();
            foreach (var relBetterID in relBetterIDs)
            {
                foreach (var otherEqualID in otherEqualIDs)
                {
                    if (ranks.Where(n => n.ReviewID == relBetterID && n.OtherReviewID == otherEqualID).Count() == 0 && relBetterID != otherEqualID)
                    {
                        if (otherList.Where(n => n.ReviewID == relBetterID && n.OtherReviewID == otherEqualID).Count() == 0)
                        {
                            list.Add(new RankingInfo { ReviewID = relBetterID, OtherReviewID = otherEqualID, Comparison = Values.Better });
                            list.Add(new RankingInfo { ReviewID = otherEqualID, OtherReviewID = relBetterID, Comparison = Values.Worse });
                        }
                    }
                }
                foreach (var otherWorseID in otherWorseIDs)
                {
                    if (ranks.Where(n => n.ReviewID == relBetterID && n.OtherReviewID == otherWorseID).Count() == 0 && relBetterID != otherWorseID)
                    {
                        if (otherList.Where(n => n.ReviewID == relBetterID && n.OtherReviewID == otherWorseID).Count() == 0)
                        {
                            list.Add(new RankingInfo { ReviewID = relBetterID, OtherReviewID = otherWorseID, Comparison = Values.Better });
                            list.Add(new RankingInfo { ReviewID = otherWorseID, OtherReviewID = relBetterID, Comparison = Values.Worse });
                        }
                    }
                }
            }
            return list;
        }

        private static List<RankingInfo> EqualEqualComparison(int[]? relEqualIDs, int[]? otherBetterIDs, int[]? otherEqualIDs, int[]? otherWorseIDs, List<RankingInfo> otherList, RankingInfo[] ranks)
        {
            List<RankingInfo> list = new();
            foreach (var relEqualID in relEqualIDs)
            {
                foreach (var otherBetterID in otherBetterIDs)
                {
                    if (ranks.Where(n => n.ReviewID == relEqualID && n.OtherReviewID == otherBetterID).Count() == 0 && relEqualID != otherBetterID)
                    {
                        if (otherList.Where(n => n.ReviewID == relEqualID && n.OtherReviewID == otherBetterID).Count() == 0)
                        {
                            list.Add(new RankingInfo { ReviewID = relEqualID, OtherReviewID = otherBetterID, Comparison = Values.Worse });
                            list.Add(new RankingInfo { ReviewID = otherBetterID, OtherReviewID = relEqualID, Comparison = Values.Better });
                        }
                    }
                }
                foreach (var otherEqualID in otherEqualIDs)
                {
                    if (ranks.Where(n => n.ReviewID == relEqualID && n.OtherReviewID == otherEqualID).Count() == 0 && relEqualID != otherEqualID)
                    {
                        if (otherList.Where(n => n.ReviewID == relEqualID && n.OtherReviewID == otherEqualID).Count() == 0)
                        {
                            list.Add(new RankingInfo { ReviewID = relEqualID, OtherReviewID = otherEqualID, Comparison = Values.Equal });
                            list.Add(new RankingInfo { ReviewID = otherEqualID, OtherReviewID = relEqualID, Comparison = Values.Equal });
                        }
                    }
                }
                foreach (var otherWorseID in otherWorseIDs)
                {
                    if (ranks.Where(n => n.ReviewID == relEqualID && n.OtherReviewID == otherWorseID).Count() == 0 && relEqualID != otherWorseID)
                    {
                        if (otherList.Where(n => n.ReviewID == relEqualID && n.OtherReviewID == otherWorseID).Count() == 0)
                        {
                            list.Add(new RankingInfo { ReviewID = relEqualID, OtherReviewID = otherWorseID, Comparison = Values.Better });
                            list.Add(new RankingInfo { ReviewID = otherWorseID, OtherReviewID = relEqualID, Comparison = Values.Worse });
                        }
                    }
                }
            }
            return list;
        }

        private static List<RankingInfo> EqualWorseComparison(int[]? relWorseIDs, int[]? otherBetterIDs, int[]? otherEqualIDs, List<RankingInfo> otherList, RankingInfo[] ranks)
        {
            List<RankingInfo> list = new();
            foreach (var relWorseID in relWorseIDs)
            {
                foreach (var otherBetterID in otherBetterIDs)
                {
                    if (ranks.Where(n => n.ReviewID == relWorseID && n.OtherReviewID == otherBetterID).Count() == 0 && relWorseID != otherBetterID)
                    {
                        if (otherList.Where(n => n.ReviewID == relWorseID && n.OtherReviewID == otherBetterID).Count() == 0)
                        {
                            list.Add(new RankingInfo { ReviewID = relWorseID, OtherReviewID = otherBetterID, Comparison = Values.Worse });
                            list.Add(new RankingInfo { ReviewID = otherBetterID, OtherReviewID = relWorseID, Comparison = Values.Better });
                        }
                    }
                }
                foreach (var otherEqualID in otherEqualIDs)
                {
                    if (ranks.Where(n => n.ReviewID == relWorseID && n.OtherReviewID == otherEqualID).Count() == 0 && relWorseID != otherEqualID)
                    {
                        if (otherList.Where(n => n.ReviewID == relWorseID && n.OtherReviewID == otherEqualID).Count() == 0)
                        {
                            list.Add(new RankingInfo { ReviewID = relWorseID, OtherReviewID = otherEqualID, Comparison = Values.Worse });
                            list.Add(new RankingInfo { ReviewID = otherEqualID, OtherReviewID = relWorseID, Comparison = Values.Better });
                        }
                    }
                }
            }
            return list;
        }
    }
}
