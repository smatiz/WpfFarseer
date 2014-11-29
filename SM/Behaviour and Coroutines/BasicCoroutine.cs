using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace SM
{
    public abstract class BasicCoroutine
    {
        protected abstract IEnumerator<BasicCoroutine> DoIt();
        private IEnumerator<BasicCoroutine> _doIt;

        private bool MoveNext()
        {
            if (_doIt == null)
            {
                _doIt = DoIt();
            }
            if (_doIt == null)
            {
                return false;
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

        public void Do()
        {
            if (!MoveNext())
            {
                _doIt = null;
                MoveNext();
            }
        }
    }
}
