using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PotionHouse.DataAccess.Migrations
{
    public partial class Add_ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Potions_PotionId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_PotionId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "PotionId",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "IngredientPotion",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "INTEGER", nullable: false),
                    PotionsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientPotion", x => new { x.IngredientsId, x.PotionsId });
                    table.ForeignKey(
                        name: "FK_IngredientPotion_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientPotion_Potions_PotionsId",
                        column: x => x.PotionsId,
                        principalTable: "Potions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientPotion_PotionsId",
                table: "IngredientPotion",
                column: "PotionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientPotion");

            migrationBuilder.AddColumn<int>(
                name: "PotionId",
                table: "Ingredients",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_PotionId",
                table: "Ingredients",
                column: "PotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Potions_PotionId",
                table: "Ingredients",
                column: "PotionId",
                principalTable: "Potions",
                principalColumn: "Id");
        }
    }
}
