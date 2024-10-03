using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplyChain.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Inventario");

            migrationBuilder.CreateTable(
                name: "TipoDeMercadoria",
                schema: "Inventario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tip_Nome = table.Column<string>(type: "text", nullable: false),
                    DataDeCriacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DataDeAlteracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeMercadoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mercadoria",
                schema: "Inventario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Mer_NumeroDeRegistro = table.Column<string>(type: "text", nullable: false),
                    Mer_Nome = table.Column<string>(type: "text", nullable: false),
                    Mer_Fabricante = table.Column<string>(type: "text", nullable: false),
                    Mer_Descricao = table.Column<string>(type: "text", nullable: false),
                    Tip_TipoMercadoriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataDeCriacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DataDeAlteracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercadoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mercadoria_TipoDeMercadoria_Tip_TipoMercadoriaId",
                        column: x => x.Tip_TipoMercadoriaId,
                        principalSchema: "Inventario",
                        principalTable: "TipoDeMercadoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entrada",
                schema: "Inventario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ent_Quantidade = table.Column<int>(type: "integer", nullable: false),
                    Ent_Local = table.Column<string>(type: "text", nullable: false),
                    Ent_DataDaEntrada = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Mer_MercadoriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataDeCriacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DataDeAlteracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entrada_Mercadoria_Mer_MercadoriaId",
                        column: x => x.Mer_MercadoriaId,
                        principalSchema: "Inventario",
                        principalTable: "Mercadoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Saida",
                schema: "Inventario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sai_Quantidade = table.Column<int>(type: "integer", nullable: false),
                    Sai_Local = table.Column<string>(type: "text", nullable: false),
                    Sai_DataDaSaida = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Mer_MercadoriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataDeCriacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DataDeAlteracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Saida_Mercadoria_Mer_MercadoriaId",
                        column: x => x.Mer_MercadoriaId,
                        principalSchema: "Inventario",
                        principalTable: "Mercadoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entrada_Mer_MercadoriaId",
                schema: "Inventario",
                table: "Entrada",
                column: "Mer_MercadoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mercadoria_Tip_TipoMercadoriaId",
                schema: "Inventario",
                table: "Mercadoria",
                column: "Tip_TipoMercadoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Saida_Mer_MercadoriaId",
                schema: "Inventario",
                table: "Saida",
                column: "Mer_MercadoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entrada",
                schema: "Inventario");

            migrationBuilder.DropTable(
                name: "Saida",
                schema: "Inventario");

            migrationBuilder.DropTable(
                name: "Mercadoria",
                schema: "Inventario");

            migrationBuilder.DropTable(
                name: "TipoDeMercadoria",
                schema: "Inventario");
        }
    }
}
