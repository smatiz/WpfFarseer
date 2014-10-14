using SM;
using SM.Farseer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFarseer
{
    public interface IFarseerBehaviourWpf : IBehaviour
    {
        // nel thread grafico quando wpf ha finito
        IEnumerator<BasicCoroutine> Start(FarseerWorldManager farseerWorld);
        // loop del thread grafico (DispatcherTimer)
        IEnumerator<BasicCoroutine> Update();
    }
    

}
