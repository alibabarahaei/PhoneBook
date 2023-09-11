using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Application.Models;
using PhoneBook.Application.Services;
using PhoneBook.Domain.Models.User;
using PhoneBook.Infrastructure.EFCore.Context;
using PhoneBook.WorkerService.SendEmail;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {

        services.AddDbContext<PhoneBookDbContext>(option =>
        {
            option.UseSqlServer(hostContext.Configuration.GetConnectionString("SqlServer"));
        });

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<PhoneBookDbContext>();

        services.Configure<EmailInformationModel>(hostContext.Configuration.GetSection("Gmail"));
        services.Configure<ConfigWorkerServiceModel>(hostContext.Configuration.GetSection("ConfigWorkerService"));
       

        services.AddScoped<UserService>();
        services.AddSingleton<IMessageSender,MessageSender>();
        services.AddHostedService<Worker>();
    })
    .Build();


await host.RunAsync();
