using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public class LoopCoroutine : BasicCoroutine
    {
        private Func<float, IEnumerator<BasicCoroutine>> _func;
        float _dt;
        public LoopCoroutine(Func<float, IEnumerator<BasicCoroutine>> func)
        {
            _func = func;
        }

        protected override IEnumerator<BasicCoroutine> DoIt()
        {
            return _func(_dt);
        }

        public void Do(float dt)
        {
            _dt = dt;
            Do();
        }
    }
}
