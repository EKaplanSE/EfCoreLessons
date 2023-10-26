using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Seeding.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Url" },
                values: new object[] { 11, "www.gencayyildiz.com/blog" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Url" },
                values: new object[] { 1, "www.gencayyildiz.com/blog" });
        }
    }
}
