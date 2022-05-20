using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DAL;
using Entities.Users;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("ManagementConnection") ?? 
        throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ManagementDbContext>(options =>
    options.UseSqlServer(connectionString)
    .UseLazyLoadingProxies());

builder.Services.AddIdentity<Customer, Role>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ManagementDbContext>()
    .AddDefaultUI()
    .AddTokenProvider<DataProtectorTokenProvider<Customer>>(TokenOptions.DefaultProvider);


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options => //CookieAuthenticationOptions
               {
                   options.LoginPath = new PathString("/Account/Login");
               });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator",
        authBuilder =>
        {
            authBuilder.RequireRole("Administrator");
        });
    options.AddPolicy("Manager",
        authBuilder =>
        {
            authBuilder.RequireRole("Administrator,Manager");
        });
});

var app = builder.Build();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
