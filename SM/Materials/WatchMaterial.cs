using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SM
{
    public class WatchMaterial 
    {
        private enum Status { Stopped, Play, Pause }
        public const float DT = 1 / 50f;
        private const long Interval = (long)(DT * 0.5);
        private Timer _timer;
        private Status _status = Status.Stopped;
        private System.Diagnostics.Stopwatch _realWatch = new System.Diagnostics.Stopwatch();
        private double _lastElapsedTotalSeconds = 0;

        private Action _stepCallback;

        public WatchMaterial(Action stepCallback)
        {
            _stepCallback = stepCallback;
            _timer = new Timer(new TimerCallback(callback), null, Interval, Timeout.Infinite);
        }

        System.Diagnostics.Stopwatch _watch = new System.Diagnostics.Stopwatch();
        float _remain;
        private void callback(Object state)
        {
            _watch.Start();
            if (_status == Status.Play)
            {
                var lastElapsedTotalSeconds = _realWatch.Elapsed.TotalSeconds;
                float seconds = (float)(lastElapsedTotalSeconds - _lastElapsedTotalSeconds) + _remain;
                while (seconds > DT)
                {
                    _stepCallback();
                    seconds -= DT;
                }

                _remain = seconds;
                _lastElapsedTotalSeconds = lastElapsedTotalSeconds;
            }
            _timer.Change(Math.Max(0, Interval - _watch.ElapsedMilliseconds), Timeout.Infinite);
        }


       public void Play()
        {
            _status = Status.Play;
            _realWatch.Start();
        }

       public void Pause()
       {
           _status = Status.Pause;
           _realWatch.Stop();
        }
    }
}
