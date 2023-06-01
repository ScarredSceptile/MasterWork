using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewRanking.Models
{
    public class AppDbContext : DbContext
    {
        private string Connection;
        public AppDbContext(string connection)
        {
            Connection= connection;
        }

        public DbSet<TextReview> TextReviews { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<CourseReview> CourseReviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={Connection}");
        }
    }
}
