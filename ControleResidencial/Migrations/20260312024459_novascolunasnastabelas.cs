using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleResidencial.Migrations
{
    /// <inheritdoc />
    public partial class novascolunasnastabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Categoria",
                table: "Transacoes",
                newName: "CategoriaId");

            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataTransacao",
                table: "Transacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "DataTransacao",
                table: "Transacoes");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Transacoes",
                newName: "Categoria");
        }
    }
}
