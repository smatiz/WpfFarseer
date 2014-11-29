using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class FuncCoroutine : BasicCoroutine
    {
        private Func<IEnumerator<BasicCoroutine>> _func;

        public FuncCoroutine(Func<IEnumerator<BasicCoroutine>> func)
        {
            _func = func;
        }

        protected override IEnumerator<BasicCoroutine> DoIt()
        {
            return _func();
        }
    }
}
