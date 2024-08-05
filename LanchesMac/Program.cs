using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LanchesMac.Services;
using LanchesMac.Areas.Admin.Services;
using ReflectionIT.Mvc.Paging;


var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("Mysql")!;
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connection, ServerVersion.AutoDetect(connection))
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();


builder.Services.Configure<ConfigurationImages>(builder.Configuration.GetSection("ConfigurationPastaImagens"));

builder.Services.AddTransient<ISnackRepository, SnackRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped<ServicesSalesReport>();
builder.Services.AddScoped<ServicesSalesChart>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", politica =>
    {
        politica.RequireRole("Admin");
    });
});


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => CartPurchase.GetCart(sp));


builder.Services.AddControllersWithViews();


builder.Services.AddPaging(options =>
{
    options.ViewName = "Bootstrap4";
    options.PageParameterName = "pageindex";
});


builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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

CriarPerfisUsuarios(app);    

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "filterCategory",
    pattern: "Snack/{action}/{category?}",
    defaults: new { controller = "Snack", action = "List" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();


void CriarPerfisUsuarios(WebApplication app)
{
    var scoopedFactory = app.Services.GetService<IServiceScopeFactory>()!;
    using(var scope = scoopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>()!;
        service.SeedRoles();
        service.SeedUsers();
    }
}