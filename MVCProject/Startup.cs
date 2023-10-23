using BLL.Interfaces;
using BLL.Repositaries;
using DAL.Contexts;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MVCProject.Mapper;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject
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
            services.AddControllersWithViews();
            services.AddDbContext<CompanyContext>(options =>
            {
                //options.UseSqlServer("server = .; database = CompanyForMvc; integrated security =true "); });
                options.UseSqlServer(Configuration.GetConnectionString("defaultconectionString"));
            },ServiceLifetime.Singleton);
            services.AddScoped<IdepartmentRepositary, DepartmentRepositary>();
            services.AddScoped<IEmployeeRepositry, EmployeeRepositary>();
            services.AddAutoMapper(M => M.AddProfile<EmployeeProfile>());
            services.AddAutoMapper(M => M.AddProfile<RegisterProfile>());
            services.AddIdentity<UserRegister, IdentityRole>(options => {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<CompanyContext>()
                .AddTokenProvider<DataProtectorTokenProvider<UserRegister>>(TokenOptions.DefaultProvider);

            //services.AddScoped<UserManager<UserRegister>, UserManager<UserRegister>>();
            //services.AddScoped<SignInManager<UserRegister>, SignInManager<UserRegister>>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "Acount/Login";
                options.AccessDeniedPath = "Home/Error";
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Acount}/{action=Register}/{id?}");
            });
        }
    }
}
