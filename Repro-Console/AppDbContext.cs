using Microsoft.EntityFrameworkCore;
using Repro_Console.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repro_Console
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=AppData.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbArticleData>().HasAlternateKey(c => new { c.ID, c.Channel });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<DbArticleData> DbArticleData { get; set; }

    }
}
