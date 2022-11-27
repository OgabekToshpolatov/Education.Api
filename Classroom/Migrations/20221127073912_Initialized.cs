using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Classroom.Migrations
{
    public partial class Initialized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalizedStrings",
                columns: table => new
                {
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Uz = table.Column<string>(type: "TEXT", nullable: true),
                    Ru = table.Column<string>(type: "TEXT", nullable: true),
                    En = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedStrings", x => x.Key);
                });

            migrationBuilder.InsertData(
                table: "LocalizedStrings",
                columns: new[] { "Key", "En", "Ru", "Uz" },
                values: new object[] { "Required", "{0} field is required", "{0} ruscha", "{0} kiritilishi kerak" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalizedStrings");
        }
    }
}
