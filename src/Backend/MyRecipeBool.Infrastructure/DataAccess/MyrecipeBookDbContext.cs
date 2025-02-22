﻿using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBool.Infrastructure.DataAccess
{
    public class MyrecipeBookDbContext :DbContext
    {
        public MyrecipeBookDbContext(DbContextOptions options) : base(options){}
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyrecipeBookDbContext).Assembly);
        }
    }
}
