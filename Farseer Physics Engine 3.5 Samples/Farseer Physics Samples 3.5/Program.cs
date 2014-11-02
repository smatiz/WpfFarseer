using System.IO;
namespace FarseerPhysics.Samples
{
    public static class Program
    {
        static void start(string code)
        {
            using (RoslynFarseerPhysicsGame game = new RoslynFarseerPhysicsGame(code))
            {
                game.Run();
            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            string frameworkName = "MONOGAME -";
#if XNA
            frameworkName = "XNA -";
#endif


            if (args.Length == 1)
            {
                if (File.Exists(args[0]))
                {
                    start( File.ReadAllText(args[0]));
                }
                else
                {
                    start(args[0]);
                }
            }
            else
            {
                using (FarseerPhysicsGame game = new FarseerPhysicsGame(frameworkName))
                {
                    game.Run();
                }
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