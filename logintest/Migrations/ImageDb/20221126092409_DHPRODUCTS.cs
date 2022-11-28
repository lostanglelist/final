using Microsoft.EntityFrameworkCore.Migrations;

namespace logintest.Migrations.ImageDb
{
    public partial class DHPRODUCTS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ProductId = table.Column<string>(nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    ProductPrice = table.Column<double>(type: "float", nullable: false),
                    ProductDetail = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    ProductSlot = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Series = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    ProductSize = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ProductRam = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ProductGPU = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ProductPOWER = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ProductImage = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ProductId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
