using System;
using System.Threading.Tasks;

namespace ClockManager
{
    public static class ClockExtensions
    {
        public static void Sleep(this IClock clock, int milliseconds)
        {
            clock.Sleep(TimeSpan.FromMilliseconds(milliseconds));
        }

        public static Task SleepAsync(this IClock clock, int milliseconds)
        {
            return clock.SleepAsync(TimeSpan.FromMilliseconds(milliseconds));
        }
    }
}
