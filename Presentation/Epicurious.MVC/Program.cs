using Epicurious.Infrastructure;
using Epicurious.MVC;
using Epicurious.Persistence;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices();
await builder.Services.AddWebServicesAsync();

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

app.UseSession();

app.UseNToastNotify();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "UpdateRecipe",
//    pattern: "recipe/update/{id}"
//    //defaults: new { controller = "Recipe", action = "UpdateRecipe" }
//);

//app.MapControllerRoute(
//    name: "auth",
//    pattern: "{controller=Auth}/{action=Recipes}/{id?}");

//app.MapControllerRoute(
//    name: "admin",
//    pattern: "{controller=Admin}/{action=ReviewRecipe}/{id?}");

app.MapControllerRoute(
    name: "AddCommentRoute",
    pattern: "comment/AddComment/{id}",
    defaults: new { controller = "Comment", action = "AddComment" }
);

app.Run();
