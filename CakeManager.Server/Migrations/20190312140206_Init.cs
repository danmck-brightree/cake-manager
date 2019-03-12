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
                name: "TempUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    OfficeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempUser_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_CakeMark_TempUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TempUser",
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
                        name: "FK_SuperCakeMark_TempUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TempUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("7357d973-d996-4c94-8806-a6264d158af6"), "Aberdeen" });

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("c4b38f56-a424-41ea-94ed-238131ecf415"), "Glasgow" });

            migrationBuilder.InsertData(
                table: "TempUser",
                columns: new[] { "Id", "Email", "Name", "OfficeId", "Password" },
                values: new object[] { new Guid("2234a4e3-7ae6-4882-b204-81f38a3d7d47"), "dmckenzie@brightree.com", "Daniel McKenzie", new Guid("7357d973-d996-4c94-8806-a6264d158af6"), "temp" });

            migrationBuilder.InsertData(
                table: "TempUser",
                columns: new[] { "Id", "Email", "Name", "OfficeId", "Password" },
                values: new object[] { new Guid("9cf309c5-aee8-4952-be05-d377d774875e"), "jsmith@brightree.com", "John Smith", new Guid("7357d973-d996-4c94-8806-a6264d158af6"), "temp" });

            migrationBuilder.InsertData(
                table: "TempUser",
                columns: new[] { "Id", "Email", "Name", "OfficeId", "Password" },
                values: new object[] { new Guid("2fe364cb-e700-4c04-90ee-87908bd2f259"), "tperson@brightree.com", "Test Person", new Guid("c4b38f56-a424-41ea-94ed-238131ecf415"), "temp" });

            migrationBuilder.CreateIndex(
                name: "IX_CakeMark_UserId",
                table: "CakeMark",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuperCakeMark_UserId",
                table: "SuperCakeMark",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TempUser_OfficeId",
                table: "TempUser",
                column: "OfficeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CakeMark");

            migrationBuilder.DropTable(
                name: "SuperCakeMark");

            migrationBuilder.DropTable(
                name: "TempUser");

            migrationBuilder.DropTable(
                name: "Office");
        }
    }
}
