using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TestDatabase.Entities;

namespace TestDatabase.Context
{
    public class TestSystemDb : DbContext
    {
        public DbSet<User> Users;
        public DbSet<Task> Tasks;
        public DbSet<Code> Codes;
        public DbSet<Result> Results;
        public DbSet<Test> Tests;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Task>().ToTable("Tasks");
            modelBuilder.Entity<Code>().ToTable("Codes");
            modelBuilder.Entity<Result>().ToTable("Results");
            modelBuilder.Entity<Test>().ToTable("Tests");
        }
    }

}