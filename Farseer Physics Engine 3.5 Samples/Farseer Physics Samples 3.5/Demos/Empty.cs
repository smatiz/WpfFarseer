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


        Vector2 v(float x, float y)
        {
            return new Vector2(x, y);
        }
        const float Density = 1f;
        
        public override void LoadContent()
        {
            base.LoadContent();
            DebugView.AppendFlags(DebugViewFlags.Shape);
            DebugView.AppendFlags(DebugViewFlags.Joint);
            DebugView.AppendFlags(DebugViewFlags.CenterOfMass);


            //Flame.Debug.Register("ScreenManager", ScreenManager);

        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        bool waitsome = false;
        public override void HandleInput(InputHelper input, Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.HandleInput(input, gameTime);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _deferedLoopActions.DoIt();
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
