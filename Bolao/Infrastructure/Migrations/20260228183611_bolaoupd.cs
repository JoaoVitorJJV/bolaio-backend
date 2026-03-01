using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bolaoupd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "partida",
                table: "boloes");

            migrationBuilder.AddColumn<Guid>(
                name: "partida_id",
                table: "boloes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_boloes_partida_id",
                table: "boloes",
                column: "partida_id");

            migrationBuilder.AddForeignKey(
                name: "fk_boloes_partidas_partida_id",
                table: "boloes",
                column: "partida_id",
                principalTable: "partidas",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_boloes_partidas_partida_id",
                table: "boloes");

            migrationBuilder.DropIndex(
                name: "ix_boloes_partida_id",
                table: "boloes");

            migrationBuilder.DropColumn(
                name: "partida_id",
                table: "boloes");

            migrationBuilder.AddColumn<string>(
                name: "partida",
                table: "boloes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
