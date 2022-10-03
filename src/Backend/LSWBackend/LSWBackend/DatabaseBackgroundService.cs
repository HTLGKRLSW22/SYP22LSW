using LSWDbLib;

namespace LSWBackend
{
    public class DatabaseBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseBackgroundService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;


        protected override Task ExecuteAsync(CancellationToken stoppingToken) {
            using IServiceScope scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<LSWContext>();
            db.Database.EnsureCreated();
            return Task.Run(() => { });
        }
    }
}
