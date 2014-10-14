using SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public class UpdateCoroutine : BasicCoroutine
    {
        private Func<IEnumerator<BasicCoroutine>> _func;

        public UpdateCoroutine(Func<IEnumerator<BasicCoroutine>> func)
        {
            _func = func;
        }

        protected override IEnumerator<BasicCoroutine> DoIt()
        {
            return _func();
        }
    }
}
