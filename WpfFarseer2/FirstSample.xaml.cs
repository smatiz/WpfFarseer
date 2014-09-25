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
    /// <summary>
    /// Interaction logic for FirstSample.xaml
    /// </summary>
    public partial class FirstSample : UserControl
    {
        FarseerPhysics.Dynamics.Joints.RopeJoint jointC;

        public FirstSample()
        {
            InitializeComponent();

            _farseerPlayer.FarseerCanvas.AddLoop(FarseerCanvas_WorldLoop);
            _farseerPlayer.FarseerCanvas.WorldReady += FarseerCanvas_WorldStarted;
        }

        void FarseerCanvas_WorldStarted(FarseerWorldManager worldManager)
        {
            jointC = ((FarseerPhysics.Dynamics.Joints.RopeJoint)worldManager.Find("jointC"));
        }

        IEnumerator<BasicCoroutine> FarseerCanvas_WorldLoop(FarseerWorldManager worldManager)
        {

            yield return new WaitSecondsCoroutine(5);
            //MessageBox.Show("**************");
            jointC.MaxLength *= 0.4f;
        }

       



    }
}
