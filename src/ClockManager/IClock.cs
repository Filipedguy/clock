using System;
using System.Threading.Tasks;

namespace ClockManager
{
    public interface IClock
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
        DateTimeOffset OffsetNow { get; }
        DateTimeOffset OffsetUtcNow { get; }

        Task SleepAsync(TimeSpan span);
        void Sleep(TimeSpan span);
    }
}
