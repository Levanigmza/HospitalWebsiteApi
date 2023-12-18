using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalWebsiteApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableHospUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "HOSP_USERS",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_ROLE = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SURNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PERSONALID = table.Column<int>(type: "int", nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHONE = table.Column<int>(type: "int", nullable: true),
                    BIRTHDATE = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOSP_USERS", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HOSP_USERS",
                schema: "dbo");
        }
    }
}
