using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Repro_Console;

namespace Repro_Console.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20170909221614_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Repro_Console.Database.DbArticleData", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Channel")
                        .IsRequired();

                    b.Property<string>("Data");

                    b.HasKey("ID");

                    b.HasAlternateKey("ID", "Channel");

                    b.ToTable("DbArticleData");
                });
        }
    }
}
