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
    internal class BreakableBodyUpdater : BodyUpdater
    {
        //private Vector2 _originalPosition;

        BreakableBody _breakableBody;
        //IBreakableBodyControl _bodyControl;
        IUpdatable[] _bodyControlParts;

        public override object Object { get { return _breakableBody; } }

        public BreakableBodyUpdater(IBreakableBodyControl bodyControl, BreakableBody body, Vector2 originPosition)
            : base(bodyControl, body.MainBody, originPosition)
        {
            //body.MainBody.UserData = bodyControl.Id;
            //body.MainBody.FixtureList.AddRange(bodyControl.GetAttachFixtures(body.MainBody));
            //body.MainBody.BodyType = bodyControl.BodyType;
            //CodeGenerator.AddCode(String.Format("{1}.BodyType = BodyType.{0};", Enum.GetName(typeof(BodyType), bodyControl.BodyType), body.MainBody.g()));
            //body.MainBody.Position = originPosition;
            //CodeGenerator.AddCode(String.Format("{1}.Position = {0};", originPosition.g(), body.MainBody.g()));

            //_originalPosition = originPosition;
            _breakableBody = body;
            //_bodyControl = bodyControl;
        }

        public override void Update()
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
                //_bodyControlParts = (from x in _breakableBody.Parts select _bodyControl.Get(BodyFactory.CreateBody(null, _originalPosition), _originalPosition)).ToArray();

            }
            else
            {
                base.Update();
                //var q = _breakableBody.MainBody.Position - _originalPosition;
                //_bodyControl.Set(q.X, q.Y, _breakableBody.MainBody.Rotation);
            }
        }

        //public string Id
        //{
        //    get { return (string)_breakableBody.MainBody.UserData; }
        //}
    }
}
