using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfFarseer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static World World { get; private set; }

        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        public static event Action Tick;

        protected override void OnStartup(StartupEventArgs e)
        {
            World = new World(new Microsoft.Xna.Framework.Vector2(0, 10));

            _timer.Interval = 100;
            _timer.Tick += (s_, e_) =>
            {
                World.Step(100);
                if (Tick != null)
                {
                    Tick();
                }
            };

            _timer.Start();

            base.OnStartup(e);
        }
    }
}
