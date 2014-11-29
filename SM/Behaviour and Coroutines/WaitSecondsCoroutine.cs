using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class WaitSecondsCoroutine : BasicCoroutine
    {
        Stopwatch _stopwatch = new Stopwatch();
        int _milliseconds;
        public WaitSecondsCoroutine(int seconds)
        {
            _milliseconds = seconds * 1000;
            _stopwatch.Start();
        }
        protected override IEnumerator<BasicCoroutine> DoIt()
        {
            while (_stopwatch.ElapsedMilliseconds < _milliseconds)
            {
                yield return new EmptyCoroutine();
            }
            _stopwatch.Stop();
            yield break;
        }
    }
}
