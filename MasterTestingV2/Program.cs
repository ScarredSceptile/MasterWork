using CSAMS.Models;
using MasterTestingV2;
using MasterTestingV2.APIModels;

namespace MasterTesting
{
    internal class Program
    {
        private const int MinReviewCount = 5;
        private const int MaxReviewCount = 10;
        private const int MinAssiCount = 1;
        private const int MaxAssiCount = 3;
        private const int MinUserCount = 5;
        private const int MaxUserCount = 10;

        static void Main(string[] args)
        {
            var connection = @"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Data\csams.sqlite";
            var ApiConnection = @"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Data\reviews.sqlite";
            var AIConnection = @"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Data\reviewsAI.sqlite";
            var ExpertConnection = @"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Data\reviewsExpert.sqlite";
            var db = new AppDbContext(connection);
            var apiDb = new APIDbContext(ApiConnection);
            var AIDb = new APIDbContext(AIConnection);
            var expertDb = new APIDbContext(ExpertConnection);

            //Used once to find the language of the reviews
            //Utility.DetectLanguage(db);

            //Used once to filter reviews for reviewing
            //Utility.DBToAPIDB(db, apiDb);

            //Used once to split the db into smaller parts
            //Utility.SplitApiDB(db, apiDb);

            //Utility.SetReviewScore(AIDb, @"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Data\AI");
            //Utility.DBtoCSV(expertDb, "ExpertTest");

            //Console.WriteLine("Starting training");
            //AI.CreateModel(expertDb.TextReviews.Where(n => n.Course == 6).Select(n => new AIModel(n.Review, (float)n.Score)).ToArray(), @"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Data\AITrain.zip");
            //Console.WriteLine("Finished training");
            var preds = AI.PredictScores(expertDb.TextReviews.Where(n => n.Course == 7).Select(n => new AIModel { Review = n.Review}).ToArray(), @"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Data\ExpertTrain.zip");

            string t = "";
            /*
            foreach (var pred in preds)
            {
                t += pred.Score + "\n";
                Console.WriteLine($"review: \"{pred.Review}\", score: {pred.Score}");
            }

            t += "Average: " + preds.Average(n => n.Score);
            t += "\nMin: " + preds.Min(n => n.Score);
            t += "\nMax: " + preds.Max(n => n.Score);
            t += "\nMedian: " + preds.OrderBy(n => n.Score).ToArray()[preds.Length / 2].Score;
            */
            /*
            
            string t = "";
            var scores = expertDb.TextReviews.Where(n => n.Course == 6).ToArray();

            for (int i = 0; i <scores.Count(); i++)
            {
                t += Math.Abs(scores[i].Score - Random.Shared.NextSingle()) + "\n";
                t += scores[i].Score + "\n";
            }

            File.WriteAllText(@"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Data\expertActualScores.txt", t);
            */

            /*
            var grouping = new Grouping();
            grouping.CourseOneGroups(apiDb);
            grouping.CourseTwoGroups(apiDb);
            grouping.CourseThreeGroups(apiDb);
            grouping.CourseFourGroups(apiDb);
            grouping.CourseFiveGroups(apiDb);
            grouping.CourseSixGroups(apiDb);
            grouping.CourseSevenGroups(apiDb);
            grouping.CourseEightGroups(apiDb);
            grouping.CourseNineGroups(apiDb);
            grouping.CourseTenGroups(apiDb);
            grouping.CourseElevenGroups(apiDb);
            grouping.CourseTwelveGroups(apiDb);
            grouping.CourseThirteenGroups(apiDb);
            */
        }

        private void FindStopwords(AppDbContext db)
        {
            var allwords = Words.Allwords(db);
            var wordsByReview = Words.WordsByReview(db);
            var wordsByAssi = Words.WordsByAssignment(db);
            var wordsByUser = Words.WordsByUser(db);

            var reviewWords = wordsByReview.Where(n => n.Value >= MinReviewCount && n.Value <= MaxReviewCount).Select(n => n.Key).ToArray();
            var assiWords = wordsByAssi.Where(n => n.Value >= MinAssiCount && n.Value <= MaxAssiCount).Select(n => n.Key).ToArray();
            var userWords = wordsByUser.Where(n => n.Value >= MinUserCount && n.Value <= MaxUserCount).Select(n => n.Key).ToArray();

            Console.WriteLine(reviewWords.Length);
            Console.WriteLine(assiWords.Length);
            Console.WriteLine(userWords.Length);

            var allowed = allwords.Where(n => reviewWords.Any(x => x == n)).ToArray();
            allowed = allowed.Where(n => assiWords.Any(x => x == n)).ToArray();
            allowed = allowed.Where(n => userWords.Any(x => x == n)).ToArray();

            var stopwords = allwords.Where(n => allowed.Any(x => x == n) == false).ToArray();

            Utility.WriteList(@$"StopList\Allowed_Review{MinReviewCount}-{MaxReviewCount}_Assi{MinAssiCount}-{MaxAssiCount}_User{MinUserCount}-{MaxUserCount}", allowed);
            Utility.WriteList(@$"StopList\Stopwords_Review{MinReviewCount}-{MaxReviewCount}_Assi{MinAssiCount}-{MaxAssiCount}_User{MinUserCount}-{MaxUserCount}", stopwords);
        }
    }
}