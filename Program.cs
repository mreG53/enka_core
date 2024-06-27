using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using stokEnka.Models;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Reflection;
using stokEnka.Resources;
using stokEnka.Services;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Yerelleþtirme servislerini ekle
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            var assemblyName = new AssemblyName(typeof(ApplicationResource).GetTypeInfo().Assembly.FullName);
            return factory.Create("ApplicationResource", assemblyName.Name);
        };
    });

builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    var cultures = new List<CultureInfo>()
    {
        new CultureInfo("tr-TR"),
        new CultureInfo("en-US"),
    };
    opt.DefaultRequestCulture = new RequestCulture(new CultureInfo("tr-TR"));
    opt.SupportedCultures = cultures;
    opt.SupportedUICultures = cultures;

    opt.RequestCultureProviders = new List<IRequestCultureProvider>()
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    };
});

// MVC ve yerelleþtirme için servisler ekle
builder.Services.AddControllersWithViews();

// LocalizationService servisini ekleyin
builder.Services.AddSingleton<LocalizationService>();

// hash
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// db
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
if (options != null)
    app.UseRequestLocalization(options.Value);

// HTTP istek iþleyicisini yapýlandýr
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Varsayýlan rota
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
