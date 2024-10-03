using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplyChain.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Tabela_Estoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estoque",
                schema: "Inventario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Est_Local = table.Column<string>(type: "text", nullable: false),
                    Est_Estoque = table.Column<int>(type: "integer", nullable: false),
                    Merc_MercadoriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataDeCriacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DataDeAlteracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estoque_Mercadoria_Merc_MercadoriaId",
                        column: x => x.Merc_MercadoriaId,
                        principalSchema: "Inventario",
                        principalTable: "Mercadoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_Merc_MercadoriaId",
                schema: "Inventario",
                table: "Estoque",
                column: "Merc_MercadoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estoque",
                schema: "Inventario");
        }
    }
}
