using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CakeManager.Server.Migrations
{
    public partial class Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SuperCakeMark",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CakeMark",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SuperCakeMark");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CakeMark");
        }
    }
}
