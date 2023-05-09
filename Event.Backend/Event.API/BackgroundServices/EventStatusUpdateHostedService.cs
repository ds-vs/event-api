using Event.Service.Interfaces;

namespace Event.API.BackgroundServices
{
    public class EventStatusUpdateHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly int _statusUpdateTimeInMilliseconds = 60_000;

        public EventStatusUpdateHostedService(IServiceScopeFactory scopeFactory)
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
                        var eventService = scope.ServiceProvider.GetRequiredService<IEventService>();

                        await eventService.UpdateEventStatusAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }

                await Task.Delay(_statusUpdateTimeInMilliseconds, stoppingToken);
            }
        }
    }
}
