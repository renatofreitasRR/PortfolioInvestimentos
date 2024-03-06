using PortfolioInvestimentos.Application.Services.Email;
using Quartz;

namespace PortfolioInvestimentos.Domain.Api.Configurations
{
    public static class QuartzConfiguration
    {
        public static void AddQuartzConfiguration(this IServiceCollection services)
        {
            services.AddQuartz(opt =>
            {
                var jobKey = JobKey.Create(nameof(DailyDueDateProductsEmailJob));

                opt
                .AddJob<DailyDueDateProductsEmailJob>(jobKey)
                .AddTrigger(
                    trigger => trigger
                    .ForJob(jobKey)
                    .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInHours(24)
                    .RepeatForever()));
            });

            services.AddQuartzHostedService();
        }
    }
}
