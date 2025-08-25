using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnStatusEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Converte bool -> int (true = 1, false = 0)
            migrationBuilder.Sql(@"
                ALTER TABLE ""tb_Employees"" 
                ALTER COLUMN ""Status"" DROP DEFAULT;
                
                ALTER TABLE ""tb_Employees"" 
                ALTER COLUMN ""Status"" TYPE integer 
                USING CASE 
                    WHEN ""Status"" = true THEN 0
                    ELSE 0
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""tb_Employees"" 
                ALTER COLUMN ""Status"" TYPE boolean 
                USING CASE 
                    WHEN ""Status"" = 0 THEN true
                    ELSE false
                END;

                ALTER TABLE ""tb_Employees"" 
                ALTER COLUMN ""Status"" SET DEFAULT true;
            ");
        }
    }
}
