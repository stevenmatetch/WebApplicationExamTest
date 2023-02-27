using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationExamTest.Data;
using WebApplicationExamTest.Models;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace WebApplicationExamTest
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


          

            //services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
            //{
            //    options.Cookie.Name = "MyCookieAuth";
            //    options.LoginPath = "/Identity/Account/Login";
            //});


            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.AddRazorPages();

            //services.AddDbContext<WebApplicationExamTestContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("WebApplicationExamTestContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                              Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
                RequestPath = new PathString("/app-images")
            });
            //app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseSession();
            app.UseAuthorization();

            SeedData.Seed(userManager, roleManager);

            app.UseEndpoints(endpoints =>
            {
              
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
