using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrepTeach.Migrations
{
    public partial class t1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    quantity = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    price = table.Column<decimal>(type: "TEXT", nullable: false),
                    is_sold = table.Column<bool>(type: "INTEGER", nullable: false),
                    image = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    create_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    update_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    login = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    father_name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    password = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    create_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    update_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "create_date", "father_name", "first_name", "last_name", "login", "password", "update_date" },
                values: new object[] { 1L, new DateTime(2022, 2, 22, 12, 45, 34, 914, DateTimeKind.Utc).AddTicks(9801), "admin", "admin", "admin", "admin", "09", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
