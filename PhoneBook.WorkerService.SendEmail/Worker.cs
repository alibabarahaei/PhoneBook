using Microsoft.VisualBasic;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Application.Services;
using System.IO;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Domain.Models.User;
using PhoneBook.Domain.InterfaceRepositories.Base;
using PhoneBook.Domain.Models.Contacts;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Infrastructure.EFCore.Context;

namespace PhoneBook.WorkerService.SendEmail
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageSender _messageSender;
        private readonly IServiceProvider _serviceProvider;


        public Worker(ILogger<Worker> logger, IMessageSender messageSender, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _messageSender = messageSender;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                using (var scope = _serviceProvider.CreateScope())
                {

                    var userService = scope.ServiceProvider.GetRequiredService<UserService>();
                    var emailSender = scope.ServiceProvider.GetRequiredService<MessageSender>();

                    //var nana = await ctx.IsUserNameInUseAsync("alibabarahaei4");
                    //var x = ctx.Users.AsQueryable().Where(u => u.EmailConfirmed == false).ToList();
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                }



                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}