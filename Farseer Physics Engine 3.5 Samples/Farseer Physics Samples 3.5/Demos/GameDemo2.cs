using System;
using System.Collections.Generic;
using System.Text;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.DrawingSystem;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using FarseerPhysics.Samples.Demos.Prefabs;

namespace FarseerPhysics.Samples.Demos
{
    class Ball : Body
    {
        public Ball(World world)
            : base(world) 
        {
            FixtureFactory.AttachEllipse(1, 1, Settings.MaxPolygonVertices, 1, this);
            Restitution = 0.1f;
            BodyType = BodyType.Dynamic;
        }

    }

    class MainBall : Ball
    {
        public MainBall(World world)
            : base(world)
        {
            Restitution = 0.1f;
            BodyType = BodyType.Dynamic;
        }
    }
    class OneHitBall : Ball
    {

        public OneHitBall(World world) : base(world) { }
        bool _oneHitten = false;
        public bool Hitten { get { return _oneHitten; } }
        public void Hit(out bool gameOver)
        {
            gameOver = _oneHitten;
            _oneHitten = true;
        }
    }

    internal class GameDemo2 : PhysicsGameScreen, IDemoScreen
    {

        Body _playerBody;
        OneHitBall[] _otherBodies;

        OneHitBall CreateHitBall(float x, float y)
        {
            var body = new OneHitBall(World);
            body.Position = new Vector2(x, y);

            body.OnCollision += body_OnCollision;
            return body;
        }

        MainBall CreateMainBall(float x, float y)
        {
            var body = new MainBall(World);
            body.Position = new Vector2(x, y);
            return body;
        }

        bool checkGameOver(Body mainBall, Body oneHit)
        {
            if (!(mainBall is MainBall)) return false;
            var oneHitT = oneHit as OneHitBall;
            if (oneHitT == null) return false;
            bool gameover;
            oneHitT.Hit(out gameover);
            return gameover;
        }

        bool checkWin()
        {
            foreach(var x in _otherBodies)
            {
                if (!x.Hitten) return false;
            }
            return true;
        }

        void Win()
        {
            System.Windows.Forms.MessageBox.Show("Win");
            _gameover = true; 
        }

        bool _gameover = false;

        void gameOver() 
        {
            System.Windows.Forms.MessageBox.Show("Game Over");
            _gameover = true; 
        }

        bool body_OnCollision(Fixture fixtureA, Fixture fixtureB, Dynamics.Contacts.Contact contact)
        {
            if (!_gameover && checkGameOver(fixtureA.Body, fixtureB.Body)
             || checkGameOver(fixtureB.Body, fixtureA.Body))
            {
                gameOver();
            }

            if (!_gameover && checkWin())
            {
                Win();
            }
            return true;
        }



        public override void LoadContent()
        {

            Settings.MaxPolygonVertices = 40;
            base.LoadContent();

            var border = new Border(World, ScreenManager, Camera);

            DebugView.AppendFlags(DebugViewFlags.Shape);


            _playerBody = CreateMainBall(0, 0);

            _otherBodies = (from x in Enumerable.Range(0, 10) select CreateHitBall(x * 2, 10)).ToArray();
            

            //World.Gravity = new Vector2(0f, 10f);

            
        }

        

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);


            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            //DebugView.TextColor


            
            var viewportSize = new Vector2(viewport.Width, viewport.Height);



            


            DebugView.BeginCustomDraw(ref Camera.SimProjection, ref Camera.SimView);
            foreach (var x in _otherBodies)
            {
                DebugView.DrawCircle(x.Position, 0.5f, x.Hitten ? Color.Red : Color.Green);
            }

            

            DebugView.DrawCircle(Vector2.Zero, 0.1f, Color.Yellow);
            DebugView.DrawCircle(new Vector2(15f * 1920f / 1080f, 15f), 0.1f, Color.Blue);

            DebugView.EndCustomDraw();


            if (_gameover)
            {
                var goText = "GAME OVER";
                var textSize = DebugView.MeasureString(goText);
                DebugView.DrawString((viewportSize - textSize) * 0.5f, goText);
            }
        }

        public string GetTitle()
        {
            return "Biliardo Fisico";
        }

        public string GetDetails()
        {
            return "";
        }
    }
}