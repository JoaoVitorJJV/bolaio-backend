using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DebitoEndpoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "transacao_id",
                table: "palpites",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "taxa_administrativa",
                table: "boloes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "ix_palpites_transacao_id",
                table: "palpites",
                column: "transacao_id");

            migrationBuilder.AddForeignKey(
                name: "fk_palpites_transacoes_transacao_id",
                table: "palpites",
                column: "transacao_id",
                principalTable: "transacoes",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_palpites_transacoes_transacao_id",
                table: "palpites");

            migrationBuilder.DropIndex(
                name: "ix_palpites_transacao_id",
                table: "palpites");

            migrationBuilder.DropColumn(
                name: "transacao_id",
                table: "palpites");

            migrationBuilder.DropColumn(
                name: "taxa_administrativa",
                table: "boloes");
        }
    }
}
