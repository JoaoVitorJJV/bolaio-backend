using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class grupoentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "grupo_id",
                table: "times",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "grupos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    letra = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grupos", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_times_grupo_id",
                table: "times",
                column: "grupo_id");

            migrationBuilder.AddForeignKey(
                name: "fk_times_grupos_grupo_id",
                table: "times",
                column: "grupo_id",
                principalTable: "grupos",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_times_grupos_grupo_id",
                table: "times");

            migrationBuilder.DropTable(
                name: "grupos");

            migrationBuilder.DropIndex(
                name: "ix_times_grupo_id",
                table: "times");

            migrationBuilder.DropColumn(
                name: "grupo_id",
                table: "times");
        }
    }
}
