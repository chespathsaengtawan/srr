
using Microsoft.EntityFrameworkCore;
using Auth0.AspNetCore.Authentication;
using srr.Support;
using srr.Contexts;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

builder.Services.AddDbContext<SRRContext>(options =>
    options.UseSqlServer(connectionString));

if (builder.Configuration["Auth0:Domain"] == null || builder.Configuration["Auth0:ClientId"] == null)
{
    throw new InvalidOperationException("Auth0 configuration is missing.");
}
// Add Auth0 authentication
var auth0Domain = builder.Configuration["Auth0:Domain"];
var auth0ClientId = builder.Configuration["Auth0:ClientId"];

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = auth0Domain!;
    options.ClientId = auth0ClientId!;
    options.CallbackPath = "/callback";
});

// Configure the HTTP request pipeline.
builder.Services.ConfigureSameSiteNoneCookies();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseCookiePolicy();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}").WithStaticAssets();
//
//    endpoints.MapDefaultControllerRoute();
//
//});

app.Run();
