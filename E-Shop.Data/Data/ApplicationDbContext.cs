using System;
using System.Collections.Generic;
using System.Linq;
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
        DbSet<CategoryProduct> CategoryProducts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);                       
            
            builder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(10,1)");
            builder.Entity<Product>().Property(x => x.OldPrice).HasColumnType("decimal(10,1)");

            builder.Entity<Category>().Property(x => x.CategoryId).ValueGeneratedOnAdd();

            builder.Entity<CategoryProduct>().HasKey(cp => new { cp.CategoryId, cp.ProductId });

            builder.Entity<CategoryProduct>()
                .HasOne(cp => cp.Category)
                .WithMany(c => c.CategoryProducts)
                .HasForeignKey(cp => cp.CategoryId);

            builder.Entity<CategoryProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.CategoryProducts)
                .HasForeignKey(cp => cp.ProductId);

            var cascadeFKs = builder.Model.GetEntityTypes()
                                    .SelectMany(t => t.GetForeignKeys())
                                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<IdentityRole>()
            .HasData(new IdentityRole("User"),
             new IdentityRole("Admin"));

            builder.Entity<Category>().HasData
            (
                new Category() { CategoryId = 1, Title = "Obývačka", Url = "obyvacka", OrderNo = 1, Hidden = false },
                new Category() { CategoryId = 2, Title = "Kuchyňa", Url = "kuchyna", OrderNo = 4, Hidden = false },

                new Category() { CategoryId = 3, ParentCategoryId = 1, Title = "Záclony", Url = "zaclony", OrderNo = 2, Hidden = false },
                new Category() { CategoryId = 4, ParentCategoryId = 1, Title = "Kvetináče", Url = "kvetinace", OrderNo = 3, Hidden = false },
                new Category() { CategoryId = 5, ParentCategoryId = 2, Title = "Riad", Url = "riad", OrderNo = 5, Hidden = false },
                new Category() { CategoryId = 6, ParentCategoryId = 2, Title = "Kuchynské dosky", Url = "kuchynske-dosky", OrderNo = 6, Hidden = false }
            );


        }

    }
}
