using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SM
{
    public class WatchView : IWatchView
    {
        private enum Status { Stopped, Play, Pause }
        private Status _status = Status.Stopped;
        private const long Interval = 50;
        private DispatcherTimer _timer;
        public Action Callback { private get; set; }

        public WatchView(long interval = Interval)
        {
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromMilliseconds(interval);
            _timer.Tick += _timer_Tick;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (_status == Status.Play && Callback != null)
            {
                Callback();
            }
        }

        public void Play()
        {
            if (_status == Status.Stopped) _timer.Start();
            _status = Status.Play;
        }

        public void Pause()
        {
            _status = Status.Pause;
        }
    }
}
