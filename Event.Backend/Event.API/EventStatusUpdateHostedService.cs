using Event.Service.Interfaces;

namespace Event.API
{
    public class EventStatusUpdateHostedService : BackgroundService
    {
        private readonly IHostApplicationLifetime _lifetime;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly int _statusUpdateTimeInMilliseconds = 60_000;

        public EventStatusUpdateHostedService(IHostApplicationLifetime lifetime,
                                              IServiceScopeFactory scopeFactory)
        {
            _lifetime = lifetime;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!await WaitForAppStartup(_lifetime, stoppingToken))
                return;

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

        private static async Task<bool> WaitForAppStartup(IHostApplicationLifetime lifetime,
                                                          CancellationToken stoppingToken)
        {
            // Создание TaskCompletionSource для ApplicationStarted.
            var startedSource = new TaskCompletionSource();
            using var firstRegistration = lifetime.ApplicationStarted.Register(startedSource.SetResult);

            // Создание TaskCompletionSource для stoppingToken.
            var cancelledSource = new TaskCompletionSource();
            using var secondRegistration = stoppingToken.Register(cancelledSource.SetResult);

            // Ожидание любого из событий запуска или запроса на остановку.
            Task completedTask = await Task.WhenAny(startedSource.Task, cancelledSource.Task).ConfigureAwait(false);

            // Если завершилась задача ApplicationStarted, возвращаем true, иначе false.
            return completedTask == startedSource.Task;
        }
    }
}
