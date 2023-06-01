using CSAMS.Models;
using CsvHelper;
using LanguageDetection;
using MasterTestingV2.APIModels;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MasterTestingV2
{
    public class Utility
    {
        private static Random rng = new();

        public static Dictionary<string, int> GetWordAmount(string[] texts)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            for (int i = 0; i < texts.Length; i++)
                texts[i] = rgx.Replace(texts[i], "");
            foreach (string text in texts)
            {
                var words = text.Split(' ');
                foreach (string word in words)
                {
                    if (result.ContainsKey(word.ToLower()))
                        result[word.ToLower()]++;
                    else
                        result.Add(word.ToLower(), 1);
                }
            }
            return result;
        }

        public static void DBToAPIDB(AppDbContext context, APIDbContext ApiDb)
        {
            var reviewByCourse = context.UserReviews.GroupBy(n => n.Assignment.CourseID).ToList();
            foreach (var course in reviewByCourse)
            {

                var reviews = course.Where(r => r.Comment != null && r.Language == "en").Select(r => r.Comment).ToList();
                reviews.AddRange(course.Where(r => r.Type == "textarea" && r.Answer != null && r.Language == "en").Select(r => r.Answer).ToList());

                var removeDupes = reviews.GroupBy(n => Filter(n)).ToList();

                foreach (var review in removeDupes)
                {
                    ApiDb.TextReviews.Add(new TextReviews(review.First(), course.Key, 0, 0));
                }
            }
            ApiDb.SaveChanges();
        }

        // New groups will be: 1 → 1, 2, 3, 4   |   2 → 5   |   3 → 6, 7, 8, 9, 10, 11, 12, 13
        public static void SplitApiDB(AppDbContext context, APIDbContext ApiDb)
        {
            ApiDb.Courses.AddRange(context.Courses.Select(n => new APIModels.Courses() { ID = n.ID, Name = n.CourseName, Code = n.CourseCode }));

            var reviewGroups = ApiDb.TextReviews.GroupBy(n => n.Course).ToArray();
            var courseTwo = reviewGroups.Where(n => n.Key == 2).First();
            foreach(var t in courseTwo)
            {
                t.Course = 5;
            }
            ApiDb.CourseReviews.Add(new CourseReview() { Course = 2, Reviews = 5 });

            int[] split = { 1, 2, 3, 4 };
            var courseOne = reviewGroups.Where(n => n.Key == 1).First();
            var reviews = courseOne.OrderBy(n => rng.Next()).ToArray();
            SplitCourse(reviews, split);
            foreach (var t in split)
                ApiDb.CourseReviews.Add(new CourseReview() { Course = 1, Reviews = t });

            int[] splitTwo = { 6, 7, 8, 9, 10, 11, 12, 13};
            var courseThree = reviewGroups.Where(n => n.Key == 3).First();
            reviews = courseThree.OrderBy(n => rng.Next()).ToArray();
            SplitCourse(reviews, splitTwo);
            foreach(var t in splitTwo)
                ApiDb.CourseReviews.Add(new CourseReview() { Course = 3, Reviews = t });
            ApiDb.SaveChanges();
        }

        private static void SplitCourse(TextReviews[] reviews, int[] split)
        {
            var amount = reviews.Length / split.Length;
            if (reviews.Length % split.Length != 0)
                amount++;

            for (int i = 0; i < split.Length; i++)
            {
                reviews.Skip(i * amount).Take(amount).ToList().ForEach(t => t.Course = split[i]);
            }
        }

        private static string Filter(string text)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            var t = rgx.Replace(text, "");
            t = t.ToLower();
            return t;
        }

        public static void DetectLanguage(AppDbContext context)
        {
            var doable = context.UserReviews.Where(r => (r.Type == "textarea" && r.Answer != null) || r.Comment != null).ToArray();
            var langDetec = new LanguageDetector();
            langDetec.AddLanguages("en", "no");

            foreach (var review in doable)
            {
                string rev;
                if (review.Comment != null)
                    rev = review.Comment;
                else
                    rev = review.Answer;
                review.Language = langDetec.Detect(rev);
            }

            context.SaveChanges();
        }

        public static void SetReviewScore(APIDbContext context, string folder)
        {
            var revs = context.TextReviews.GroupBy(n => n.Course).ToArray();
            foreach (var reviews in revs)
            {
                foreach (var review in reviews)
                {
                    if (File.Exists(folder + @$"\{review.Id}.txt"))
                    {
                        var data = File.ReadAllLines(folder + @$"\{review.Id}.txt");
                        if (data.Length != 0)
                        {
                            review.Score = (float)Math.Round(data.Where(n => n.StartsWith("Worse")).Count() / (float)reviews.Count(), 2);
                        }
                    }
                }
            }
            context.SaveChanges();
        }

        public static void DBtoCSV(APIDbContext context, string name)
        {
            var revs = context.TextReviews.Where(n => n.Course == 7).ToArray();
            using (var writer = new StreamWriter($@"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Data\{name}.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(revs);
            }
        }

        public static Dictionary<string, int> CommonWords(List<string> words, string fileName)
        {
            var totalwords = GetWordAmount(words.ToArray());

            /*var assifreq = totalwords.GroupBy(n => n.Value);
            foreach (var freq in assifreq)
            {
                var t = "";
                foreach (var word in freq)
                {
                    t += $"{ word.Key}\n";
                }
                File.WriteAllText(@$"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Results\{fileName}{freq.Key}.txt", t);
            }*/
            return totalwords;
        }

        public static void WriteResult(string fileName, Dictionary<string, int> result)
        {
            string t = "";
            foreach (var word in result)
                t += $"{word.Key} ({word.Value})\n";
            if (t != "")
                File.WriteAllText(@$"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Results\{fileName}.txt", t);
        }

        public static void WriteList(string fileName, string[] list)
        {
            string t = "";
            foreach (var word in list)
                t += $"{word}\n";
            if (t != "")
                File.WriteAllText(@$"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Results\{fileName}.txt", t);
        }

        public static int WordCount(string comment)
        {
            return comment.Split(" ").Length;
        }
    }
}
