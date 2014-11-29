using SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class StartCoroutine : BasicCoroutine
    {
        private Func<BasicManager, IEnumerator<BasicCoroutine>> _func;
        BasicManager _farseerWorldManager;
        public StartCoroutine(BasicManager farseerWorldManager, Func<BasicManager, IEnumerator<BasicCoroutine>> func)
        {
            _farseerWorldManager = farseerWorldManager;
            _func = func;
        }

        protected override IEnumerator<BasicCoroutine> DoIt()
        {
            return _func(_farseerWorldManager);
        }
    }

}
