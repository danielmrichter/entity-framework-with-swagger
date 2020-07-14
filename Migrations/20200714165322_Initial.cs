using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace dotnet_bakery.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bakers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bakers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BreadInventory",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    inventory = table.Column<int>(nullable: false),
                    breadType = table.Column<int>(nullable: false),
                    bakedByid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreadInventory", x => x.id);
                    table.ForeignKey(
                        name: "FK_BreadInventory_Bakers_bakedByid",
                        column: x => x.bakedByid,
                        principalTable: "Bakers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BreadInventory_bakedByid",
                table: "BreadInventory",
                column: "bakedByid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreadInventory");

            migrationBuilder.DropTable(
                name: "Bakers");
        }
    }
}
