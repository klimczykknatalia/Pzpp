using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;

using Pzpp.Data;
using Pzpp.Data.Entities;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// 1) DbContext SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) Identity
builder.Services.AddDefaultIdentity<User>(opts => opts.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// 3) MVC + Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Ja�niejsza obs�uga b��d�w i HTTPS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// domy�lny routing
 app.MapControllerRoute(
     name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}");
 app.MapRazorPages();


// domy�lny routing kieruje teraz na ContactController.Index
// app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Contact}/{action=Index}/{id?}");





app.Run();
