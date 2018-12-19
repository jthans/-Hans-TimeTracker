using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hans.App.TimeTracker.Migrations
{
    public partial class AddedTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Description", "ExternalApplication" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "DevOrg_01", "Slack" });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Description", "ExternalApplication" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), "DevOrg_02", "Slack" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ExternalId", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "User01", "DevUser_01" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));
        }
    }
}
