using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Shop.Data.Migrations
{
    public partial class Reviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6fa174bf-cd93-4b29-bdf5-674da38c8ce9", "0999cd4f-e951-4974-b050-518e759c9956" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "e101751d-3240-4b50-b594-40e44eb48bc6", "5c20167f-90e0-4a67-9d4c-cb35c7c6b718" });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(maxLength: 5000, nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Sent = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fde99f2f-65c5-411e-a4b9-6f1cc5af4972", "944dd68f-b802-436b-8447-a2a4d88e429d", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e6d2a6f-af45-41d0-8285-6a7201cbd30e", "0f21f4c9-60d7-4ec0-bdc9-6a6b84e14855", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0e6d2a6f-af45-41d0-8285-6a7201cbd30e", "0f21f4c9-60d7-4ec0-bdc9-6a6b84e14855" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "fde99f2f-65c5-411e-a4b9-6f1cc5af4972", "944dd68f-b802-436b-8447-a2a4d88e429d" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e101751d-3240-4b50-b594-40e44eb48bc6", "5c20167f-90e0-4a67-9d4c-cb35c7c6b718", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6fa174bf-cd93-4b29-bdf5-674da38c8ce9", "0999cd4f-e951-4974-b050-518e759c9956", "Admin", null });
        }
    }
}
