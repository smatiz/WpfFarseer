using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.Demos.Prefabs;
using FarseerPhysics.Samples.ScreenSystem;
using FarseerPhysics.Samples.SM;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarseerPhysics.Samples.Demos
{

    public class DeferedLoopAction
    {
        public int LoopCountWait { get; set; }
        public Action Action { get; set; }
        public bool Done {get { return LoopCountWait<= 0;}}
        public void DoIt()
        {
            if (Done) return;
            LoopCountWait--;
            if(LoopCountWait<= 0)
            {
                Action();
            }
        }


    }

    public class DeferedLoopActions
    {
        List<DeferedLoopAction> _deferedLoopAction = new List<DeferedLoopAction>();
        public DeferedLoopActions()
        {

        }
        public void Add(DeferedLoopAction deferedLoopAction)
        {
            _deferedLoopAction.Add(deferedLoopAction);
        }
        public void DoIt()
        {
            foreach(var x in _deferedLoopAction)
            {
                x.DoIt();
            }
            _deferedLoopAction = (from x in _deferedLoopAction where !x.Done select x).ToList<DeferedLoopAction>();
        }
    }


    public class Actions
    {
        public Action<Microsoft.Xna.Framework.GameTime> Draw { get; set; }
        public Action<InputHelper, Microsoft.Xna.Framework.GameTime> HandleInput { get; set; }
        public Action<Microsoft.Xna.Framework.GameTime, bool, bool> Update { get; set; }

    }


    internal class Empty : PhysicsGameScreen, IDemoScreen
    {


        DeferedLoopActions _deferedLoopActions = new DeferedLoopActions();



        public string GetTitle()
        {
            return "empty";
        }

        public string GetDetails()
        {
            return GetTitle();
        }

        Actions _actions = new Actions();


        Vector2 v(float x, float y)
        {
            return new Vector2(x, y);
        }
        const float Density = 1f;
        List<Body> walls = new List<Body>();
        
        public override void LoadContent()
        {
            base.LoadContent();
            DebugView.AppendFlags(DebugViewFlags.Shape);
            DebugView.AppendFlags(DebugViewFlags.Joint);
            DebugView.AppendFlags(DebugViewFlags.CenterOfMass);

            World.Gravity = v(0, 10);

            var border = new Border(World, ScreenManager, Camera);

            var wall = BodyFactory.CreateRectangle(World, 10, 1, Density);
            wall.Position = v(0, -10);
            walls.Add(wall);

            //BodyFactory.CreateCompoundPolygon(World, new List<Vertices>() {
            //    new Vertices(new Vector2[]{
            //        new Vector2(0, 0), 
            //        new Vector2(1, 0), 
            //        new Vector2(1, 1), 
            //        new Vector2(0, 1), 
            //    }),
            //    new Vertices(new Vector2[]{
            //        new Vector2(10, 10), 
            //        new Vector2(10, 20), 
            //        new Vector2(20, 20), 
            //        new Vector2(20, 10), 
            //    })
            //}, Density);



            //Flame.Debug.Register("ScreenManager", ScreenManager);
            Flame.Debug.Register("Actions", _actions);

            Flame.Debug.Register("ScreenManager", ScreenManager);
            Flame.Debug.Register("Camera", Camera);
            Flame.Debug.Register("World", World);

            RopeCreator1 = (new FarseerPhysics.Samples.SM.RopeCreator1(World));
            //RopeCreator1.Test2();

            //_deferedLoopActions.Add(new DeferedLoopAction() { Action = () => RopeCreator1.Launch(), LoopCountWait = 90 });
        }
        RopeCreator1 RopeCreator1;
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (_actions.Draw != null) _actions.Draw(gameTime);
            base.Draw(gameTime);
        }

        bool waitsome = false;
        public override void HandleInput(InputHelper input, Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (_actions.HandleInput != null) _actions.HandleInput(input, gameTime);

            if(input.KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                RopeCreator1.Launch();
            }

            if (!waitsome)
            {
                if (input.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    waitsome = true;
                    _deferedLoopActions.Add(new DeferedLoopAction() { Action = () => waitsome = false, LoopCountWait = 10 });
                    var p = Camera.ConvertScreenToWorld(input.Cursor);//new Vector2(input.MouseState.X, input.MouseState.Y));

                    bool attached = false;
                    foreach (var w in walls)
                    {
                        foreach (var f in w.FixtureList)
                        {
                            if (f.TestPoint(ref p))
                            {
                                RopeCreator1.Launch(p, w);
                                _deferedLoopActions.Add(new DeferedLoopAction() { Action = () => RopeCreator1.Launch(), LoopCountWait = 10 });
                                attached = true;
                                break;
                            }
                        }
                        if (attached)
                            break;
                    }
                    if (!attached)
                        RopeCreator1.Destroy();

                }
            }

            base.HandleInput(input, gameTime);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _deferedLoopActions.DoIt();
            if (_actions.Update != null) _actions.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
