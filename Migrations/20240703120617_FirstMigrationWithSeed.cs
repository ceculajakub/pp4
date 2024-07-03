using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceMvc.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigrationWithSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "ProviderName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProviderName",
                table: "Products",
                newName: "Name");
        }
    }
}
