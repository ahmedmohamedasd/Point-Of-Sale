using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BarItem> BarItems { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<OrdersStock> OrdersStocks { get; set; }
        public DbSet<Sheek> Sheeks { get; set; }
        public DbSet<AssignToSheek> AssignToSheeks { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ExpiredStock> ExpiredStocks { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
           

        //}
    }
}
