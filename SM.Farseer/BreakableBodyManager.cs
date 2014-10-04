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
    public class BreakableBodyManager : IUpdatable
    {
        private Vector2 _originalPosition;

         BreakableBody _breakableBody;
        IBreakableBodyObject _bodyControl;
        BodyUpdater[] _bodyControlParts;

        public object Object { get { return _breakableBody; } }

        public BreakableBodyManager(IBreakableBodyObject bodyControl, BreakableBody body, Vector2 originPosition)
        {
            body.MainBody.UserData = bodyControl.Id;
            body.MainBody.FixtureList.AddRange(bodyControl.GetAttachFixtures(body.MainBody));
            body.MainBody.BodyType = bodyControl.BodyType;
            CodeGenerator.AddCode(String.Format("{1}.BodyType = BodyType.{0};", Enum.GetName(typeof(BodyType), bodyControl.BodyType), body.MainBody.g()));
            body.MainBody.Position = originPosition;
            CodeGenerator.AddCode(String.Format("{1}.Position = {0};", originPosition.g(), body.MainBody.g()));

            _originalPosition = originPosition;
            _breakableBody = body;
            _bodyControl = bodyControl;
        }

        public void Update()
        {
            if (_bodyControlParts != null)
            {
                for (int i = 0; i < _bodyControlParts.Length; i++)
                {
                    _bodyControlParts[i].Update();
                }
            }
            else if (_breakableBody.Broken)
            {
                 var q = _breakableBody.MainBody.Position - _originalPosition;
                _bodyControl.Set(q.X, q.Y, _breakableBody.MainBody.Rotation);
            }
            else
            {

                _bodyControlParts = (from x in _breakableBody.Parts select _bodyControl.Get(BodyFactory.CreateBody(null, _originalPosition), _originalPosition)).ToArray();
            }
        }

        public string Id
        {
            get { return (string)_breakableBody.MainBody.UserData; } 
        }
    }
}
