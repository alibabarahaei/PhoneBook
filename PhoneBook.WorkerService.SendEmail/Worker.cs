using Microsoft.VisualBasic;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Application.Services;
using System.IO;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Domain.Models.User;
using PhoneBook.Domain.InterfaceRepositories.Base;
using PhoneBook.Domain.Models.Contacts;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Application.Models;
using PhoneBook.Infrastructure.EFCore.Context;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace PhoneBook.WorkerService.SendEmail
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        //private readonly EmailInformationModel? _EmailInformationOptions;
        private readonly IMessageSender _messageSender;
        

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IMessageSender messageSender)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _messageSender = messageSender;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                using (var scope = _serviceProvider.CreateScope())
                {

                    var userService = scope.ServiceProvider.GetRequiredService<UserService>();
                    var usersNotEmailConfirmed = userService.GetUsersNotEmailConfirmed();
                    if (usersNotEmailConfirmed != null)
                    {
                        var listEmailSendConfirmation = new List<string>();

                        foreach (var u in usersNotEmailConfirmed)
                        {
                            _messageSender.SendEmail(u.Email, "Confirm your email",
                                $"Please confirm your account by <a href='{u.UrlEmailConfirmed}'>clicking here</a>.",
                                true);
                            listEmailSendConfirmation.Add(u.Email);
                        }

                        await userService.DeleteUrlEmailConfirmationWithEmailAsync(listEmailSendConfirmation);
                    }
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                }



                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}