namespace Event.API.Configuration
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Pgsql");

            services.AddDbContext<EventDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }
    }
}
