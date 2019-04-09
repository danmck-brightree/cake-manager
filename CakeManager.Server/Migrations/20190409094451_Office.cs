using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CakeManager.Server.Migrations
{
    public partial class Office : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveDirectoryUser_Office_OfficeId",
                table: "ActiveDirectoryUser");

            migrationBuilder.AlterColumn<Guid>(
                name: "OfficeId",
                table: "ActiveDirectoryUser",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveDirectoryUser_Office_OfficeId",
                table: "ActiveDirectoryUser",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveDirectoryUser_Office_OfficeId",
                table: "ActiveDirectoryUser");

            migrationBuilder.AlterColumn<Guid>(
                name: "OfficeId",
                table: "ActiveDirectoryUser",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveDirectoryUser_Office_OfficeId",
                table: "ActiveDirectoryUser",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
