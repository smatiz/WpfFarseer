using SM;
using SM.Farseer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public interface IFarseerBehaviourWpf : IBehaviour
    {
        // nel thread grafico quando wpf ha finito
        IEnumerator<BasicCoroutine> Start(FarseerWorldManager_old farseerWorld);
        // loop del thread grafico (DispatcherTimer)
        IEnumerator<BasicCoroutine> Update();
    }
    

}
