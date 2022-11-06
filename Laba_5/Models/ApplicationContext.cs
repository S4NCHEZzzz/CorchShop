using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Laba_5.Models
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(b => b.User)
                .WithOne(i => i.Order)
                .HasForeignKey<Order>(b => b.User_Id);

            //modelBuilder.Entity<Product>()
            //   .HasOne(b => b.Order)
            //   .WithOne(i => i.product)
            //   .HasForeignKey<Product>(b => b.Order_product);
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();//Создаем БД при первом обращении
        }
    }
}
