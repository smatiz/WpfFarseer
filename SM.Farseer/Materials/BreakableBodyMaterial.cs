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


        public void Build(string id, IEnumerable<IShape> shapes)
        {
            var vss =  from x in shapes select x.ToFarseerVertices();
            _breakableBody = BodyFactory.CreateBreakableBody(__world, vss);
            _breakableBody.MainBody.UserData = id;
            _breakableBody.Strength = 1;


            var shapeNames = new List<string>();
            var polygonsName = CodeGenerator.N("ps_");
            CodeGenerator.AddCode("var {0} = new List<FarseerPhysics.Collision.Shapes.PolygonShape>();", polygonsName);
            foreach(var poly in vss)
            {
                var shapeName = CodeGenerator.N("vs_");
                shapeNames.Add(shapeName);
                var polyName = shapeName + "_V";
                poly.AddCode(polyName, shapeName);
                CodeGenerator.AddCode("{0}.Add({1});", polygonsName, polyName);
            }


            var breakableBodyName = CodeGenerator.N("bb_");
            CodeGenerator.AddCode("var {0} = BodyFactory.CreateBreakableBody(W, {1});", breakableBodyName, polygonsName);
            CodeGenerator.AddCode("{0}.Strength = {1};", breakableBodyName, _breakableBody.Strength);

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
                return _breakableBody.Broken; 
            }
        }



        public IEnumerable<IBodyMaterial> GetPieces()
        {
            int i = 0;
            _breakableBody.Update();
            foreach(var b in _breakableBody.Parts)
            {
                yield return new BodyMaterial(b.Body, Id + ":" + i++);
            }
        }
    }
}
