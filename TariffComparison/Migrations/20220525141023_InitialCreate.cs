using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TariffComparison.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ProductType = table.Column<int>(type: "INTEGER", nullable: false),
                    UnconditionalCosts = table.Column<decimal>(type: "TEXT", nullable: false),
                    PackageCosts = table.Column<decimal>(type: "TEXT", nullable: false),
                    InclidedInPackage = table.Column<int>(type: "INTEGER", nullable: false),
                    ConsumptionCosts = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ConsumptionCosts", "InclidedInPackage", "Name", "PackageCosts", "ProductType", "UnconditionalCosts" },
                values: new object[] { 1, 0.22m, 0, "Basic electricity tariff", 0m, 1, 5m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ConsumptionCosts", "InclidedInPackage", "Name", "PackageCosts", "ProductType", "UnconditionalCosts" },
                values: new object[] { 2, 0.3m, 4000, "Packaged tariff", 800m, 2, 0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
