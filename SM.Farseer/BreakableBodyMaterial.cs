using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public class BreakableBodyMaterial : IBreakableBodyMaterial
    {
        private Vector2 _originalPosition = Vector2.Zero;
        private BreakableBody _breakableBody;

        public virtual object Object { get { return _breakableBody; } }
        public string Id { get { return (string)_breakableBody.MainBody.UserData; } }

        World __world;

        public BreakableBodyMaterial(World world)
        {
            __world = world;
        }


        public void Build(string id, IEnumerable<IShapeView> shapes)
        {
            //_body = body;
            _breakableBody = BodyFactory.CreateBreakableBody(__world, from x in shapes select x.ToFarseerVertices());
            _breakableBody.MainBody.UserData = id;

            //_breakableBody.MainBody.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;


            //_body.Position = originPosition;
            //_originalPosition = originPosition;

            //foreach (var shape in shapes)
            {
              //  return new ShapeMaterial(_breakableBody.MainBody);
            }


        }

        public rotoTranslation RotoTranslation
        {
            get
            {
                var q = _breakableBody.MainBody.Position - _originalPosition;
                return new rotoTranslation(new float2(q.X, q.Y), _breakableBody.MainBody.Rotation);
            }
        }


        public bool IsBroken
        {
            get
            { 
                return _breakableBody.Broken; }
        }



        public IEnumerable<IBodyMaterial> GetPieces()
        {
            int i = 0;
            foreach(var b in _breakableBody.Parts)
            {
                yield return new BodyMaterial(b.Body, Id + ":" + i++);
            }
        }
    }
}
