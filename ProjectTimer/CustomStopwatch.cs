using System;
using System.Diagnostics;

namespace ProjectTimer
{
    /// <summary>
    /// Custom Stopwatch implementation which supports a base time when resetting. 
    /// </summary>
    public class CustomStopwatch
    {
        private Stopwatch _stopwatch;
        private long _baseTime;

        public CustomStopwatch()
        {
            _stopwatch = new Stopwatch();
        }

        public long ElapsedMilliseconds => _stopwatch.ElapsedMilliseconds + _baseTime;

        public bool IsRunning => _stopwatch.IsRunning;

        internal void Start() => _stopwatch.Start();

        internal void Stop() => _stopwatch.Stop();

        internal void Reset(long? baseTimeInMilliseconds = null)
        {
            _stopwatch.Reset();
            _baseTime = baseTimeInMilliseconds ?? 0;
        }

    }
}
