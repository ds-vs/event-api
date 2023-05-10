namespace Event.API.Configuration
{
    public static class RepositoryConfiguration
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
