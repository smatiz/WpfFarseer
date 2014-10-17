using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SM
{
    class ViewWatch : IViewWatch
    {
        private enum Status { Stopped, Play, Pause }
        private Status _status = Status.Stopped;
        private const long Interval = 40;
        DispatcherTimer _timer;
        public Action Callback { private get; set; }

        public ViewWatch(Dispatcher dispacher)
        {
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromMilliseconds(Interval);
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
            _status = Status.Play;
        }

        public void Pause()
        {
            _status = Status.Pause;
        }
    }
}
