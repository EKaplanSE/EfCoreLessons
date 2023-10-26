using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inheritance_Table_Per_Hierarchy.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Persons",
                newName: "ayirici");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ayirici",
                table: "Persons",
                newName: "Discriminator");
        }
    }
}
