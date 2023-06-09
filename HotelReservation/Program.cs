using HotelReservation;
using Stripe;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("coneccion")));
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddScoped<JwtService>();
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
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
    { 
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
 
        endpoints.MapControllerRoute(
            name: "home",
            pattern: "{controller=Home}/{action=HomePage}/{id?}");

        // endpoints.MapControllerRoute(
        //     name: "home",
        //     pattern: "{controller=Account}/{action=Login}/{username}/{password}");

        // endpoints.MapControllerRoute(
        //     name: "register", 
        //     pattern: "{controller=Account}/{action=Register}/{username}/{email}/{password}")
}); 

app.Run();
