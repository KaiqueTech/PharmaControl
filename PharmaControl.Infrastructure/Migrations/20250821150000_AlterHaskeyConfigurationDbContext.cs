using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterHaskeyConfigurationDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_tb_Suppliers_SupplierId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "tb_Products");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "tb_Employees");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SupplierId",
                table: "tb_Products",
                newName: "IX_tb_Products_SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_Products",
                table: "tb_Products",
                column: "IdProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_Employees",
                table: "tb_Employees",
                column: "IdEmployee");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_Products_tb_Suppliers_SupplierId",
                table: "tb_Products",
                column: "SupplierId",
                principalTable: "tb_Suppliers",
                principalColumn: "IdSupplier",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_Products_tb_Suppliers_SupplierId",
                table: "tb_Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_Products",
                table: "tb_Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_Employees",
                table: "tb_Employees");

            migrationBuilder.RenameTable(
                name: "tb_Products",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "tb_Employees",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_tb_Products_SupplierId",
                table: "Products",
                newName: "IX_Products_SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "IdProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "IdEmployee");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_tb_Suppliers_SupplierId",
                table: "Products",
                column: "SupplierId",
                principalTable: "tb_Suppliers",
                principalColumn: "IdSupplier",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
