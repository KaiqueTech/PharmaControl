using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "tb_Employees",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "tb_Employees");
        }
    }
}
