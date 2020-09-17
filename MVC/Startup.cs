using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using BLL.Repository;
using DAL.Context;
using DAL.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MVC
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        //install-package Microsoft.EntityFrameworkCore.Tools
        //install-package Microsoft.EntityFrameworkCore.Design
        //install-package Microsoft.EntityFrameworkCore.SqlServer
        //Package manager console açýlarak
        //add-migration tanýmlandýktan sonra bir isim verilerek migration oluþturulur.


        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(x=>x.EnableEndpointRouting=false);
            services.AddDbContext<AppDbContext>(options=>options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),b=>b.MigrationsAssembly("MVC")));
            //Scoped
            services.AddScoped<ICategoryService, CategoryRepository>();
            services.AddScoped<IProductService, ProductRepository>();
            services.AddScoped<IOrderService, OrderRepository>();

            //Identity
            services.AddIdentity<AppUser, AppUserRole>().AddEntityFrameworkStores<AppDbContext>();

            //Session
            services.AddSession(x =>
            {
                x.Cookie.Name = "Cart.Session";
                x.IdleTimeout = TimeSpan.FromMinutes(5);
            });

            //cookie
            services.ConfigureApplicationCookie(x =>
            {
                x.LoginPath = new PathString("/Member/Login");
                x.AccessDeniedPath = new PathString("/Member/Login");
                x.Cookie = new CookieBuilder
                {
                    Name = "LoginCookie",
                    Expiration = null
                };
                x.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseAuthorization();//kimlik doðrulama
            app.UseSession();//Session
            app.UseRouting();

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
               
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                 routes.MapRoute(
                 name: "default",
                 template: "{controller=Home}/{action=Index}/{id?}"
               );
                
            });
        }
    }
}
