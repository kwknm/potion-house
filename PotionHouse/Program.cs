using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PotionHouse.DataAccess;
using PotionHouse.DataAccess.Entities;
using PotionHouse.DataAccess.Repositories;
using PotionHouse.DataAccess.Repositories.Abstractions;
using PotionHouse.Services;
using PotionHouse.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(x =>
{
    x.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("admin", p => p.RequireRole("admin"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    
    if (builder.Environment.IsDevelopment())
    {
        x.Password.RequireDigit = false;
        x.Password.RequiredLength = 3;
        x.Password.RequiredUniqueChars = 0;
        x.Password.RequireLowercase = false;
        x.Password.RequireNonAlphanumeric = false;
        x.Password.RequireUppercase = false;
    }
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.Cookie.Name = "ph.id";
    x.Cookie.HttpOnly = true;
    x.LoginPath = "/account/login";
    x.LogoutPath = "/account/logout";
    x.AccessDeniedPath = "/account/denied";
    x.SlidingExpiration = true;
    x.ReturnUrlParameter = "returnUrl";
});

builder.Services.AddRouting(x => x.LowercaseUrls = true);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

builder.Services.AddScoped<IIngredientsService, IngredientsService>();
builder.Services.AddScoped<IFilesService, FilesService>();
builder.Services.AddScoped<IPotionsService, PotionsService>();
builder.Services.AddScoped<IRarityService, RarityService>();

// Add DA
builder.Services.AddDataAccess();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
