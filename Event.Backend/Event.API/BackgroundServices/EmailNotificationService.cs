namespace Event.API.BackgroundServices
{
    public class EmailNotificationService: BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;

        public EmailNotificationService(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<EventDbContext>();
                        var emailService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                        var upcomingEvents = await context.Events
                            .Include(e => e.Accounts)
                            .Where(e => e.EventDate.Day == DateTime.Now.AddDays(1).Day)
                            .ToListAsync();

                        foreach (var upcomingEvent in upcomingEvents)
                        {
                            foreach (var subscriber in upcomingEvent.Accounts!)
                            {
                                await emailService.SendEmailAsync(
                                    subscriber.Email, 
                                    "Уведомление о предстоящем событии",
                                    $"{upcomingEvent.Title} начнется {upcomingEvent.EventDate.ToString("g")}."
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(
                        double.Parse(_configuration.GetSection("BackgroundServiceSettings:DelayNotificationInMinutes").Value!)
                    ), stoppingToken
                );
            }
        }
    }
}
