using P_U3.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

var authentication = builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);

authentication.AddCookie(options =>
{
    options.LoginPath = "/Access/Login";
    //options.LogoutPath = "/Access/Logout";
    //options.AccessDeniedPath = "/Home/Restringido";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});

var configuration = builder.Configuration;


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BddCitasMedicasContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("mssql"));
});

var app = builder.Build();

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
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();