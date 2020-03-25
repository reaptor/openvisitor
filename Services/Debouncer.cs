using System;
using System.Timers;
using System.Collections.Generic;

namespace OpenVisitor.Services
{
    public class Debouncer
    {
        Action _action = () => {};
        Timer _timer = new Timer();
        static Dictionary<string, Debouncer> Debouncers = new Dictionary<string, Debouncer>();

        public Debouncer()
        {
            _timer.Elapsed += OnTimer;
        }

        public void Debounce(int millisecs, Action action)
        {
            _timer.Stop();
            _timer.Interval = millisecs;
            _action = action;
            _timer.Start();
        }

        void OnTimer(object sender, EventArgs ea)
        {
            _timer.Stop();
            _action();
        }
    }
}