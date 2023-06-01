using MasterRankingAPI.APIModels;
using MasterRankingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace MasterRankingAPI
{
    public class Functions
    {
        public static string GetSalt()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        //TODO: Create function?
        public static async Task<bool> IsUserLoggedIn(AppDbContext context, Appdata user)
        {
            var usr = await context.User.Where(n => n.UserName == user.UserName).FirstOrDefaultAsync();
            if (usr == null) return false;

            HashAlgorithm sha = SHA256.Create();
            var connection = sha.ComputeHash(Encoding.ASCII.GetBytes(user.ConnectionToken + usr.Salt));
            if (usr.ConnectionHash == Encoding.Default.GetString(connection))
                return true;
            else return false;
        }

        public static string GetCourseName(int review, AppDbContext context)
        {
            var courseID = context.CourseReviews.Where(n => n.Reviews == review).Select(n => n.Course).First();
            return context.Courses.Where(n => n.ID == courseID).Select(n => n.Name).First();
        }

        public static List<TextReview> LoadReviews(TextReview[] reviews, string user)
        {

            var folder = Directory.GetCurrentDirectory() + "/Data/" + user;
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
            return rev;
        }

        public static void SaveComparisons(RankingInfo[] rankings, string user)
        {
            var files = rankings.GroupBy(n => n.ReviewID).ToList();
            var folder = Directory.GetCurrentDirectory() + "/Data/" + user;

            Parallel.ForEach(files, file =>
            {
                var output = file.Select(n => $"{n.Comparison} {n.OtherReviewID}").ToList();
                File.AppendAllLines(folder + $"/{file.Key}.txt", output);
            });
        }
    }
}
