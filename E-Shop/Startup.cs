using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Shop.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using E_Shop.Data.Models;
using E_Shop.Extensions;
using E_Shop.Business.Managers;
using E_Shop.Business.Interfaces;
using E_Shop.Data.Repositories;
using E_Shop.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Razor.TagHelpers;
using E_Shop.Classes;
using AutoMapper;
using E_Shop.Business.Classes;
using E_Shop.Business.Services;

namespace E_Shop
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddEntityFrameworkProxies();
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => false;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"))
                    .UseLazyLoadingProxies()
                    .ConfigureWarnings(x => x.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning))
                    .EnableSensitiveDataLogging(true));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            services.AddImageProcessing();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryProductRepository, CategoryProductRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonDetailRepository, PersonDetailRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IEOrderRepository, EOrderRepository>();
            services.AddScoped<IProductEOrderRepository, ProductEOrderRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();

            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IReviewManager, ReviewManager>();
            services.AddScoped<IPersonManager, PersonManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IArticleManager, ArticleManager>();
            services.AddSingleton(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc()
            .AddRazorPagesOptions(options =>
              {
                  options.Conventions.AuthorizePage("/Logout");
                  options.Conventions.AuthorizePage("/ChangePassword");
                  options.Conventions.AuthorizePage("/PassChangeConfirmation");
              });
            services.AddAutoMapper();
            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
