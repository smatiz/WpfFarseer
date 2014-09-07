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

        static void Debug_Registered(object sender, Flame.Debug.Registration e)
        {

        }

        private static void test()
        {
            var path = @"C:\Users\Developer\Desktop\Farseer Physics Engine 3.5 Samples\Farseer Physics Samples 3.5\bin\x86\Debug\";
           var game = new FarseerPhysics.Samples.FarseerPhysicsGame("flame");
            game.Content.RootDirectory = path + @"Content\";
            
            System.Diagnostics.Debugger.Launch();
            Control f = Form.FromHandle(game.Window.Handle);
            game.Activated += (s, e) => { f.Visible = false; };
            var timer = new Timer() { Interval = 2000 };
            timer.Tick += (s, e) => { f.Visible = !f.Visible; };
            timer.Start();
            game.Run(); 
        }

    }
}
