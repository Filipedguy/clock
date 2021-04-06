using FluentAssertions;
using System;
using System.Diagnostics;
using Xunit;

namespace ClockManager.UnitTests
{
    public class ClockTests
    {
        private Clock _subject;
        private int _acceptableRangeInMilliseconds = 15;

        public ClockTests()
        {
            _subject = new Clock();
        }

        [Fact]
        public void Clock_Now_ShouldBeWithinAnAcceptableRange()
        {
            var expectedValue = DateTime.Now;

            var value = _subject.Now;

            (value - expectedValue).TotalMilliseconds.Should().BeApproximately(0, _acceptableRangeInMilliseconds);
        }

        [Fact]
        public void Clock_UtcNow_ShouldBeWithinAnAcceptableRange()
        {
            var expectedValue = DateTime.UtcNow;

            var value = _subject.UtcNow;

            (value - expectedValue).TotalMilliseconds.Should().BeApproximately(0, _acceptableRangeInMilliseconds);
        }

        [Fact]
        public void Clock_OffsetNow_ShouldBeWithinAnAcceptableRange()
        {
            var expectedValue = DateTimeOffset.Now;

            var value = _subject.OffsetNow;

            (value - expectedValue).TotalMilliseconds.Should().BeApproximately(0, _acceptableRangeInMilliseconds);
        }

        [Fact]
        public void Clock_OffsetUtcNow_ShouldBeWithinAnAcceptableRange()
        {
            var expectedValue = DateTimeOffset.UtcNow;

            var value = _subject.OffsetUtcNow;

            (value - expectedValue).TotalMilliseconds.Should().BeApproximately(0, _acceptableRangeInMilliseconds);
        }

        [Fact]
        public void Clock_Sleep_AwaitedTimeShouldBeWithinAnAcceptableRange()
        {
            var watch = new Stopwatch();
            var sleepTime = 1000;

            watch.Start();
            _subject.Sleep(sleepTime);
            watch.Stop();

            Convert.ToDouble(watch.ElapsedMilliseconds - sleepTime).Should().BeApproximately(0, _acceptableRangeInMilliseconds);
        }

        [Fact]
        public async void Clock_SleepAsync_AwaitedTimeShouldBeWithinAnAcceptableRange()
        {
            var watch = new Stopwatch();
            var sleepTime = 1000;

            watch.Start();
            await _subject.SleepAsync(sleepTime);
            watch.Stop();

            Convert.ToDouble(watch.ElapsedMilliseconds - sleepTime).Should().BeApproximately(0, _acceptableRangeInMilliseconds);
        }

        [Fact]
        public async void Clock_ManySleeps_ShouldRunAsync()
        {
            var watch = new Stopwatch();
            var sleepTime = 5000;

            var firstWait = _subject.SleepAsync(sleepTime);
            var secondWait = _subject.SleepAsync(sleepTime);
            var thirdWait = _subject.SleepAsync(sleepTime);

            watch.Start();

            await firstWait;
            await secondWait;
            await thirdWait;

            watch.Stop();

            Convert.ToDouble(watch.ElapsedMilliseconds - sleepTime).Should().BeApproximately(0, _acceptableRangeInMilliseconds);
        }
    }
}
