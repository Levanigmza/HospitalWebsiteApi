using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalWebsiteApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APPOINTMENTS",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_ID = table.Column<string>(nullable: true),
                    DOCTOR_ID = table.Column<string>(nullable: true),
                    DATE = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TIME = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    COMMENT = table.Column<string>(maxLength: 50, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPOINTMENTS", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APPOINTMENTS",
                schema: "dbo");
        }
    }
}
