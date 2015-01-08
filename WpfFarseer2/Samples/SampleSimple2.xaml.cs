using FarseerPhysics.Dynamics;
using SM.Farseer;
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
    public partial class SampleSimple2 : UserControl
    {
        public SampleSimple2()
        {
            InitializeComponent();
            _farseerPlayer.Ready += _farseerPlayer_Ready;
        }

        void _farseerPlayer_Ready(FarseerWorldManager farseer)
        {
            var body = (Body)farseer.Entity.x;
            body.LinearVelocity = new Microsoft.Xna.Framework.Vector2(5, -10);


        }
    }
}
