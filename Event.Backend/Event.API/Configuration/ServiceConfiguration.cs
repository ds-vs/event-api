namespace Event.API.Configuration
{
    public static class ServiceConfiguration
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddHostedService<EventStatusUpdateHostedService>();
            services.AddHostedService<EmailNotificationService>();
        }
    }
}
