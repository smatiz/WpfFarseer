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
    // si occupa di gestire il dialogo tra Body e BodyControl
    internal class BodyUpdater : IUpdatable
    {
        protected IBodyControl _bodyControl;
        private Vector2 _originalPosition;
        private Body _body;
        
        public BodyUpdater(IBodyControl bodyControl, Body body, Vector2 originPosition)
        {
            body.UserData = bodyControl.Id;
            body.FixtureList.AddRange(bodyControl.GetAttachFixtures(body));
            body.BodyType = (FarseerPhysics.Dynamics.BodyType)bodyControl.BodyType;
            CodeGenerator.AddCode(String.Format("{1}.BodyType = BodyType.{0};", Enum.GetName(typeof(BodyType), bodyControl.BodyType), body.g()));
            body.Position = originPosition;
            CodeGenerator.AddCode(String.Format("{1}.Position = {0};", originPosition.g(), body.g()));

            _originalPosition = originPosition;
            _body = body;
            _bodyControl = bodyControl;
        }
        public virtual object Object { get { return _body; } }
        public string Id { get { return (string)_body.UserData; } }


        public virtual void Update()
        {
            var q = _body.Position - _originalPosition;
            _bodyControl.Set(q.X, q.Y, _body.Rotation);
        }
    }
}
