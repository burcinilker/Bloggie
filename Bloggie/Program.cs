global using Bloggie.Data;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using Bloggie.Helpers;
global using System.Text;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Bloggie.Areas.Admin.Models;
global using Bloggie.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Bloggie.Services;

var builder = WebApplication.CreateBuilder(args);

// paket olarak þunlar yüklenir,githubda paylaþýrken secret olan idlerimizin görünmemesi için 
//dotnet user-secrets set "Authentication:Google:ClientId" "896449691881-u1d6p1up8ln29lspv91dcr2aq31a0jt6.apps.googleusercontent.com" --project Bloggie
//dotnet user-secrets set "Authentication:Google:ClientSecret" "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" --project Bloggie

// Add services to the container.
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    //googleOptions.ClientId = "896449691881-u1d6p1up8ln29lspv91dcr2aq31a0jt6.apps.googleusercontent.com";
    //googleOptions.ClientSecret = "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";
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

//asenkron metot ekliyoruz seed data için 
await app.SeedDataAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
     );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
