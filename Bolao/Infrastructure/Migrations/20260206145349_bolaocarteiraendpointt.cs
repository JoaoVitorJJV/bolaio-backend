using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bolaocarteiraendpointt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "bolao_id",
                table: "transacoes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "boloes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_transacoes_bolao_id",
                table: "transacoes",
                column: "bolao_id");

            migrationBuilder.AddForeignKey(
                name: "fk_transacoes_boloes_bolao_id",
                table: "transacoes",
                column: "bolao_id",
                principalTable: "boloes",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transacoes_boloes_bolao_id",
                table: "transacoes");

            migrationBuilder.DropIndex(
                name: "ix_transacoes_bolao_id",
                table: "transacoes");

            migrationBuilder.DropColumn(
                name: "bolao_id",
                table: "transacoes");

            migrationBuilder.DropColumn(
                name: "status",
                table: "boloes");
        }
    }
}
