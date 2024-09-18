using Microsoft.EntityFrameworkCore.Migrations;

namespace DotnetBakery.Migrations
{
    public partial class AddLikesWearingHatsPropertyToBakers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "likesWearingHats",
                table: "Bakers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "likesWearingHats",
                table: "Bakers");
        }
    }
}
