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
    public class BodyMaterial : IBodyMaterial
    {
        private Vector2 _originalPosition = Vector2.Zero;
        private Body _body;

        public virtual object Object { get { return _body; } }
        public string Id { get { return (string)_body.UserData; } }

        public BodyMaterial(World world)
        {
            _body = BodyFactory.CreateBody(world);
        }
        public BodyMaterial(Body body, string id)
        {
            _body = body;
            _body.UserData = id;
            _body.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;
            CodeGenerator.AddCode("{0}.UserData = {1};", _body.n(), _body.UserData);
            CodeGenerator.AddCode("{0}.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;", _body.n());
        }


        public void Build(string id, float2 position, SM.BodyType bodyType)
        {
            _body.UserData = id;
            _originalPosition = position.ToFarseer();
            CodeGenerator.AddCode("Body {0} = BodyFactory.CreateBody(W);", _body.n());
            _body.BodyType = (FarseerPhysics.Dynamics.BodyType)bodyType;
            CodeGenerator.AddCode(@"{0}.UserData = ""{1}"";", _body.n(), _body.UserData);
            CodeGenerator.AddCode("{0}.BodyType = BodyType.{1};", _body.n(), _body.BodyType.ToString());
            //_body.Position = originPosition;
            //_originalPosition = originPosition;
        }

        public rotoTranslation RotoTranslation
        {
            get
            {
                var q = _body.Position - _originalPosition; 
                return new rotoTranslation(new float2(q.X, q.Y), _body.Rotation);
            }
        }

        public void AddShape(IShape shape)
        {
            var vs = shape.Points_X.ToFarseerVertices();
            FarseerPhysics.Factories.FixtureFactory.AttachPolygon(vs, shape.Density_X, _body);

            string shapeName = CodeGenerator.N("vs_");
            vs.AddCode(shapeName);
            CodeGenerator.AddCode("FarseerPhysics.Factories.FixtureFactory.AttachPolygon({0}, {1}, {2});", shapeName, shape.Density_X, _body.n());
        }
    }
}
