using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SeesionASPCore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddSession();   // this is for creating a new session.

builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(10); });    //for creating session and setting TimeOut also.
//It's using the AddSession method to register session-related services and configure session behavior.
//It uses the AddSingleton method to register a service in the application's dependency injection container.
//Specifically, it registers the IHttpContextAccessor interface with the HttpContextAccessor implementation.

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();          //this line of code is for viewing session data with using action methods.
//IHttpContextAccessor: This is an interface provided by ASP.NET Core that allows access to the current HttpContext within your application.
//It's typically used to access the HTTP context information, such as request and response details.
//HttpContextAccessor: This is the concrete implementation of the IHttpContextAccessor interface.
//It's provided by ASP.NET Core and allows you to access the current HttpContext within your application.
//By registering IHttpContextAccessor with the HttpContextAccessor implementation as a singleton,
//you're ensuring that there's a single shared instance of HttpContextAccessor throughout your application.
//This can be useful when you need to access the current HTTP context from various parts of your application, such as in services or middleware.


//These 3 lines of code are used for Connecting to SqlServer DB purpose :
var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<DbFirstApprochContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseSession();    //for using session.


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
