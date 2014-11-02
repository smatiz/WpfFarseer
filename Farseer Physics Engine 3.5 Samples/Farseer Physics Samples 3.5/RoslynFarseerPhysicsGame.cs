using System;
using FarseerPhysics.Samples.Demos;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;

namespace FarseerPhysics.Samples
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class RoslynFarseerPhysicsGame : Game
    {
        private GraphicsDeviceManager _graphics;
        string _code;
        public RoslynFarseerPhysicsGame(string code = "")
        {
            _code = code;
            Window.Title = "Roslyn Farseer Samples Framework";
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferMultiSampling = true;

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            ConvertUnits.SetDisplayUnitToSimUnitRatio(24f);
            IsFixedTimeStep = true;

            _graphics.IsFullScreen = false;

            Content.RootDirectory = "Content";

            //new-up components and add to Game.Components
            ScreenManager = new ScreenManager(this);
            Components.Add(ScreenManager);

            FrameRateCounter frameRateCounter = new FrameRateCounter(ScreenManager);
            frameRateCounter.DrawOrder = 101;
            Components.Add(frameRateCounter);
        }

        public ScreenManager ScreenManager { get; set; }

        protected override void Initialize()
        {
            base.Initialize();

            MenuScreen menuScreen = new MenuScreen("Farseer Samples");
            menuScreen.AddMenuItem("", EntryType.Screen, new RoslynGame(_code));

            ScreenManager.AddScreen(new BackgroundScreen());
            ScreenManager.AddScreen(menuScreen);
        }
    }
}