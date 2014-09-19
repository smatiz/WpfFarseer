using FarseerPhysics.Samples;
using Flame.Controls;
using Flame.Dlr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace exe_flame_farseer_xna
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                var f = new Flame.Controls.ScripterControlForm();
                f.Size = new System.Drawing.Size(900,500);
               
                FarseerPhysicsGame game;
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    game = new FarseerPhysicsGame("flame");
                    game.InactiveSleepTime = new TimeSpan(0);
                    Flame.Debug.Register("xna", game);

                    var path = Application.ExecutablePath;
                    path = System.IO.Path.Combine(path, "..");
                    path = System.IO.Path.Combine(path, "..");
                    path = System.IO.Path.Combine(path, "..");
                    path = System.IO.Path.Combine(path, "..");
                    path = System.IO.Path.Combine(path, "..");
                    path = System.IO.Path.Combine(path, "..");
                    path = System.IO.Path.Combine(path, @"Farseer Physics Engine 3.5 Samples\Farseer Physics Samples 3.5\bin\x86\Debug\Content\");
                    path = System.IO.Path.GetFullPath(path);
                    game.Content.RootDirectory = path;
                    //System.Diagnostics.Debugger.Launch();
                    game.Run();
                })).Start();


                Application.Run(f);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
