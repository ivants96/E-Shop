using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Shop.Data.Migrations
{
    public partial class Person : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0e6d2a6f-af45-41d0-8285-6a7201cbd30e", "0f21f4c9-60d7-4ec0-bdc9-6a6b84e14855" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "fde99f2f-65c5-411e-a4b9-6f1cc5af4972", "944dd68f-b802-436b-8447-a2a4d88e429d" });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StreetNameAndHouseNumber = table.Column<string>(maxLength: 50, nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    PostalCode = table.Column<int>(maxLength: 30, nullable: false),
                    Country = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    BankAccoutID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Swift = table.Column<string>(maxLength: 30, nullable: true),
                    Iban = table.Column<string>(maxLength: 30, nullable: true),
                    AccountOwner = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.BankAccoutID);
                });

            migrationBuilder.CreateTable(
                name: "PersonDetails",
                columns: table => new
                {
                    PersonDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Fax = table.Column<string>(maxLength: 20, nullable: true),
                    DIČ = table.Column<string>(maxLength: 30, nullable: true),
                    IČO = table.Column<string>(maxLength: 20, nullable: true),
                    RegistryEntry = table.Column<string>(maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDetails", x => x.PersonDetailId);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UseId = table.Column<string>(nullable: true),
                    DeliveryAddressId = table.Column<int>(nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    PersonDetailId = table.Column<int>(nullable: false),
                    BankAccountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_People_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "BankAccoutID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_Addresses_DeliveryAddressId",
                        column: x => x.DeliveryAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_PersonDetails_PersonDetailId",
                        column: x => x.PersonDetailId,
                        principalTable: "PersonDetails",
                        principalColumn: "PersonDetailId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_AspNetUsers_UseId",
                        column: x => x.UseId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "75122d72-5558-4ad5-9ace-d955d6256bf5", "b79b6872-0e4b-4007-b0cb-10ae07885b75", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f25ea566-ebda-4259-94e3-a833bb6adf42", "9cce510d-709a-427f-8da1-e9fb02620193", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_People_AddressId",
                table: "People",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_People_BankAccountId",
                table: "People",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_People_DeliveryAddressId",
                table: "People",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_People_PersonDetailId",
                table: "People",
                column: "PersonDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_People_UseId",
                table: "People",
                column: "UseId",
                unique: true,
                filter: "[UseId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "PersonDetails");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "75122d72-5558-4ad5-9ace-d955d6256bf5", "b79b6872-0e4b-4007-b0cb-10ae07885b75" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f25ea566-ebda-4259-94e3-a833bb6adf42", "9cce510d-709a-427f-8da1-e9fb02620193" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fde99f2f-65c5-411e-a4b9-6f1cc5af4972", "944dd68f-b802-436b-8447-a2a4d88e429d", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e6d2a6f-af45-41d0-8285-6a7201cbd30e", "0f21f4c9-60d7-4ec0-bdc9-6a6b84e14855", "Admin", null });
        }
    }
}
