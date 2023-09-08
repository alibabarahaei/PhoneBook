using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Application.Security.Identity;
using PhoneBook.Application.Services;
using PhoneBook.Domain.InterfaceRepositories.Base;
using PhoneBook.Domain.Models.User;
using PhoneBook.Infrastructure.EFCore.Context;
using PhoneBook.Infrastructure.EFCore.Repository.Base;
using PhoneBook.WorkerService.SendEmail;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {

        services.AddDbContext<PhoneBookDbContext>(option =>
        {
            option.UseSqlServer(hostContext.Configuration.GetConnectionString("SqlServer"));
        });
        
       


        services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<PhoneBookDbContext>();


        services.AddScoped<UserService>();


        //services.AddSingleton(typeof(IGenericRepository<ApplicationUser>), typeof(GenericRepository<ApplicationUser>));
        services.AddScoped<MessageSender>();
        
        services.AddHostedService<Worker>();
    })
    .Build();












await host.RunAsync();
