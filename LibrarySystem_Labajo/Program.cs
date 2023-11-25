using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibrarySystem_Labajo.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibrarySystem_LabajoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibrarySystem_LabajoContext") ?? throw new InvalidOperationException("Connection string 'LibrarySystem_LabajoContext' not found.")));


/* ========== WHAT IS SESSION ========?  
-Is a way to store and retrieve user-specific information between multiple HTTP requests and responses.

-Is an ASP.NET Core scenario for storage of user data while the user browses a web app.

-uses a store maintained by the app to persist data across requests from a client

--uSED FOR STORING A DATA PERMANELTY WHILE THE PROGRAM IS RUNNING. It will dsiplay using ViewBag or ViewData(based Upon this program).
*/

//1. Add services memory cache
builder.Services.AddDistributedMemoryCache();

//4 Ad context-Accessor   (5 - Users View)
builder.Services.AddHttpContextAccessor();

//2 Assigning of Session Variable
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MVCLibrary.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});

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

//declaring Session Method
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
