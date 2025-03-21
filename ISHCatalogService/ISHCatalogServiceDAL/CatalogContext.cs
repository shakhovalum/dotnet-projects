using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using ISHCatalogServiceDAL.Entities;
using System.Reflection.Emit;

namespace ISHCatalogServiceDAL
{
    public class CatalogContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ISHCatalog;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer("Server=localhost;Database=ISHCatalog;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Image).IsRequired(false);
                entity.HasOne(e => e.ParentCategory)
                      .WithMany()
                      .HasForeignKey(e => e.ParentCategoryId)
                      .IsRequired(false);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).IsRequired(false);
                entity.Property(e => e.Image).IsRequired(false);
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.Amount).IsRequired();
                entity.HasOne(e => e.Category)
                      .WithMany()
                      .HasForeignKey(e => e.CategoryId)
                      .IsRequired();
            });
        }
    }
}