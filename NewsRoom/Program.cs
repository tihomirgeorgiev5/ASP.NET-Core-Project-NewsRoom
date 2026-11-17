using Google.Cloud.Diagnostics.AspNetCore3;
using Microsoft.AspNetCore.Builder;
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
using System;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// ---------------------------------------------------------------------
// Services (formerly ConfigureServices)
// ---------------------------------------------------------------------

var googleProjectId = configuration["GoogleCloud:ProjectId"];

if (!string.IsNullOrWhiteSpace(googleProjectId))
{
    builder.Services.AddGoogleDiagnosticsForAspNetCore(
        googleProjectId,
        configuration["GoogleCloud:ServiceName"],
        configuration["GoogleCloud:Version"]);
}

var connectionString = configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<NewsRoomDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDefaultIdentity<User>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<NewsRoomDbContext>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMemoryCache();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("bg"),
        new CultureInfo("en"),
        new CultureInfo("de")
    };

    options.DefaultRequestCulture = new RequestCulture("bg");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services
    .AddControllersWithViews(options =>
    {
        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
    })
    .AddMvcLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.AddScoped<IAppRepository, AppRepository>();

// Application services
builder.Services.AddTransient<IStatisticsService, StatisticsService>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<IJournalistService, JournalistService>();
builder.Services.AddTransient<IAboutService, AboutService>();

// ---------------------------------------------------------------------
// HTTP pipeline (formerly Configure)
// ---------------------------------------------------------------------

var app = builder.Build();

app.PrepareDatabase();

if (app.Environment.IsDevelopment())
{
    // DeveloperExceptionPage is enabled automatically in Development
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultAreaRoute();

app.MapControllerRoute(
    name: "News Details",
    pattern: "/News/Details/{id}/{information}",
    defaults: new
    {
        controller = typeof(NewsController).GetControllerName(),
        action = nameof(NewsController.Details)
    });

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();

// Required for WebApplicationFactory<Program> in the test project
public partial class Program { }
