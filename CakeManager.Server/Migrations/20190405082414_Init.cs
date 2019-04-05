using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CakeManager.Server.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActiveDirectoryUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    OfficeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveDirectoryUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveDirectoryUser_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CakeMark",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CakeMark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CakeMark_ActiveDirectoryUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ActiveDirectoryUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuperCakeMark",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperCakeMark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperCakeMark_ActiveDirectoryUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ActiveDirectoryUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("f0b09b2f-6b1d-4565-94f6-3281009cc314"), "Aberdeen" });

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("a248d098-f3b5-4da4-a394-09505c56bf9e"), "Glasgow" });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveDirectoryUser_OfficeId",
                table: "ActiveDirectoryUser",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_CakeMark_UserId",
                table: "CakeMark",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuperCakeMark_UserId",
                table: "SuperCakeMark",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CakeMark");

            migrationBuilder.DropTable(
                name: "SuperCakeMark");

            migrationBuilder.DropTable(
                name: "ActiveDirectoryUser");

            migrationBuilder.DropTable(
                name: "Office");
        }
    }
}
