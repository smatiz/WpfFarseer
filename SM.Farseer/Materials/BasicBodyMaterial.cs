using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public abstract class BasicBodyMaterial
    {
        protected abstract Body Body { get; }

        public rotoTranslation RotoTranslation
        {
            get
            {
                var q = Body.Position;
                return new rotoTranslation(new float2(q.X, q.Y), Body.Rotation);
            }
        }
        public IdInfo Id { get { return (IdInfo)Body.UserData; } }

        protected PolygonShape getPolygon(PolygonMaterial shape)
        {
            var vertices = shape.Points.ToFarseerVertices();
            return new PolygonShape(vertices, shape.Density);
        }
        protected CircleShape getCircle(CircleMaterial shape)
        {
            var circleShape = new CircleShape(shape.Radius, shape.Density);
            circleShape.Position = shape.Center.ToFarseer();
            return circleShape;
        }
    }
}
