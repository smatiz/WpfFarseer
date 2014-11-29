using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface ICoroutinable
    {
        IEnumerator<BasicCoroutine> DoCoroutine();
    }

    public class Coroutinator : BasicCoroutine
    {
        ICoroutinable _coroutinable;

        public Coroutinator(ICoroutinable coroutinable)
        {
            _coroutinable = coroutinable;
        }
        protected override IEnumerator<BasicCoroutine> DoIt()
        {
            return _coroutinable.DoCoroutine();
        }
    }
}
