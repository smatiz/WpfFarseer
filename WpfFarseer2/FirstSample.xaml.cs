using SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfFarseer
{
    class xxx : IFarseerBehaviourWpf
    {
        FarseerPhysics.Dynamics.Joints.RopeJoint jointC;

        public IEnumerator<BasicCoroutine> Start(FarseerWorldManager farseerWorld)
        {
            jointC = ((FarseerPhysics.Dynamics.Joints.RopeJoint)farseerWorld.Find("jointC"));
            return null;
        }

        public IEnumerator<BasicCoroutine> Update()
        {
            return null;
        }

        public IEnumerator<BasicCoroutine> Loop(float dt)
        {
             yield return new WaitSecondsCoroutine(5);
            //MessageBox.Show("**************");
            jointC.MaxLength *= 0.4f;
            //return null;
        }
    }


    /// <summary>
    /// Interaction logic for FirstSample.xaml
    /// </summary>
    public partial class FirstSample : UserControl
    {

        public FirstSample()
        {
            InitializeComponent();

            _farseerPlayer.FarseerCanvas.AddFarseerBehaviour(new xxx());
            //_farseerPlayer.FarseerCanvas.AddLoop(FarseerCanvas_WorldLoop);
            //_farseerPlayer.FarseerCanvas.WorldReady += FarseerCanvas_WorldStarted;
        }

       



    }
}
