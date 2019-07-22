using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Shop.Data.Migrations
{
    public partial class Article : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "644cd43d-a741-485c-a69f-9c2ab50be401", "e47f5f8a-f2c9-445b-870f-8188fb214284" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6b9d7b06-e2fd-4cfa-8fec-2ee347725e02", "fceb7edd-9d45-4527-86b8-52628f9d24bc" });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    ArticleType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6ce98053-2597-43ed-af46-57017c33343d", "8cce9a0c-019a-4c22-a401-83c9b68347f1", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f3db42be-7b46-405f-b2db-1f155fe58f16", "3d1f2341-dff8-44f5-b54a-2c25edddc2c1", "Admin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6ce98053-2597-43ed-af46-57017c33343d", "8cce9a0c-019a-4c22-a401-83c9b68347f1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f3db42be-7b46-405f-b2db-1f155fe58f16", "3d1f2341-dff8-44f5-b54a-2c25edddc2c1" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6b9d7b06-e2fd-4cfa-8fec-2ee347725e02", "fceb7edd-9d45-4527-86b8-52628f9d24bc", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "644cd43d-a741-485c-a69f-9c2ab50be401", "e47f5f8a-f2c9-445b-870f-8188fb214284", "Admin", null });
        }
    }
}
