using Microsoft.EntityFrameworkCore.Migrations;

namespace DotnetBakery.Migrations
{
    public partial class AddFavoriteRestaurantPropertyToBakers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "favoriteRestaurant",
                table: "Bakers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "favoriteRestaurant",
                table: "Bakers");
        }
    }
}
