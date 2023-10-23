using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotaSys.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arquivos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArquivoXml = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    serie = table.Column<int>(type: "int", nullable: false),
                    nNf = table.Column<int>(type: "int", nullable: false),
                    DEmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnidadeFederativa = table.Column<int>(type: "int", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arquivos");
        }
    }
}
