using SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IBehaviourView 
    {
        // nel thread grafico quando il load e concluso
        IEnumerator<BasicCoroutine> Start(BasicManager farseerWorld);
        // loop del thread grafico 
        IEnumerator<BasicCoroutine> Update();
    }
}
