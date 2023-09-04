using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Application.Models;
using PhoneBook.Application.Security.Identity;
using PhoneBook.Application.Services;
using PhoneBook.Application.Utilities;
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


builder.Services.AddRazorPages();


var configurationSection = builder.Configuration;

#region Config Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    })
    .AddEntityFrameworkStores<PhoneBookDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<PersianIdentityErrorDescriber>();
#endregion


#region ConfigCookie


builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(10);
    options.AccessDeniedPath = configurationSection.GetSection("Route:AccessDenied").Value ;
    options.Cookie.Name = configurationSection.GetSection("Cookie:Name").Value;
    options.Cookie.HttpOnly = true;
    options.LoginPath = configurationSection.GetSection("Route:Login").Value;
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});

#endregion




#region MyRegion

builder.Services.Configure<EmailInformationModel>(builder.Configuration.GetSection("Gmail"));
builder.Services.Configure<StorePathModel>(builder.Configuration.GetSection("StorePath"));

#endregion






builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));


#region Services

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IMessageSender, MessageSender>();

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
