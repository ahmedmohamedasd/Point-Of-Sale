using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PointOfSale.Data;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale
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
            services.AddSession();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequiredLength = 6;
                option.Password.RequireUppercase = true;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireLowercase = false;

            });
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter());
            });
            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
            options =>
            {
                options.LoginPath = "/Accounting/Login";
                options.LogoutPath = "/Accounting/Logout";
                options.AccessDeniedPath = "/Accounting/AccessDenied";
                options.SlidingExpiration = true;
              
            });
            services.AddAuthorizationCore(
               options =>
               {
                    //options.AddPolicy("UsersEdit",
                    //    policy => policy.RequireAssertion(
                    //              context =>context.User.HasClaim());
                    // users policy
                    options.AddPolicy("UsersShow",
                       policy => policy.RequireClaim("UsersShow", "true"));
                   options.AddPolicy("UsersAdd",
                       policy => policy.RequireClaim("UsersAdd", "true"));
                   options.AddPolicy("UsersEdit",
                       policy => policy.RequireClaim("UsersEdit", "true"));
                   options.AddPolicy("UsersDelete",
                       policy => policy.RequireClaim("UsersDelete", "true"));
                   // role policy
                   options.AddPolicy("RoleShow",
                       policy => policy.RequireClaim("RoleShow", "true"));
                   options.AddPolicy("RoleAdd",
                       policy => policy.RequireClaim("RoleAdd", "true"));
                   options.AddPolicy("RoleEdit",
                       policy => policy.RequireClaim("RoleEdit", "true"));
                   options.AddPolicy("RoleDelete",
                       policy => policy.RequireClaim("RoleDelete", "true"));
                   // Stock
                   options.AddPolicy("StockShow",
                       policy => policy.RequireClaim("StockShow", "true"));
                   options.AddPolicy("StockAdd",
                       policy => policy.RequireClaim("StockAdd", "true"));
                   options.AddPolicy("StockEdit",
                       policy => policy.RequireClaim("StockEdit", "true"));
                   options.AddPolicy("StockDelete",
                       policy => policy.RequireClaim("StockDelete", "true"));
                   //CartStatistics
                  
                   //Cart
                   options.AddPolicy("CartShow",
                       policy => policy.RequireClaim("CartShow", "true"));
                   options.AddPolicy("CartAdd",
                       policy => policy.RequireClaim("CartAdd", "true"));
                   options.AddPolicy("CartEdit",
                       policy => policy.RequireClaim("CartEdit", "true"));
                   options.AddPolicy("CartDelete",
                       policy => policy.RequireClaim("CartDelete", "true"));
                   //Categries
                   options.AddPolicy("CategriesShow",
                     policy => policy.RequireClaim("CategriesShow", "true"));
                   options.AddPolicy("CategriesAdd",
                       policy => policy.RequireClaim("CategriesAdd", "true"));
                   options.AddPolicy("CategriesEdit",
                       policy => policy.RequireClaim("CategriesEdit", "true"));
                   options.AddPolicy("CategriesDelete",
                       policy => policy.RequireClaim("CategriesDelete", "true"));
                   //BarItems
                   options.AddPolicy("BarItemsShow",
                   policy => policy.RequireClaim("BarItemsShow", "true"));
                   options.AddPolicy("BarItemsAdd",
                       policy => policy.RequireClaim("BarItemsAdd", "true"));
                   options.AddPolicy("BarItemsEdit",
                       policy => policy.RequireClaim("BarItemsEdit", "true"));
                   options.AddPolicy("BarItemsDelete",
                       policy => policy.RequireClaim("BarItemsDelete", "true"));


               });

          
        
        services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Products}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
               
                endpoints.MapRazorPages();
            });
        }
    }
}
