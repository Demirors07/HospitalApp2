using Microsoft.EntityFrameworkCore;  // Namespace required for Entity Framework Core
using HospitalApp.Models;  // Including the models defined in HospitalApp
using Microsoft.AspNetCore.Identity;  // Namespace required for ASP.NET Core Identity

var builder = WebApplication.CreateBuilder(args);  // Initializes the application configuration

builder.Services.AddControllersWithViews();  // Adds services for MVC controllers and views

// Configures the database connection for StoreDbContext (using SQLite)
builder.Services.AddDbContext<StoreDbContext>(opts => {
    opts.UseSqlite(  // Using SQLite as the database provider
        builder.Configuration["ConnectionStrings:HospitalsStoreDBConnection"]);
});

// Configures the database connection for AppIdentityDbContext (using SQLite)
builder.Services.AddDbContext<AppIdentityDbContext>(opts => 
    opts.UseSqlite(  // Using SQLite for Identity database
        builder.Configuration["ConnectionStrings:IdentityDBConnection"]));

// Configures ASP.NET Core Identity for user authentication and role management
builder.Services.AddIdentity<IdentityUser, IdentityRole>()  // Defines Identity for user and role classes
    .AddRoles<IdentityRole>()  // Adds role management
    .AddEntityFrameworkStores<AppIdentityDbContext>();  // Uses AppIdentityDbContext for Identity database operations

var app = builder.Build();  // Builds the application

app.UseStaticFiles();  // Allows serving static files (CSS, JS, images, etc.)
app.MapDefaultControllerRoute();  // Defines the default route for MVC controllers

app.UseAuthentication();  // Middleware to handle user authentication
app.UseAuthorization();  // Middleware to handle user authorization

// Seeds the database with initial data when the application starts
SeedData.EnsurePopulated(app);  // Adds initial data to the main application database
IdentitySeedData.EnsurePopulated(app);  // Adds initial data to the Identity database (users, roles, etc.)

app.Run();  // Runs the application
