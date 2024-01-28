using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokedexAPI.Migrations
{
    public partial class AddType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_type_id",
                table: "pokemon");

            migrationBuilder.AlterColumn<int>(
                name: "type1_id",
                table: "pokemon",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_pokemon_pokemon_type_type2_id",
                table: "pokemon",
                column: "type2_id",
                principalTable: "pokemon_type",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_type_id",
                table: "pokemon",
                column: "type1_id",
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

            migrationBuilder.AlterColumn<int>(
                name: "type1_id",
                table: "pokemon",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_type_id",
                table: "pokemon",
                column: "type1_id",
                principalTable: "pokemon_type",
                principalColumn: "id");
        }
    }
}
