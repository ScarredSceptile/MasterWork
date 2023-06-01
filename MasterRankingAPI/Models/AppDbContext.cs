using MasterRankingAPI.APIModels;
using Microsoft.EntityFrameworkCore;

namespace MasterRankingAPI.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<TextReview> TextReviews { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseReview> CourseReviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dir = Directory.GetCurrentDirectory();
            if (dir.Contains("bin"))
                dir = dir.Substring(0, dir.IndexOf("bin"));
            var file = dir + @"/db.sqlite";
            optionsBuilder.UseSqlite($"Data Source={file}");
        }
    }
}
