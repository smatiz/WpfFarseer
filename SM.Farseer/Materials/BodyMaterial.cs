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



    public class __BodyMaterial : __IBodyMaterial
    {
        // private Vector2 _originalPosition = Vector2.Zero;
        private World _world;
        private Body _body;

        public virtual object Object { get { return _body; } }
        public string Id { get { return (string)_body.UserData; } }

        public __BodyMaterial(World world)
        {
            _world = world;
        }
        public __BodyMaterial(Body body, string id)
        {
            _body = body;
            _body.UserData = id;
            _body.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;
            CodeGenerator.AddCode("{0}.UserData = {1};", _body.n(), _body.UserData);
            CodeGenerator.AddCode("{0}.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;", _body.n());
        }


        public void Build(string id, SM.__BodyType bodyType, rotoTranslation rt, IEnumerable<__IShape> shapes)
        {
            _body = BodyFactory.CreateBody(_world);
            _body.UserData = id;
            //_originalPosition = position.ToFarseer();
            CodeGenerator.AddCode("Body {0} = BodyFactory.CreateBody(W);", _body.n());
            _body.BodyType = (FarseerPhysics.Dynamics.BodyType)bodyType;
            CodeGenerator.AddCode(@"{0}.UserData = ""{1}"";", _body.n(), _body.UserData);
            CodeGenerator.AddCode("{0}.BodyType = BodyType.{1};", _body.n(), _body.BodyType.ToString());
            _body.Position = new Vector2(rt.Translation.X, rt.Translation.Y);
            CodeGenerator.AddCode(@"{0}.Position = new Vector2({1}f,{2}f);", _body.n(), rt.Translation.X, rt.Translation.Y);
            _body.Rotation = rt.Angle;
            CodeGenerator.AddCode("{0}.Rotation = {1}f;", _body.n(), rt.Angle);
            foreach (var shape in shapes)
            {
                AddShape(shape);
            }
        }

        public rotoTranslation RotoTranslation
        {
            get
            {
                var q = _body.Position;// -_originalPosition; 
                return new rotoTranslation(new float2(q.X, q.Y), _body.Rotation);
            }
        }

        private void addPolygon(float density, IEnumerable<float2> points)
        {
            var vs = points.ToFarseerVertices();
            FarseerPhysics.Factories.FixtureFactory.AttachPolygon(vs, density, _body);

            string shapeName = CodeGenerator.N("vs_");
            vs.AddCode(shapeName);
            CodeGenerator.AddCode("FarseerPhysics.Factories.FixtureFactory.AttachPolygon({0}, {1}, {2});", shapeName, density, _body.n());
       
        }
        private void AddShape(__IShape shape)
        {
            var circle = shape as ICircleShape;
            if (circle != null)
            {
                FarseerPhysics.Factories.FixtureFactory.AttachCircle(circle.Radius, shape.Density, _body, new Vector2(circle.X, circle.Y));
                string shapeName = CodeGenerator.N("vs_");
                CodeGenerator.AddCode("FarseerPhysics.Factories.FixtureFactory.AttachCircle({0}, {1}, {2});", circle.Radius, shape.Density, _body.n());
                return;
            }

            var ellipse = shape as IEllipseShape;
            if (ellipse != null)
            {
                FarseerPhysics.Factories.FixtureFactory.AttachPolygon(ellipse.ToVertices(), ellipse.Density, _body);
                string shapeName = CodeGenerator.N("vs_");
                CodeGenerator.AddCode("FarseerPhysics.Factories.FixtureFactory.AttachCircle({0}, {1}, {2});", circle.Radius, shape.Density, _body.n());
                return;
            }

            var poly = shape as IPolygonShape;
            if (poly != null)
            {
                addPolygon(shape.Density, poly.Points);
                return;
            }

            var polys = shape as IPolygonsShape;
            if (polys != null)
            {
                foreach(var x in polys.PolygonShapes)
                {
                    addPolygon(shape.Density, x);
                }
                return;
            }

        }
     }
}
