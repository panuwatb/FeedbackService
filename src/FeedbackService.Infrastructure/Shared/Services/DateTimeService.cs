using FeedbackService.Core.Interfaces.Services;
using System;

namespace FeedbackService.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
