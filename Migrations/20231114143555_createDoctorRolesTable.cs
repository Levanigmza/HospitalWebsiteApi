using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalWebsiteApi.Migrations
{
    /// <inheritdoc />
    public partial class createDoctorRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DOCTOR_ROLES",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DOCTOR_ID = table.Column<string>(nullable: true),
                    POSSITION = table.Column<string>(maxLength: 50, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCTOR_ROLES", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOCTOR_ROLES",
                schema: "dbo");
        }
    }
}
