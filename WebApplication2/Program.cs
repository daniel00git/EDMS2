/*// Ensure you have the correct namespace for your ApplicationDbContext
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
*/
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using Microsoft.Graph;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Win32;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());

// Add Azure AD authentication
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
   /* .EnableTokenAcquisitionToCallDownstreamApi()
    .AddInMemoryTokenCaches()*/; 



// Configure the token validation to use the "name" claim
builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
  options.TokenValidationParameters.NameClaimType = "name";

});

builder.Services.AddAuthorization(options =>
{
  options.FallbackPolicy = options.DefaultPolicy;
});


builder.Services.AddHostedService<ProtocolHandlerRegistrationService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "logout",
    pattern: "{controller=Account}/{action=Logout}");

// Route for Admin/EditDocumentType
app.MapControllerRoute(
name: "editdocumenttype",
    pattern: "{controller = Admin}/{action = EditDocumentType}/{id}");

// Route for Home/ShortcutCreator
app.MapControllerRoute(
       name: "shortcutcreator",
       pattern: "{controller = Home}/{action = ShortcutCreator}/{id}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id}");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "createfolder",
        pattern: "{controller=Home}/{action=CreateFolder}/{id}");
});

/*app.MapControllerRoute(
    name: "SearchDocument",
    pattern: "{controller=Home}/{action=SearchDocumet}/{id}");*/

app.Run();

/*public class Startup
{
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        RegisterProtocolHandler();
    }

    private void RegisterProtocolHandler()
    {
        //
    }
}*/



/*public void RegisterProtocolHandler()
{
    var key = Registry.ClassesRoot.CreateSubKey("myapp");
    key.SetValue("", "URL:myapp Protocol"); key.SetValue("URL Protocol", "");
    var commandKey = key.CreateSubKey("shell");
    var openKey = commandKey.CreateSubKey("open");
    var commandKey2 = openKey.CreateSubKey("command");

    commandKey2.SetValue("", $"\"C:\\Path\\To\\Application\\Executable.exe\" \"%1\"");
}*/

