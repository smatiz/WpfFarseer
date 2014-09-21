using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFarseer
{
    public class BodyController
    {
        const float AngleSubst = 180f / (float)Math.PI;
        private Body _body;
        Vector2 _originalPosition;

        public BodyController(Body body, Vector2 originPosition)
        {
            _originalPosition = originPosition;
            _body = body;
        }

        public void Update(System.Windows.Media.TranslateTransform _traslation, System.Windows.Media.RotateTransform _rotation)
        {
            var q = _body.Position - _originalPosition;
            _traslation.X = q.X;
            _traslation.Y = q.Y;
            _rotation.Angle = _body.Rotation * AngleSubst;
        }

        internal void Update()
        {
            throw new NotImplementedException();
        }
    }
}
