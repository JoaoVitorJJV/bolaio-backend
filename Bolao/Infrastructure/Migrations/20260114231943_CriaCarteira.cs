using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriaCarteira : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "carteiras",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    saldo_atual = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_carteiras", x => x.id);
                    table.ForeignKey(
                        name: "fk_carteiras_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "transacoes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    valor = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    data_hora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false),
                    carteira_id = table.Column<int>(type: "integer", nullable: false),
                    carteira_id1 = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transacoes", x => x.id);
                    table.ForeignKey(
                        name: "fk_transacoes_carteiras_carteira_id1",
                        column: x => x.carteira_id1,
                        principalTable: "carteiras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_carteiras_usuario_id",
                table: "carteiras",
                column: "usuario_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_transacoes_carteira_id1",
                table: "transacoes",
                column: "carteira_id1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transacoes");

            migrationBuilder.DropTable(
                name: "carteiras");
        }
    }
}
