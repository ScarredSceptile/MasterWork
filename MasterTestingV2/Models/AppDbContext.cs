using Microsoft.EntityFrameworkCore;
using System;

namespace CSAMS.Models
{
    /// <summary>
    /// Class for connecting with the database
    /// </summary>
    public class AppDbContext : DbContext
    {
        public string Connection;
        public AppDbContext(string connection)
        {
            Connection = connection;
        }

        public DbSet<Assignments> Assignments { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Fields> Fields { get; set; }
        public DbSet<Forms> Forms { get; set; }
        public DbSet<PeerReviews> PeerReviews { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Submissions> Submissions { get; set; }
        public DbSet<UserCourses> UserCourses { get; set; }
        public DbSet<UserReviews> UserReviews { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserSubmissions> UserSubmissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={Connection}");
        }
    }

}
