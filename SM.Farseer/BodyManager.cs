using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public interface IUpdatable : IFarseerObject
    {
        void Update();
    }

    // si occupa di gestire il dialogo tra Body e BodyControl
    public class BodyUpdater : IUpdatable
    {
        IBodyObject _bodyControl;
        private Vector2 _originalPosition;
        
        public BodyUpdater(IBodyObject bodyControl, Body body, Vector2 originPosition)
        {
            body.UserData = bodyControl.Id;
            body.FixtureList.AddRange(bodyControl.GetAttachFixtures(body));
            body.BodyType = bodyControl.BodyType;
            CodeGenerator.AddCode(String.Format("{1}.BodyType = BodyType.{0};", Enum.GetName(typeof(BodyType), bodyControl.BodyType), body.g()));
            body.Position = originPosition;
            CodeGenerator.AddCode(String.Format("{1}.Position = {0};", originPosition.g(), body.g()));

            _originalPosition = originPosition;
            Body = body;
            _bodyControl = bodyControl;
        }

        public Body Body { get; private set; }
        public string Id { get { return (string)Body.UserData; } }
        

        public void Update()
        {
            var q = Body.Position - _originalPosition;
            _bodyControl.Set(q.X, q.Y, Body.Rotation);
        }
    }
}
