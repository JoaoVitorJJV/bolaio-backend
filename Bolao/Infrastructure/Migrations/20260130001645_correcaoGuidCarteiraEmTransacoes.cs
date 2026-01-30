using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class correcaoGuidCarteiraEmTransacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transacoes_carteiras_carteira_id1",
                table: "transacoes");

            migrationBuilder.DropIndex(
                name: "ix_transacoes_carteira_id1",
                table: "transacoes");

            migrationBuilder.DropColumn(
                name: "carteira_id1",
                table: "transacoes");
            migrationBuilder.DropColumn(
               name: "carteira_id",
               table: "transacoes");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
