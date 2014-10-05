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
        BreakableBody _breakableBody;
        bool _brokenWarned = false;
        public event Action OnBroke;

        public BreakableBodyUpdater(IBodyControl bodyControl, BreakableBody body, Vector2 originPosition)
            : base(bodyControl, body.MainBody, originPosition)
        {
            _breakableBody = body;
        }

        public override void Update()
        {
            if (_breakableBody.Broken && !_brokenWarned)
            {
                _brokenWarned = true;
                if(OnBroke != null)
                {
                    OnBroke();
                }
            }
            else
            {
                base.Update();
            }
        }

        public override object Object { get { return _breakableBody; } }
    }
}
