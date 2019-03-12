using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CakeManager.Server.Migrations
{
    public partial class TempUser : Migration
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
                name: "TempUserToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Token = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempUserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempUserToken_TempUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TempUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("dfc0c273-3e77-4d27-a29a-c400ed740431"), "Aberdeen" });

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("31cf2f00-69a8-4a6a-90b4-2f34b907bf05"), "Glasgow" });

            migrationBuilder.InsertData(
                table: "TempUser",
                columns: new[] { "Id", "Email", "Name", "OfficeId", "Password" },
                values: new object[] { new Guid("2234a4e3-7ae6-4882-b204-81f38a3d7d47"), "dmckenzie@brightree.com", "Daniel McKenzie", new Guid("dfc0c273-3e77-4d27-a29a-c400ed740431"), "temp" });

            migrationBuilder.InsertData(
                table: "TempUser",
                columns: new[] { "Id", "Email", "Name", "OfficeId", "Password" },
                values: new object[] { new Guid("2f24cae6-02da-48e6-b933-d4ac6849310b"), "jsmith@brightree.com", "John Smith", new Guid("dfc0c273-3e77-4d27-a29a-c400ed740431"), "temp" });

            migrationBuilder.InsertData(
                table: "TempUser",
                columns: new[] { "Id", "Email", "Name", "OfficeId", "Password" },
                values: new object[] { new Guid("ec626486-ccf2-4ef7-9eca-42c81d6de4a9"), "tperson@brightree.com", "Test Person", new Guid("31cf2f00-69a8-4a6a-90b4-2f34b907bf05"), "temp" });

            migrationBuilder.InsertData(
                table: "TempUserToken",
                columns: new[] { "Id", "Token", "UserId" },
                values: new object[] { new Guid("dbeac247-b2ef-4dc8-be09-0f5370596ecf"), "06a0d9bf-6932-4807-a672-a88d23b37f78", new Guid("2234a4e3-7ae6-4882-b204-81f38a3d7d47") });

            migrationBuilder.CreateIndex(
                name: "IX_CakeMark_UserId",
                table: "CakeMark",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TempUser_OfficeId",
                table: "TempUser",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_TempUserToken_UserId",
                table: "TempUserToken",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CakeMark_TempUser_UserId",
                table: "CakeMark",
                column: "UserId",
                principalTable: "TempUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CakeMark_TempUser_UserId",
                table: "CakeMark");

            migrationBuilder.DropTable(
                name: "TempUserToken");

            migrationBuilder.DropTable(
                name: "TempUser");

            migrationBuilder.DropTable(
                name: "Office");

            migrationBuilder.DropIndex(
                name: "IX_CakeMark_UserId",
                table: "CakeMark");
        }
    }
}
