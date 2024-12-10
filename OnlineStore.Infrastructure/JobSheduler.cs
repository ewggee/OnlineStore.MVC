using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Core.Images.Repositories;

namespace OnlineStore.Infrastructure
{
    /// <summary>
    /// Класс планирования задач.
    /// </summary>
    public class JobScheduler
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IServiceProvider _serviceProvider;

        public JobScheduler(
            IRecurringJobManager recurringJobManager, 
            IServiceProvider serviceProvider)
        {
            _recurringJobManager = recurringJobManager;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Запланировать задач.
        /// </summary>
        public void ScheduleJobs()
        {
            _recurringJobManager.AddOrUpdate(
                "Remove images with product_id is null",
                () => _serviceProvider.GetRequiredService<IImageRepository>().RemoveImagesWithProductIdNull(),
                Cron.Daily()
            );
        }
    }
}
