using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Shop.Data.Migrations
{
    public partial class EOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7545a300-5a36-4ffe-b441-3a9cb0114bb1", "d88d42d9-8e6b-4d4b-aaea-207c56ae0935" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "c35bf819-ec2b-4cff-a18a-e48c730b17bc", "15089998-db18-43ff-a309-69cd713b19ed" });

            migrationBuilder.CreateTable(
                name: "EOrders",
                columns: table => new
                {
                    EOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Issued = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    OrderState = table.Column<byte>(nullable: false),
                    BuyerId = table.Column<int>(nullable: true),
                    SellerId = table.Column<int>(nullable: true),
                    BuyerPersonDetailId = table.Column<int>(nullable: true),
                    SellerPersonDetailId = table.Column<int>(nullable: true),
                    BuyerAddressId = table.Column<int>(nullable: true),
                    BuyerDeliveryAddressId = table.Column<int>(nullable: true),
                    SellerAddressId = table.Column<int>(nullable: true),
                    DeliveryProductId = table.Column<int>(nullable: true),
                    FinalPrice = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EOrders", x => x.EOrderId);
                    table.ForeignKey(
                        name: "FK_EOrders_Addresses_BuyerAddressId",
                        column: x => x.BuyerAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EOrders_Addresses_BuyerDeliveryAddressId",
                        column: x => x.BuyerDeliveryAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EOrders_People_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EOrders_PersonDetails_BuyerPersonDetailId",
                        column: x => x.BuyerPersonDetailId,
                        principalTable: "PersonDetails",
                        principalColumn: "PersonDetailId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EOrders_Products_DeliveryProductId",
                        column: x => x.DeliveryProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EOrders_Addresses_SellerAddressId",
                        column: x => x.SellerAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EOrders_People_SellerId",
                        column: x => x.SellerId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EOrders_PersonDetails_SellerPersonDetailId",
                        column: x => x.SellerPersonDetailId,
                        principalTable: "PersonDetails",
                        principalColumn: "PersonDetailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductEOrders",
                columns: table => new
                {
                    ProductEOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    EOrderId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEOrders", x => x.ProductEOrderId);
                    table.ForeignKey(
                        name: "FK_ProductEOrders_EOrders_EOrderId",
                        column: x => x.EOrderId,
                        principalTable: "EOrders",
                        principalColumn: "EOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductEOrders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "13e0f942-6dae-4a88-a811-7acacdcc56fd", "944d3e87-cf8a-4ac6-afda-0589d5d29a84", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cf27bba5-e7f2-43e5-b3dd-577828317907", "58221b7f-6f5c-401b-8aa5-97a5d37ebc0a", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_EOrders_BuyerAddressId",
                table: "EOrders",
                column: "BuyerAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EOrders_BuyerDeliveryAddressId",
                table: "EOrders",
                column: "BuyerDeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EOrders_BuyerId",
                table: "EOrders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_EOrders_BuyerPersonDetailId",
                table: "EOrders",
                column: "BuyerPersonDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_EOrders_DeliveryProductId",
                table: "EOrders",
                column: "DeliveryProductId");

            migrationBuilder.CreateIndex(
                name: "IX_EOrders_SellerAddressId",
                table: "EOrders",
                column: "SellerAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EOrders_SellerId",
                table: "EOrders",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_EOrders_SellerPersonDetailId",
                table: "EOrders",
                column: "SellerPersonDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEOrders_EOrderId",
                table: "ProductEOrders",
                column: "EOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEOrders_ProductId",
                table: "ProductEOrders",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductEOrders");

            migrationBuilder.DropTable(
                name: "EOrders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "13e0f942-6dae-4a88-a811-7acacdcc56fd", "944d3e87-cf8a-4ac6-afda-0589d5d29a84" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "cf27bba5-e7f2-43e5-b3dd-577828317907", "58221b7f-6f5c-401b-8aa5-97a5d37ebc0a" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c35bf819-ec2b-4cff-a18a-e48c730b17bc", "15089998-db18-43ff-a309-69cd713b19ed", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7545a300-5a36-4ffe-b441-3a9cb0114bb1", "d88d42d9-8e6b-4d4b-aaea-207c56ae0935", "Admin", null });
        }
    }
}
