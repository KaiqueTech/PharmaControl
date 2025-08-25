using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingStatusColumnFromSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "tb_Suppliers",
                newName: "Cnpj");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "tb_Suppliers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "tb_Suppliers");

            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "tb_Suppliers",
                newName: "CNPJ");
        }
    }
}
