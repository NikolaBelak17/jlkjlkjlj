using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a80616f-ff6f-4f94-ab11-4679936d2326", null, "Trainer", "TRAINER" },
                    { "131a27b1-9b28-4261-82e5-b95cc93521ab", null, "Client", "CLIENT" },
                    { "9b89da1b-5e1c-4072-bdc2-2ea4b04f3917", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a80616f-ff6f-4f94-ab11-4679936d2326");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "131a27b1-9b28-4261-82e5-b95cc93521ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b89da1b-5e1c-4072-bdc2-2ea4b04f3917");
        }
    }
}
