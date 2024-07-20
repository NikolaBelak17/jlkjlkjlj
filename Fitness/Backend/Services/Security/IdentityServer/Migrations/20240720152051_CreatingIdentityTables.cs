using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityServer.Migrations
{
    /// <inheritdoc />
    public partial class CreatingIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72e1a1c1-9774-4cf6-a37d-48bf30b1b7ae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bcc7dee3-d727-405f-8c67-263a95d31054");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "cf9df875-9699-428e-ba4d-d2089ef0c1f1", null, "Administrator", "ADMINISTRATOR" },
                    { "dca95df7-ad44-48b6-b504-7ae456dacf1c", null, "Buyer", "BUYER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf9df875-9699-428e-ba4d-d2089ef0c1f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dca95df7-ad44-48b6-b504-7ae456dacf1c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "72e1a1c1-9774-4cf6-a37d-48bf30b1b7ae", null, "Buyer", "BUYER" },
                    { "bcc7dee3-d727-405f-8c67-263a95d31054", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
