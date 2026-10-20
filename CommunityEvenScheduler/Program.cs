
using CommunityEventScheduler.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' not found.");

// Configure SQL Server database.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity with user accounts and roles.
builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
        options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add MVC services.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ------------------------------------------------------
// ADMIN ROLE SETUP
// Create the Admin role and assign it to the admin user.
// This setup is for local development.
// ------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        // Get the Identity role and user managers.
        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();

        // Define the administrator role and email.
        const string adminRole = "Admin";
        const string adminEmail = "francis.sengele@gmail.com";

        // Create the Admin role if it does not exist.
        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            var roleResult = await roleManager.CreateAsync(
                new IdentityRole(adminRole));

            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException(
                    "Could not create the Admin role.");
            }
        }

        // Find the existing administrator account.
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        // Assign the Admin role if the account exists.
        if (adminUser != null &&
            !await userManager.IsInRoleAsync(adminUser, adminRole))
        {
            var result = await userManager.AddToRoleAsync(
                adminUser, adminRole);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    "Could not assign the Admin role.");
            }
        }
    }
}

// ------------------------------------------------------
// HTTP REQUEST PIPELINE
// ------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Enable routing.
app.UseRouting();

// Check who the user is before checking permissions.
app.UseAuthentication();
app.UseAuthorization();

// Enable static files.
app.MapStaticAssets();

// Configure the default MVC route.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Enable Identity Razor Pages.
app.MapRazorPages()
    .WithStaticAssets();

app.Run();
