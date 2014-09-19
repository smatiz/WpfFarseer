namespace FarseerPhysics.Samples
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            string frameworkName = "MONOGAME -";
#if XNA
            frameworkName = "XNA -";
#endif

            using (FarseerPhysicsGame game = new FarseerPhysicsGame(frameworkName))
            {
                //Flame.Debug.Register("game", game);
                //var manager = new ManagerCreator();
                //var settings = new ScripterControlFormSettings()
                //var f = new Flame.Controls.ScripterControlForm();
                //f.Load += (s, e) =>
                //{
                //    f.ScripterTabbedControl.ScripterLoaderControl.ScripterControl.Manager.AddVariable(new Variable() { Name = "game", Data = game });
                //};
                //f.ShowDialog();
                //game.Content.RootDirectory = @"C:\Users\Developer\Desktop\Farseer Physics Engine 3.5 Samples\Farseer Physics Samples 3.5\bin\x86\Debug\Content";

                game.Run();
            }
        }

        static FarseerPhysicsGame game;
       
        public static void Start()
        {
            string frameworkName = "MONOGAME -";
#if XNA
            frameworkName = "XNA -";
#endif
            
            {
                FarseerPhysicsGame game = new FarseerPhysicsGame(frameworkName);
                Flame.Debug.Register("game", game);
          
            }
        }
    }
}