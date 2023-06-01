using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTestingV2.APIModels
{
    public class APIDbContext : DbContext
    {
        private string Connection;
        public APIDbContext(string connection)
        {
            Connection= connection;
        }

        public DbSet<TextReviews> TextReviews { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<CourseReview> CourseReviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={Connection}");
        }
    }
}
