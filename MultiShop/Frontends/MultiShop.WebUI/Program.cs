using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot içindeki statik dosyalarý sunar

// Eðer `online-shop-website-template` dizini wwwroot dýþýnda ise:
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "online-shop-website-template")),
    RequestPath = "/online-shop-website-template" // URL'deki yol
});

// Ana sayfa `/` isteði geldiðinde, `DefaultController`'ýn `Index` aksiyonuna yönlendirme
app.MapGet("/", () =>
{
    return Results.Redirect("/Default/Index");
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

 //Default route için controller ve action ayarlama
app.MapControllerRoute(
  name: "default",
pattern: "{controller=Default}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "admin",
        areaName: "Admin",
        pattern: "Admin/{controller=FeatureSlider}/{action=Index}/{id?}"
    );
});


app.Run();
