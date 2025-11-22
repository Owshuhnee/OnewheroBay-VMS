using Microsoft.EntityFrameworkCore;
using VMS.WebApp.Data;
using Npgsql;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Add an in-memory cache for Session to use
builder.Services.AddDistributedMemoryCache();

// Enable Session
builder.Services.AddSession();

// So we can inject HttpContextAccessor into _Layout.cshtml
builder.Services.AddHttpContextAccessor();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database Context for PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure Cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";      // <-- change if your login URL is different
        options.AccessDeniedPath = "/Account/AccessDenied"; // optional
    });

builder.Services.AddAuthorization();



var app = builder.Build();

// Configure Swagger (for API testing)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();   

app.Use(async (context, next) =>
{
    if (context.User?.Identity?.IsAuthenticated == true &&
        context.Session.GetInt32("UserID") == null)
    {
        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            context.User = new System.Security.Claims.ClaimsPrincipal(
            new System.Security.Claims.ClaimsIdentity());
    }

    await next();
});

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
