using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Shop.Data.Migrations
{
    public partial class RelationCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(maxLength: 255, nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    OrderNo = table.Column<int>(nullable: false),
                    Hidden = table.Column<bool>(nullable: false),
                    ParentCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Url = table.Column<string>(maxLength: 255, nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    OldPrice = table.Column<decimal>(nullable: true),
                    Stock = table.Column<int>(nullable: false),
                    ImagesCount = table.Column<int>(nullable: false),
                    Hidden = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6f663b69-78eb-4679-9954-14c22d019a8e", "e9d88343-4e4c-4f9c-a925-f07e1ce58c0c", "User", null },
                    { "606d125f-a82e-47c8-9fdf-9d3fd77de05e", "2a1127ac-ed2c-4eba-a2e5-f8d6bc6f5e04", "Admin", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Hidden", "OrderNo", "ParentCategoryId", "Title", "Url" },
                values: new object[,]
                {
                    { 1, false, 1, null, "Obývací pokoj", "obyvaci-pokoj" },
                    { 2, false, 4, null, "Kuchyně", "kuchyne" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Hidden", "OrderNo", "ParentCategoryId", "Title", "Url" },
                values: new object[,]
                {
                    { 3, false, 2, 1, "Záclony", "zaclony" },
                    { 4, false, 3, 1, "Květináče", "kvetinace" },
                    { 5, false, 5, 2, "Nádobí", "nadobi" },
                    { 6, false, 6, 2, "Kuchyňské desky", "kuchynske-desky" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductId",
                table: "CategoryProduct",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "606d125f-a82e-47c8-9fdf-9d3fd77de05e", "2a1127ac-ed2c-4eba-a2e5-f8d6bc6f5e04" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6f663b69-78eb-4679-9954-14c22d019a8e", "e9d88343-4e4c-4f9c-a925-f07e1ce58c0c" });
        }
    }
}
