using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokedexAPI.Migrations
{
    public partial class UpdateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_type_id",
                table: "pokemon");

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "pokemon",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_pokemon_type2_id",
                table: "pokemon",
                column: "type2_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pokemon_pokemon_type_type2_id",
                table: "pokemon",
                column: "type2_id",
                principalTable: "pokemon_type",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_type_id",
                table: "pokemon",
                column: "type_id",
                principalTable: "pokemon_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pokemon_pokemon_type_type2_id",
                table: "pokemon");

            migrationBuilder.DropForeignKey(
                name: "fk_type_id",
                table: "pokemon");

            migrationBuilder.DropIndex(
                name: "IX_pokemon_type2_id",
                table: "pokemon");

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "pokemon",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_type_id",
                table: "pokemon",
                column: "type_id",
                principalTable: "pokemon_type",
                principalColumn: "id");
        }
    }
}
