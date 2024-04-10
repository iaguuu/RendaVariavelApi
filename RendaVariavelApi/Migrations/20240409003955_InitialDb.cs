using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RendaVariavelApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COTACOES",
                columns: table => new
                {
                    ID_DIVIDENDO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VALOR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DATA_COTACAO = table.Column<DateTime>(type: "date", nullable: false),
                    TICKER = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COTACOES", x => x.ID_DIVIDENDO);
                });

            migrationBuilder.CreateTable(
                name: "DIVIDENDOS",
                columns: table => new
                {
                    ID_DIVIDENDO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TIPO_DIVIDENDO = table.Column<int>(type: "int", nullable: false),
                    DATA_COM = table.Column<DateTime>(type: "date", nullable: false),
                    DATA_PAGAMENTO = table.Column<DateTime>(type: "date", nullable: false),
                    VALOR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TICKER = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIVIDENDOS", x => x.ID_DIVIDENDO);
                });

            migrationBuilder.CreateTable(
                name: "FUNDOS_IMOBILIARIOS",
                columns: table => new
                {
                    ID_FUNDO_IMOBILIARIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SEGMENTO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PUBLICO_ALVO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MANDATO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TIPO_FUNDO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PRAZO_DURACAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TIPO_GESTAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TAXA_ADMINISTRACAO = table.Column<double>(type: "float", nullable: true),
                    VACANCIA = table.Column<double>(type: "float", nullable: true),
                    NUMERO_COTISTAS = table.Column<int>(type: "int", nullable: true),
                    COTAS_EMITIDAS = table.Column<int>(type: "int", nullable: true),
                    VALOR_PATRIMONIAL_POR_COTA = table.Column<double>(type: "float", nullable: true),
                    VALOR_PATRIMONIAL = table.Column<double>(type: "float", nullable: true),
                    ULTIMO_RENDIMENTO = table.Column<double>(type: "float", nullable: true),
                    TICKER = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TIPO_INVESTIMENTO = table.Column<int>(type: "int", nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    RAZAO_SOCIAL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNDOS_IMOBILIARIOS", x => x.ID_FUNDO_IMOBILIARIO);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FUNDOS_IMOBILIARIOS_TICKER",
                table: "FUNDOS_IMOBILIARIOS",
                column: "TICKER",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COTACOES");

            migrationBuilder.DropTable(
                name: "DIVIDENDOS");

            migrationBuilder.DropTable(
                name: "FUNDOS_IMOBILIARIOS");
        }
    }
}
