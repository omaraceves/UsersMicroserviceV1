using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Users.API.Migrations
{
    public partial class UsersV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 30, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 16, nullable: false),
                    WatchLaterId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "UserName", "WatchLaterId" },
                values: new object[] { new Guid("36de8e2f-e3ff-4718-a2e8-c9fc08610728"), "omar.aceves@mymail.com", "password", "omaraceves", new Guid("34487500-ba60-4962-9342-907c4665681f") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "UserName", "WatchLaterId" },
                values: new object[] { new Guid("443ed08c-e98a-4ac1-84ed-fee7b27513f9"), "karen.aceves@mymail.com", "password", "karenaceves", new Guid("1c389e4c-0272-48ea-b260-6b29c0c51ad7") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "UserName", "WatchLaterId" },
                values: new object[] { new Guid("434c538e-7aed-42d7-a667-92b0760ef88c"), "milo.woof@mymail.com", "password", "milowoof", new Guid("c3e95c44-1e39-4328-9d52-e0291ddf2da7") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
