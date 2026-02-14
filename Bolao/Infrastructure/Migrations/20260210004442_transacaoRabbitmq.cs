using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class transacaoRabbitmq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "external_reference",
                table: "transacoes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "qr_code",
                table: "transacoes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "external_reference",
                table: "transacoes");

            migrationBuilder.DropColumn(
                name: "qr_code",
                table: "transacoes");
        }
    }
}
