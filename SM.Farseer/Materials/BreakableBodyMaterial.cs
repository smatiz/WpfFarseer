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

    public class __BreakableBodyMaterial : __IBreakableBodyMaterial
    {
        World _world;
        Body _body;
        private BreakableBody _breakableBody;

        public virtual object Object { get { return _breakableBody; } }
        public string Id { get { return (string)_body.UserData; } }


        public __BreakableBodyMaterial(World world)
        {
            _world = world;
        }


        public void Build(string id, rotoTranslation rt, IEnumerable<__IShape> shapes)
        {
            var allShapes = shapes.SelectMany<__IShape, Shape>(x => ToShapes(x));


            _breakableBody = BodyFactory.CreateBreakableBody(_world, allShapes);
            //_breakableBody.Strength = 1;
            _body = _breakableBody.MainBody;
            _body.UserData = id;
            //_originalPosition = position.ToFarseer();
            CodeGenerator.AddCode("Body {0} = BodyFactory.CreateBody(W);", _body.n());
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



        public IEnumerable<__IBodyMaterial> GetPieces()
        {
            int i = 0;
            _breakableBody.Update();
            foreach (var b in _breakableBody.Parts)
            {
                yield return new __BodyMaterial(b.Body, Id + ":" + i++);
            }
        }


        private IEnumerable<Shape> ToShapes(__IShape shape)
        {
            var circle = shape as ICircleShape;
            if (circle != null)
            {
                yield return new CircleShape(circle.Radius, shape.Density);
            }

            var poly = shape as IPolygonShape;
            if (poly != null)
            {
                yield return new PolygonShape(poly.Points.ToFarseerVertices(), poly.Density);
            }

            var polys = shape as IPolygonsShape;
            if (polys != null)
            {
                foreach (var x in polys.PolygonShapes)
                {
                    yield return new PolygonShape(x.ToFarseerVertices(), polys.Density);
                }
            }
        }



    }




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
