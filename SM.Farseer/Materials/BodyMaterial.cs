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
    public class BodyMaterial : BasicBodyMaterial, IBodyMaterial
    {
        private Body _body;

        protected override Body Body
        {
            get
            {
                return _body;
            }
        }

        public BodyMaterial(World world, BodyInfo bodyInfo, IShapeMaterialCreator shapeCreator)
        {
            _body = BodyFactory.CreateBody(world);
            _body.UserData = bodyInfo.Id;
            CodeGenerator.AddCode("Body {0} = BodyFactory.CreateBody(W);", _body.n());
            _body.BodyType = (FarseerPhysics.Dynamics.BodyType)bodyInfo.Body.BodyType;
            CodeGenerator.AddCode(@"{0}.UserData = ""{1}"";", _body.n(), _body.UserData);
            CodeGenerator.AddCode("{0}.BodyType = BodyType.{1};", _body.n(), _body.BodyType.ToString());
            _body.Position = new Vector2(bodyInfo.Transform.RotoTranslation.Translation.X, bodyInfo.Transform.RotoTranslation.Translation.Y);
            CodeGenerator.AddCode(@"{0}.Position = new Vector2({1}f,{2}f);", _body.n(), bodyInfo.Transform.RotoTranslation.Translation.X, bodyInfo.Transform.RotoTranslation.Translation.Y);
            _body.Rotation = bodyInfo.Transform.RotoTranslation.Angle;
            CodeGenerator.AddCode("{0}.Rotation = {1}f;", _body.n(), bodyInfo.Transform.RotoTranslation.Angle);

            var materialShape = shapeCreator.Create(bodyInfo.Body.Shapes, bodyInfo.Transform.Scale);

            foreach (var shape in materialShape.Polygons)
            {
                _body.CreateFixture(getPolygon(shape));
            }
            foreach (var shape in materialShape.Circles)
            {
                _body.CreateFixture(getCircle(shape));
            }
        }
        public BodyMaterial(Body body, string id)
        {
            _body = body;
            _body.UserData = id;
            _body.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;
            CodeGenerator.AddCode("{0}.UserData = {1};", _body.n(), _body.UserData);
            CodeGenerator.AddCode("{0}.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;", _body.n());
        }

        public virtual object Object { get { return _body; } }
     }
}
