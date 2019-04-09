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

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("63888896-7b57-4aba-a465-b885ae90c6be"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("6967ccfe-e252-40af-b069-9529b38ecea8"));

            migrationBuilder.AlterColumn<Guid>(
                name: "OfficeId",
                table: "ActiveDirectoryUser",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("5ffca255-621e-4a08-ab74-1b67335c9419"), "Aberdeen" });

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("7cb1c233-fb4d-4d77-9711-730bfe9415bb"), "Glasgow" });

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

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("5ffca255-621e-4a08-ab74-1b67335c9419"));

            migrationBuilder.DeleteData(
                table: "Office",
                keyColumn: "Id",
                keyValue: new Guid("7cb1c233-fb4d-4d77-9711-730bfe9415bb"));

            migrationBuilder.AlterColumn<Guid>(
                name: "OfficeId",
                table: "ActiveDirectoryUser",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("6967ccfe-e252-40af-b069-9529b38ecea8"), "Aberdeen" });

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("63888896-7b57-4aba-a465-b885ae90c6be"), "Glasgow" });

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
