﻿// <auto-generated />
using System;
using E_Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace E_Shop.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("E_Shop.Data.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Country");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("StreetNameAndHouseNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("E_Shop.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("E_Shop.Data.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleType");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("E_Shop.Data.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Hidden");

                    b.Property<int>("OrderNo");

                    b.Property<int?>("ParentCategoryId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new { CategoryId = 1, Hidden = false, OrderNo = 1, Title = "Obývačka", Url = "obyvacka" },
                        new { CategoryId = 2, Hidden = false, OrderNo = 4, Title = "Kuchyňa", Url = "kuchyna" },
                        new { CategoryId = 3, Hidden = false, OrderNo = 2, ParentCategoryId = 1, Title = "Záclony", Url = "zaclony" },
                        new { CategoryId = 4, Hidden = false, OrderNo = 3, ParentCategoryId = 1, Title = "Kvetináče", Url = "kvetinace" },
                        new { CategoryId = 5, Hidden = false, OrderNo = 5, ParentCategoryId = 2, Title = "Riad", Url = "riad" },
                        new { CategoryId = 6, Hidden = false, OrderNo = 6, ParentCategoryId = 2, Title = "Kuchynské dosky", Url = "kuchynske-dosky" }
                    );
                });

            modelBuilder.Entity("E_Shop.Data.Models.CategoryProduct", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("ProductId");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("CategoryProducts");
                });

            modelBuilder.Entity("E_Shop.Data.Models.EOrder", b =>
                {
                    b.Property<int>("EOrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BuyerAddressId");

                    b.Property<int?>("BuyerDeliveryAddressId");

                    b.Property<int?>("BuyerId");

                    b.Property<int?>("BuyerPersonDetailId");

                    b.Property<DateTime>("Created");

                    b.Property<int?>("DeliveryProductId");

                    b.Property<DateTime>("DueDate");

                    b.Property<decimal?>("FinalPrice");

                    b.Property<DateTime>("Issued");

                    b.Property<int?>("Number");

                    b.Property<byte>("OrderState");

                    b.Property<int?>("SellerAddressId");

                    b.Property<int?>("SellerId");

                    b.Property<int?>("SellerPersonDetailId");

                    b.Property<string>("Token");

                    b.Property<int?>("WayOfPaymentId");

                    b.HasKey("EOrderId");

                    b.HasIndex("BuyerAddressId");

                    b.HasIndex("BuyerDeliveryAddressId");

                    b.HasIndex("BuyerId");

                    b.HasIndex("BuyerPersonDetailId");

                    b.HasIndex("DeliveryProductId");

                    b.HasIndex("SellerAddressId");

                    b.HasIndex("SellerId");

                    b.HasIndex("SellerPersonDetailId");

                    b.HasIndex("WayOfPaymentId");

                    b.ToTable("EOrders");
                });

            modelBuilder.Entity("E_Shop.Data.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId");

                    b.Property<int>("DeliveryAddressId");

                    b.Property<int>("PersonDetailId");

                    b.Property<string>("UserId");

                    b.HasKey("PersonId");

                    b.HasIndex("AddressId");

                    b.HasIndex("DeliveryAddressId");

                    b.HasIndex("PersonDetailId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("People");
                });

            modelBuilder.Entity("E_Shop.Data.Models.PersonDetail", b =>
                {
                    b.Property<int>("PersonDetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyName")
                        .HasMaxLength(100);

                    b.Property<string>("DIČ")
                        .HasMaxLength(30);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Fax")
                        .HasMaxLength(20);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("IČO")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20);

                    b.Property<string>("RegistryEntry")
                        .HasMaxLength(2048);

                    b.HasKey("PersonDetailId");

                    b.ToTable("PersonDetails");
                });

            modelBuilder.Entity("E_Shop.Data.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("Hidden");

                    b.Property<int>("ImagesCount");

                    b.Property<decimal?>("OldPrice")
                        .HasColumnType("decimal(10,1)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,1)");

                    b.Property<int>("Stock");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("E_Shop.Data.Models.ProductEOrder", b =>
                {
                    b.Property<int>("ProductEOrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EOrderId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("ProductEOrderId");

                    b.HasIndex("EOrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductEOrders");
                });

            modelBuilder.Entity("E_Shop.Data.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<int>("ProductId");

                    b.Property<int>("Rating");

                    b.Property<DateTime>("Sent");

                    b.Property<string>("UserId");

                    b.HasKey("ReviewId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new { Id = "6ce98053-2597-43ed-af46-57017c33343d", ConcurrencyStamp = "8cce9a0c-019a-4c22-a401-83c9b68347f1", Name = "User" },
                        new { Id = "f3db42be-7b46-405f-b2db-1f155fe58f16", ConcurrencyStamp = "3d1f2341-dff8-44f5-b54a-2c25edddc2c1", Name = "Admin" }
                    );
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("E_Shop.Data.Models.Category", b =>
                {
                    b.HasOne("E_Shop.Data.Models.Category", "ParentCategory")
                        .WithMany("ChildCategories")
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("E_Shop.Data.Models.CategoryProduct", b =>
                {
                    b.HasOne("E_Shop.Data.Models.Category", "Category")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("E_Shop.Data.Models.Product", "Product")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("E_Shop.Data.Models.EOrder", b =>
                {
                    b.HasOne("E_Shop.Data.Models.Address", "BuyerAddress")
                        .WithMany()
                        .HasForeignKey("BuyerAddressId");

                    b.HasOne("E_Shop.Data.Models.Address", "BuyerDeliveryAddress")
                        .WithMany()
                        .HasForeignKey("BuyerDeliveryAddressId");

                    b.HasOne("E_Shop.Data.Models.Person", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId");

                    b.HasOne("E_Shop.Data.Models.PersonDetail", "BuyerPersonDetail")
                        .WithMany()
                        .HasForeignKey("BuyerPersonDetailId");

                    b.HasOne("E_Shop.Data.Models.Product", "DeliveryProduct")
                        .WithMany()
                        .HasForeignKey("DeliveryProductId");

                    b.HasOne("E_Shop.Data.Models.Address", "SellerAddress")
                        .WithMany()
                        .HasForeignKey("SellerAddressId");

                    b.HasOne("E_Shop.Data.Models.Person", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId");

                    b.HasOne("E_Shop.Data.Models.PersonDetail", "SellerPersonDetail")
                        .WithMany()
                        .HasForeignKey("SellerPersonDetailId");

                    b.HasOne("E_Shop.Data.Models.Product", "WayOfPayment")
                        .WithMany()
                        .HasForeignKey("WayOfPaymentId");
                });

            modelBuilder.Entity("E_Shop.Data.Models.Person", b =>
                {
                    b.HasOne("E_Shop.Data.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("E_Shop.Data.Models.Address", "DeliveryAddress")
                        .WithMany()
                        .HasForeignKey("DeliveryAddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("E_Shop.Data.Models.PersonDetail", "PersonDetail")
                        .WithMany()
                        .HasForeignKey("PersonDetailId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("E_Shop.Data.Models.ApplicationUser", "User")
                        .WithOne("Person")
                        .HasForeignKey("E_Shop.Data.Models.Person", "UserId");
                });

            modelBuilder.Entity("E_Shop.Data.Models.ProductEOrder", b =>
                {
                    b.HasOne("E_Shop.Data.Models.EOrder", "EOrder")
                        .WithMany("ProductEOrders")
                        .HasForeignKey("EOrderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("E_Shop.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("E_Shop.Data.Models.Review", b =>
                {
                    b.HasOne("E_Shop.Data.Models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("E_Shop.Data.Models.ApplicationUser", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("E_Shop.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("E_Shop.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("E_Shop.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("E_Shop.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
