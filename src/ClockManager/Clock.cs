using System;
using System.Threading.Tasks;

namespace ClockManager
{
    public class Clock : IClock
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTimeOffset OffsetNow => DateTimeOffset.Now;
        public DateTimeOffset OffsetUtcNow => DateTimeOffset.UtcNow;

        public void Sleep(TimeSpan span)
        {
            ClockAwaiter.AwaitAsync(this, span).Wait();
        }

        public Task SleepAsync(TimeSpan span)
        {
            return ClockAwaiter.AwaitAsync(this, span);
        }
    }
}
