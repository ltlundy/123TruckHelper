using _123TruckHelper.Services;

namespace _123TruckHelper.Ingestion
{
    public class NotificationManager
    {
        private static IServiceProvider _serviceProvider;

        public static void InitializeServiceProvider(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Initializing Notification Manager.");
            _serviceProvider = serviceProvider;
            Console.WriteLine("Initialized Notification Manager");
        }

        public static async void Manage()
        {
            while (true)
            {
                await Task.Delay(10000);

                var notificationService = _serviceProvider.GetRequiredService<INotificationService>();
                await notificationService.NotifyOfAvailableLoadsAsync();
            }
        }
    }
}
