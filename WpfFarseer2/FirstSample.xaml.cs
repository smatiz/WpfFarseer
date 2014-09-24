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
    public partial class FirstSample : UserControl, ICoroutinable
    {
        FarseerPhysics.Dynamics.Joints.RopeJoint jointC;

        public FirstSample()
        {
            InitializeComponent();
            _farseerPlayer.FarseerCanvas.Loaded += (s, e) =>
            {

                jointC = ((FarseerPhysics.Dynamics.Joints.RopeJoint)_farseerPlayer.FarseerCanvas.WorldManager.Find("jointC"));
                _farseerPlayer.FarseerCanvas.WorldManager.Coroutine = new Coroutinator(this);
            };
        }




        public IEnumerator<BasicCoroutine> DoCoroutine()
        {
            yield return new WaitSecondsCoroutine(5);
            //MessageBox.Show("**************");
            jointC.MaxLength *= 0.4f;

        }
    }
}
