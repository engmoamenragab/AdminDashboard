using ADB.BL.Interfaces;
using ADB.BL.Mapper;
using ADB.BL.Repository;
using ADB.DAL.Database;
using ADB.DAL.Extends;
using ADB.PL.Languages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ADB.PL
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
            // services.AddLocalization(opt => opt.ResourcesPath = "");

            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                })
                .AddNewtonsoftJson(opt =>
                {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddDbContextPool<AdminDashboardContext>(options
                => options.UseSqlServer(Configuration.GetConnectionString("AdminDashboardDbConnection")));

            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            // Take one Instance for for all client for each operation (Default)
            //services.AddTransient<IDepartmentRepo, DepartmentRepo>();

            // Take one Instance for one page for one client for every operation
            services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<ICountryRepo, CountryRepo>();
            services.AddScoped<ICityRepo, CityRepo>();
            services.AddScoped<IDistrictRepo, DistrictRepo>();

            // Take only one Instance for all clients
            //services.AddSingletone<IDepartmentRepo, DepartmentRepo>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // We can add a default user settings also.
                options.User.RequireUniqueEmail = true; // True by default

                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<AdminDashboardContext>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

            //options =>
            //{
            //    options.LoginPath = new PathString("/Account/Signin");
            //    options.AccessDeniedPath = new PathString("/Account/Signin");
            //});

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Signin";
                options.AccessDeniedPath = "/Account/Signin";
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
            }

            var supportedCultures = new[] {
                  new CultureInfo("ar-EG"),
                  new CultureInfo("en-US"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                }
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Signin}/{id?}");
            });
        }
    }
}
