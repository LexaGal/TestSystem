﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcTestSystem.Models;

namespace MvcTestSystem.Repository
{
    public class TestSystemDb : DbContext
    {
        public DbSet<User> Users;
        public DbSet<Task> Tasks;
        public DbSet<Code> Codes;
        public DbSet<Result> Results;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Task>().ToTable("Tasks");
            modelBuilder.Entity<Code>().ToTable("Codes");
            modelBuilder.Entity<Result>().ToTable("Results");
        }
    }

}