using FeedbackService.Core.Interfaces.Repositories;
using FeedbackService.Core.Interfaces.Services;
using FeedbackService.Core.Services;
using FeedbackService.Infrastructure.Context;
using FeedbackService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FeedbackService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddDbContext<FeedbackDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
            services.AddScoped<IFeedbackService, FeedbacksService>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();

            return services;
        }
    }
}
