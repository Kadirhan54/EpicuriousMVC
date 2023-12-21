using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epicurious.Infrastructure.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddedApprovingToRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Recipes",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Recipes");
        }
    }
}
