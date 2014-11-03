using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
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
        World _world;
        Body _body;
        private BreakableBody _breakableBody;

        public virtual object Object { get { return _breakableBody; } }
        public string Id { get { return (string)_body.UserData; } }


        public BreakableBodyMaterial(World world)
        {
            _world = world;
        }


        public void Build(string id, rotoTranslation rt, IEnumerable<IShape> shapes)
        {
            var allShapes = shapes.SelectMany<IShape, Shape>(x => x.ToShapes());




            //CodeGenerator.AddCode("var allShapes_{0} = new List<Shape>();", id);
            int i = 0;
            foreach(var shape in allShapes)
            {

                //CodeGenerator.AddCode("var allShapes_{0}_item{1} = ;", id, i, shape);

            }



            _breakableBody = BodyFactory.CreateBreakableBody(_world, allShapes);
            //_breakableBody.Strength = 1;
            _body = _breakableBody.MainBody;
            _body.UserData = id;
            //_originalPosition = position.ToFarseer();
            CodeGenerator.AddCode("var {0} = BodyFactory.CreateBreakableBody(W, allShapes);", _body.n());
            CodeGenerator.AddCode(@"{0}.UserData = ""{1}"";", _body.n(), _body.UserData);
            _body.Position = new Vector2(rt.Translation.X, rt.Translation.Y);
            CodeGenerator.AddCode(@"{0}.Position = new Vector2({1},{2});", _body.n(), rt.Translation.X, rt.Translation.Y);
            _body.Rotation = rt.Angle;
            CodeGenerator.AddCode("{0}.Angle = {1};", _body.n(), rt.Angle);
        }

        public rotoTranslation RotoTranslation
        {
            get
            {
                var q = _breakableBody.MainBody.Position;
                return new rotoTranslation(new float2(q.X, q.Y), _body.Rotation);
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
            foreach (var b in _breakableBody.Parts)
            {
                yield return new BodyMaterial(b.Body, Id + ":" + i++);
            }
        }


        


    }


}
