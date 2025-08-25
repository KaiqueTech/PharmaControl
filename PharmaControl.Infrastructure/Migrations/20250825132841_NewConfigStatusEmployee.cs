using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewConfigStatusEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "tb_Employees",
                type: "integer",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "tb_Employees",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
