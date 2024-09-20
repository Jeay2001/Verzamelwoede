using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerzamelWoede.Models;

namespace Verzamelwoede.Data
{
    public class VerzamelwoedeDB : DbContext
    {
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Collection> Collections { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public VerzamelwoedeDB(DbContextOptions<VerzamelwoedeDB> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.; Initial Catalog=VerzamelWoede;Integrated Security=true;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Collection to Category (one-to-many)
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Collection)
                .WithMany(c => c.Categories)
                .HasForeignKey(c => c.CollectionId);


            // Category to Item (one-to-many)
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CategoryId);
        }
    }
}
