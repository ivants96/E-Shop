using System;
using System.Collections.Generic;
using System.Text;
using E_Shop.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CategoryProduct>().HasKey(cp => new { cp.CategoryId, cp.ProductId });

            builder.Entity<CategoryProduct>()
                .HasOne(cp => cp.Category)
                .WithMany(c => c.CategoryProducts)
                .HasForeignKey(cp => cp.CategoryId);

            builder.Entity<CategoryProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.CategoryProducts)
                .HasForeignKey(cp => cp.ProductId);


            builder.Entity<IdentityRole>()
            .HasData(new IdentityRole("User"),
             new IdentityRole("Admin"));

            builder.Entity<Category>().HasData
            (
                new Category() { CategoryId = 1, Title = "Obývací pokoj", Url = "obyvaci-pokoj", OrderNo = 1, Hidden = false },
                new Category() { CategoryId = 2, Title = "Kuchyně", Url = "kuchyne", OrderNo = 4, Hidden = false },

                new Category() { CategoryId = 3, ParentCategoryId = 1, Title = "Záclony", Url = "zaclony", OrderNo = 2, Hidden = false },
                new Category() { CategoryId = 4, ParentCategoryId = 1, Title = "Květináče", Url = "kvetinace", OrderNo = 3, Hidden = false },
                new Category() { CategoryId = 5, ParentCategoryId = 2, Title = "Nádobí", Url = "nadobi", OrderNo = 5, Hidden = false },
                new Category() { CategoryId = 6, ParentCategoryId = 2, Title = "Kuchyňské desky", Url = "kuchynske-desky", OrderNo = 6, Hidden = false }
            );
           

        }

    }
}
