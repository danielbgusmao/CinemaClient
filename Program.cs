using CinemaClient.Repositories;
using CinemaClient.Services;
using Flurl.Http.Configuration;
using System.Drawing.Text;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Flurl.Util;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

List<CultureInfo> supportedCultures = new List<CultureInfo>
{
    new CultureInfo("pt-BR"),
    new CultureInfo("en-US")
};

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IUserManagementService, UserManagementService>();
builder.Services.AddSingleton<IUserManagementRepository, UserManagementRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();

builder.Services.AddSingleton<IFilmeService, FilmeService>();
builder.Services.AddSingleton<IFilmeRepository, FilmeRepository>();

builder.Services.AddSingleton<ISalaService, SalaService>();
builder.Services.AddSingleton<ISalaRepository, SalaRepository>();

builder.Services.AddSingleton<ISessaoService, SessaoService>();
builder.Services.AddSingleton<ISessaoRepository, SessaoRepository>();

builder.Services.AddLocalization(opt => opt.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("pt-BR","pt-BR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


var app = builder.Build();


app.UseHttpsRedirection();
app.UseRequestLocalization(new RequestLocalizationOptions 
{ 
    DefaultRequestCulture = new RequestCulture("pt-BR","pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseRequestLocalization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserManagement}/{action=Index}/{id?}");

app.Run();
