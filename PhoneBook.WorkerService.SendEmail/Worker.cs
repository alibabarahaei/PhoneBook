using Microsoft.Extensions.Options;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Application.Models;
using PhoneBook.Application.Services;

namespace PhoneBook.WorkerService.SendEmail
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageSender _messageSender;
        private readonly ConfigWorkerServiceModel _configworkerService;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IMessageSender messageSender, IOptions<ConfigWorkerServiceModel> configworkerService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _messageSender = messageSender;
            _configworkerService = configworkerService.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetRequiredService<UserService>();
                    var usersNotEmailConfirmed = userService.GetUsersNotEmailConfirmed();
                    if (usersNotEmailConfirmed.Count != 0)
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
                await Task.Delay(_configworkerService.Timer, stoppingToken);
            }
        }
    }
}