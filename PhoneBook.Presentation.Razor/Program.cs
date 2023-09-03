using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Application.Security.Identity;
using PhoneBook.Application.Services;
using PhoneBook.Domain.InterfaceRepositories.Base;
using PhoneBook.Domain.Models.User;
using PhoneBook.Infrastructure.EFCore.Context;
using PhoneBook.Infrastructure.EFCore.Repository.Base;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Westwind.AspNetCore.LiveReload;

var builder = WebApplication.CreateBuilder(args);





#region AddLiveReload



builder.Services.AddLiveReload(config =>
{
    // optional - use config instead
    //config.LiveReloadEnabled = true;
    //config.FolderToMonitor = Path.GetFullname(Path.Combine(Env.ContentRootPath,"..")) ;
});


#endregion


// Add services to the container.
builder.Services.AddRazorPages();








#region Config Identity


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);


    })
    .AddEntityFrameworkStores<PhoneBookDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<PersianIdentityErrorDescriber>();


#endregion


#region ConfigCookie


builder.Services.ConfigureApplicationCookie(options =>
{
   
    options.ExpireTimeSpan = TimeSpan.FromDays(10);
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.Name = "YourAppCookieName";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/Identity/Account/Login";
    // ReturnUrlParameter requires 
    //using Microsoft.AspNetCore.Authentication.Cookies;
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});

#endregion





builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));


#region Services

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContactService, ContactService>();


#endregion

#region Config Database
builder.Services.AddDbContext<PhoneBookDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseLiveReload();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
