using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExecution.Domain.Entities;

namespace TestExecution.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<UserAttempt> UserAttempts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tests)
                .WithOne(t => t.Owner)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Attempts)
                .WithOne(t => t.User)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<UserAttempt>()
                .HasMany(x => x.UserAnswers)
                .WithOne(t => t.UserAttempt)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Test>()
                .HasMany(t => t.Questions)
                .WithOne(x => x.Test)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Question>()
                .HasMany(x => x.Options)
                .WithOne(t => t.Question)
                .OnDelete(DeleteBehavior.SetNull);
        }


    }
}
