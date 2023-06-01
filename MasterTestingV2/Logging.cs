using CSAMS.Models;
using MasterTestingV2.APIModels;

namespace MasterTestingV2
{
    internal class Logging
    {

        public void WordCountByAssi(AppDbContext db)
        {
            var assis = db.UserReviews.Where(n => n.Language == "en").GroupBy(n => n.AssignmentID).ToList();
            foreach (var assi in assis)
            {
                var words = assi.Where(n => n.Comment != null).Select(n => n.Comment).ToList();
                words.AddRange(assi.Where(n => n.Type == "textarea" && n.Answer != null).Select(n => n.Answer));
                var amount = words.Select(n => Utility.WordCount(n)).GroupBy(n => n).OrderBy(n => n.Key).ToList();
                string text = "";
                foreach (var count in amount)
                {
                    text += $"Word Count: {count.Key},\tAmount: {count.Count()}\n";
                }
                File.WriteAllText(@$"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Results\WordCountAssignment{assi.Key}.txt", text);
            }
        }

        public void WordCountByCourse(APIDbContext apiDb)
        {
            var reviewCourse = apiDb.TextReviews.GroupBy(n => n.Course).ToList();
            foreach (var course in reviewCourse)
            {
                var amount = course.Select(n => Utility.WordCount(n.Review)).GroupBy(n => n).OrderBy(n => n.Key).ToList();
                string text = "";
                foreach (var count in amount)
                {
                    text += $"Word Count: {count.Key},\tAmount: {count.Count()}\n";
                }
                File.WriteAllText(@$"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Results\WordCountCourse{course.Key}.txt", text);
            }
        }

        public void AllReviewsWordCount(AppDbContext db)
        {
            var words = db.UserReviews.Where(r => r.Comment != null).Select(r => new { Review = r.Comment, Lang = r.Language }).ToList();
            words.AddRange(db.UserReviews.Where(r => r.Type == "textarea" && r.Answer != null).Select(r => new { Review = r.Answer, Lang = r.Language }).ToList());
            var amount = words.Select(n => Utility.WordCount(n.Review)).GroupBy(n => n).OrderBy(n => n.Key).ToList();
            string text = "";
            foreach (var count in amount)
            {
                text += $"Word Count: {count.Key},\tAmount: {count.Count()}\n";
            }
            File.WriteAllText(@"E:\UniWork\csams\ReviewRanking\MasterTestingV2\Results\WordCount.txt", text);
        }
    }
}
