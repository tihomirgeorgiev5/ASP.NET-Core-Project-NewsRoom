using Google.Cloud.Diagnostics.AspNet;
using Google.Cloud.Diagnostics.AspNetCore3;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsRoom.Controllers;
using NewsRoom.Data;
using NewsRoom.Data.Models;
using NewsRoom.Infrastructure.Data.Repositories;
using NewsRoom.Infrastructure.Extensions;
using NewsRoom.Services.About;
using NewsRoom.Services.Journalists;
using NewsRoom.Services.News;
using NewsRoom.Services.Statistics;
using System.Globalization;
using System.Web.Http;

namespace NewsRoom
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;


        public IConfiguration Configuration { get; }

        private static void Register(HttpConfiguration config)
        {
            // Add a catch all for the uncaught exceptions.
            // Replace ProjectId with your Google Cloud Project ID.
            // Replace Service with a name or identifier for the service.
            // Replace Version with a version for the service.
            config.Services.Add(typeof(System.Web.Http.ExceptionHandling.IExceptionLogger),
                ErrorReportingExceptionLogger.Create("valiant-healer-403410", "My First Project", "772578124567"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGoogleDiagnosticsForAspNetCore("valiant-healer-403410", "My First Project", "772578124567");

            services
                .AddDbContext<NewsRoomDbContext>(options =>
                   options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddDatabaseDeveloperPageExceptionFilter();
             
            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<NewsRoomDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services.AddMemoryCache();

            services.AddLocalization(options
                => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new CultureInfo[]
                {
                    new CultureInfo("bg"),
                    new CultureInfo("en"),
                    new CultureInfo("de")
                };

                options.DefaultRequestCulture = new RequestCulture("bg");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services
                .AddControllersWithViews(options =>
                {
                    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                })
                .AddMvcLocalization(LanguageViewLocationExpanderFormat.Suffix);

            services.AddScoped<IAppRepository, AppRepository>();
            // Application services
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IJournalistService, JournalistService>();
            services.AddTransient<IAboutService, AboutService>();
            
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseRequestLocalization()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultAreaRoute();
                    endpoints.MapControllerRoute(
                        name: "News Details",
                        pattern: "/News/Details/{id}/{information}",
                        defaults: new 
                        {
                            controller = typeof(NewsController).GetControllerName(),
                            action = nameof(NewsController.Details)
                        });

                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });

          

        }

    }
}
