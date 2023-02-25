﻿using Microsoft.EntityFrameworkCore;
using MyAspCoreApp.Web.Models;

namespace MyAspCoreApp.Web.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<MyAspCoreApp.Web.Models.Category> Category { get; set; }
    }
}
