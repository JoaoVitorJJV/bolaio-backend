using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BolaoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "boloes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    organizador_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_fechamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    visibilidade = table.Column<int>(type: "integer", nullable: false),
                    tipo_esporte = table.Column<int>(type: "integer", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    valor = table.Column<decimal>(type: "numeric", nullable: false),
                    tipo_bolao = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_boloes", x => x.id);
                    table.ForeignKey(
                        name: "fk_boloes_usuarios_organizador_id",
                        column: x => x.organizador_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "palpites",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    participante_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bolao_id = table.Column<Guid>(type: "uuid", nullable: false),
                    qtd_cotas = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_palpites", x => x.id);
                    table.ForeignKey(
                        name: "fk_palpites_boloes_bolao_id",
                        column: x => x.bolao_id,
                        principalTable: "boloes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_palpites_usuarios_participante_id",
                        column: x => x.participante_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "ix_boloes_organizador_id",
                table: "boloes",
                column: "organizador_id");

            migrationBuilder.CreateIndex(
                name: "ix_palpites_bolao_id",
                table: "palpites",
                column: "bolao_id");

            migrationBuilder.CreateIndex(
                name: "ix_palpites_participante_id",
                table: "palpites",
                column: "participante_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "palpites");

            migrationBuilder.DropTable(
                name: "boloes");
        }
    }
}
