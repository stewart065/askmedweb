using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using login.Entities;
using login.Identity;
using AutoMapper;
using login.Profiles;
using login.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<dataofmedContext>();

builder.Services.AddDbContext<sampleappContext>(options => 
    options.UseMySql("server=localhost;database=sampleapp;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.21-mariadb")));

builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseMySql("server=localhost;database=sampleapp;user=root;password=;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
{
    option.SignIn.RequireConfirmedAccount = false;
    option.SignIn.RequireConfirmedEmail = false;
    option.SignIn.RequireConfirmedPhoneNumber = false;
    option.User.RequireUniqueEmail = true;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireLowercase = false;
    option.Password.RequiredUniqueChars = 0;
    option.Password.RequireDigit = false;
}).AddEntityFrameworkStores<ApplicationIdentityDbContext>();

// Configure AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<SampleAppProfile>();
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton<IMapper>(mapper);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

// Add CORS policy to allow requests from your Flutter app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutterApp",
        builder =>
        {
             builder.WithOrigins("http://localhost:45336", "http://localhost:52252") // Replace with your Flutter app's origins
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Show detailed errors in development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Enable CORS with the specified policy
app.UseCors("AllowFlutterApp");

app.UseAuthentication();
app.UseAuthorization();

// Custom error handling middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        // Log the exception (use a logging framework in production)
        var logger = context.RequestServices.GetService<ILogger<Program>>();
        logger?.LogError(ex, "An unhandled exception occurred.");
        
        // Return a generic error response
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
