using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickSpace.DataAccess.Migrations
{
    public partial class UserDel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "317e76f8-b0dd-4e08-a0ec-643254a71b53");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "412ef595-bca7-4b94-a7c3-676aedc6f1d7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3ff1cfd2-fb6a-4eb8-81dc-ab46fb32cc5c", "1a9e327d-acd3-483e-bdca-3edc1c606dc2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f65cce8c-b8f1-4cbc-a8f2-a1859a350c13", "c00a6140-bbbf-4e7e-a9f1-8bbd016b66bf", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ff1cfd2-fb6a-4eb8-81dc-ab46fb32cc5c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f65cce8c-b8f1-4cbc-a8f2-a1859a350c13");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "317e76f8-b0dd-4e08-a0ec-643254a71b53", "f8cafe74-231b-4c1c-b095-4ba4fe809b8b", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "412ef595-bca7-4b94-a7c3-676aedc6f1d7", "dcf3f4de-5684-4682-9326-27bdb20149b8", "User", "USER" });
        }
    }
}
