using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryProject.Api.Migrations
{
    public partial class authors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    BirthYear = table.Column<short>(type: "smallint", nullable: false),
                    YearOfDeath = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "BirthYear", "FirstName", "LastName", "MiddleName", "YearOfDeath" },
                values: new object[] { 1, (short)1948, "George", "Martin", "R.R.", null });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "BirthYear", "FirstName", "LastName", "MiddleName", "YearOfDeath" },
                values: new object[] { 2, (short)1832, "Lewis", "Carol", "", (short)1898 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Author");
        }
    }
}
