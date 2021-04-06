using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClockManager
{
    public class ClockAwaiter
    {
        private IClock _clock;
        private DateTime _beginTime;

        private ClockAwaiter(IClock clock)
        {
            _clock = clock;
        }

        public static Task AwaitAsync(TimeSpan span)
        {
            return AwaitAsync(new Clock(), span);
        }

        public static Task AwaitAsync(IClock clock, TimeSpan span)
        {
            var waitStep = 50;

            if (waitStep < span.TotalMilliseconds)
            {
                waitStep = Convert.ToInt32(span.TotalMilliseconds);
            }

            return Task.Run(() =>
            {
                var awaiter = new ClockAwaiter(clock);
                awaiter.Start();

                while (awaiter.Elapsed < span)
                {
                    Thread.Sleep(waitStep);
                }
            });
        }

        public void Start()
        {
            _beginTime = _clock.Now;
        }

        public TimeSpan Elapsed => _clock.Now - _beginTime;
    }
}
