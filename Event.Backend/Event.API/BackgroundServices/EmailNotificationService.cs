using Event.DAL;
using Event.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Event.API.BackgroundServices
{
    public class EmailNotificationService: BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public EmailNotificationService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
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

                        // Получения списка событий, которые начинаются на следующий день.
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
                                    "Уведомление о предстоящих событиях",
                                    $"{upcomingEvent.Title} начнется {upcomingEvent.EventDate.ToString("f")}."
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }
        }
    }
}
