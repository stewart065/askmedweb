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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<dataofmedContext>();

builder.Services.AddDbContext<sampleappContext>(options => 
        options.UseMySql("server=localhost;database=sampleapp;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.21-mariadb")));



builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseMySql("server=localhost;database=sampleapp;user=root;password=;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option =>{
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

// builder.Services.ConfigureApplicationCookie(option => {
//     option.LoginPath = "/Account/Login";
//     option.AccessDeniedPath = "/Account/Login";
// });
builder.WebHost.UseKestrel(options =>{

});
builder.WebHost.UseUrls("https://localhost:7070","https://192.168.101.11:7070");
builder.Services.AddCors( options => {

    options.AddPolicy("AllowAllOrigins",b =>{
        b.AllowAnyOrigin().
                AllowAnyMethod().
                AllowAnyHeader();
    });
});
// Configure endpoints

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<SampleAppProfile>();
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton<IMapper>(mapper);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseCors();

app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
