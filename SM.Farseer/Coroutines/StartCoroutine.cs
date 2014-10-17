using SM;
using SM.Farseer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public class StartCoroutine : BasicCoroutine
    {
        private Func<FarseerWorldManager_old, IEnumerator<BasicCoroutine>> _func;
        FarseerWorldManager_old _farseerWorldManager;
        public StartCoroutine(FarseerWorldManager_old farseerWorldManager, Func<FarseerWorldManager_old, IEnumerator<BasicCoroutine>> func)
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
