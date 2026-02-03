using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class partidaBolao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "partida_id",
                table: "boloes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "times",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    sigla = table.Column<string>(type: "text", nullable: false),
                    bandeira_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_times", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "partidas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    time_a_id = table.Column<Guid>(type: "uuid", nullable: false),
                    time_b_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_partida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    resultado_time_a = table.Column<string>(type: "text", nullable: false),
                    resultado_time_b = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_partidas", x => x.id);
                    table.ForeignKey(
                        name: "fk_partidas_times_time_a_id",
                        column: x => x.time_a_id,
                        principalTable: "times",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_partidas_times_time_b_id",
                        column: x => x.time_b_id,
                        principalTable: "times",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "ix_boloes_partida_id",
                table: "boloes",
                column: "partida_id");

            migrationBuilder.CreateIndex(
                name: "ix_partidas_time_a_id",
                table: "partidas",
                column: "time_a_id");

            migrationBuilder.CreateIndex(
                name: "ix_partidas_time_b_id",
                table: "partidas",
                column: "time_b_id");

            migrationBuilder.AddForeignKey(
                name: "fk_boloes_partidas_partida_id",
                table: "boloes",
                column: "partida_id",
                principalTable: "partidas",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_boloes_partidas_partida_id",
                table: "boloes");

            migrationBuilder.DropTable(
                name: "partidas");

            migrationBuilder.DropTable(
                name: "times");

            migrationBuilder.DropIndex(
                name: "ix_boloes_partida_id",
                table: "boloes");

            migrationBuilder.DropColumn(
                name: "partida_id",
                table: "boloes");
        }
    }
}
