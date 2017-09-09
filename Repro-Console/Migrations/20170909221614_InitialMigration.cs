using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repro_Console.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbArticleData",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Channel = table.Column<string>(nullable: false),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    //table.PrimaryKey("PK_DbArticleData", x => x.ID); // unwanted PK constraint created
                    table.UniqueConstraint("AK_DbArticleData_ID_Channel", x => new { x.ID, x.Channel });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbArticleData");
        }
    }
}
