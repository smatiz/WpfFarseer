using SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFarseer
{
    public class StartCoroutine : BasicCoroutine
    {
        private Func<FarseerWorldManager, IEnumerator<BasicCoroutine>> _func;
        FarseerWorldManager _farseerWorldManager;
        public StartCoroutine(FarseerWorldManager farseerWorldManager, Func<FarseerWorldManager, IEnumerator<BasicCoroutine>> func)
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
