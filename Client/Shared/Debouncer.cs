// Copyright (c) Reaptor AB. All rights reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Timers;

namespace OpenVisitor.Client.Shared
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