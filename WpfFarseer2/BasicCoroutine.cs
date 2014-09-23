using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace WpfApplication1
{
    public abstract class BasicCoroutine : IEnumerator<BasicCoroutine>
    {
        protected abstract IEnumerator<BasicCoroutine> DoIt();

        private IEnumerator<BasicCoroutine> _doIt;
        public BasicCoroutine()
        {
        }


        public BasicCoroutine Current
        {
            get 
            { 
                return _doIt.Current; 
            }
        }
        public bool MoveNext()
        {
            if(_doIt == null)
            {
                _doIt = DoIt();
            }
            if (_doIt.Current == null)
            {
                return _doIt.MoveNext();
            }
            else if (_doIt.Current.MoveNext())
            {
                return true;
            }
            else
            {
                return _doIt.MoveNext();
            }
        }


        public void Reset()
        {
            _doIt = null;
        }


        public void LoopStep()
        {
            if(! MoveNext())
            {
                Reset();
                MoveNext();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        object IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }

    }
    public class EmptyCoroutine : BasicCoroutine
    {
        protected override IEnumerator<BasicCoroutine> DoIt()
        {
            yield break;
        }
    }

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

    public class TestCoroutine : BasicCoroutine
    {
        protected override IEnumerator<BasicCoroutine> DoIt()
        {
            yield return new WaitSecondsCoroutine(5);
            MessageBox.Show("**************");
        }
		
		public static void Test()
		{
		
			var x = new TestCoroutine();
      
            var t = new System.Windows.Forms.Timer();

            t.Interval = 10;
            t.Tick += (s, e) =>  { x.LoopStep(); };
     

            t.Start();


		}
    }
	
	
	
	
}
