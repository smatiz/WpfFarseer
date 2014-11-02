using System.IO;
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

            if (args.Length == 1)
            {
                if (File.Exists(args[0]))
                {
                    string code = File.ReadAllText(args[0]);
                    using (RoslynFarseerPhysicsGame game = new RoslynFarseerPhysicsGame(code))
                    {
                        game.Run();
                    }

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