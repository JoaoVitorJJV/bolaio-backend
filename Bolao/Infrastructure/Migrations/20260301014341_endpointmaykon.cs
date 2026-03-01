using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class endpointmaykon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status_partida",
                table: "partidas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "palpite_gols_a",
                table: "palpites",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "palpite_gols_b",
                table: "palpites",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status_partida",
                table: "partidas");

            migrationBuilder.DropColumn(
                name: "palpite_gols_a",
                table: "palpites");

            migrationBuilder.DropColumn(
                name: "palpite_gols_b",
                table: "palpites");
        }
    }
}
