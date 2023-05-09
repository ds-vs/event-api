namespace Event.API.BackgroundServices
{
    public class EventStatusUpdateHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;

        public EventStatusUpdateHostedService(IServiceScopeFactory scopeFactory, IConfiguration configuration)
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
                        var eventService = scope.ServiceProvider.GetRequiredService<IEventService>();

                        await eventService.UpdateEventStatusAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(
                        double.Parse(_configuration.GetSection("BackgroundServiceSettings:StatusUpdateDelayInMinutes").Value!)
                    ), stoppingToken
                );
            }
        }
    }
}
