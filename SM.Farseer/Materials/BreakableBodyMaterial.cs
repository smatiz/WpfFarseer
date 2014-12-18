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
    public class BreakableBodyMaterial : BasicBodyMaterial, IBreakableBodyMaterial
    {
        private BreakableBody _breakableBody;
        IEnumerable<BodyPieceMaterial> _pieces;
        protected override Body Body
        {
            get
            {
                return _breakableBody.MainBody;
            }
        }
        public BreakableBodyMaterial(World world, BodyInfo bodyInfo, IShapeMaterialCreator shapeCreator)
        {
            var materialShape = shapeCreator.Create(bodyInfo.Body.Shapes, bodyInfo.Transform.Scale);
            IEnumerable<Shape> allShapes = materialShape.Circles.Select(c => getCircle(c)).Concat<Shape>(materialShape.Polygons.Select(p => getPolygon(p)));

            _breakableBody = BodyFactory.CreateBreakableBody(world, allShapes);
            Body.UserData = bodyInfo.Id;
            CodeGenerator.AddCode("var {0} = BodyFactory.CreateBreakableBody(W, allShapes);", Body.n());
            CodeGenerator.AddCode(@"{0}.UserData = ""{1}"";", Body.n(), Body.UserData);
            Body.Position = new Vector2(bodyInfo.Transform.RotoTranslation.Translation.X, bodyInfo.Transform.RotoTranslation.Translation.Y);
            CodeGenerator.AddCode(@"{0}.Position = new Vector2({1},{2});", Body.n(), bodyInfo.Transform.RotoTranslation.Translation.X, bodyInfo.Transform.RotoTranslation.Translation.Y);
            Body.Rotation = bodyInfo.Transform.RotoTranslation.Angle;
            CodeGenerator.AddCode("{0}.Angle = {1};", Body.n(), bodyInfo.Transform.RotoTranslation.Angle);
        }
        public virtual object Object { get { return _breakableBody; } }
        public bool IsBroken
        {
            get
            {
                return _breakableBody.Broken;
            }
        }
        private List<BodyPieceMaterial> getPieces()
        {
            var result = new List<BodyPieceMaterial>();
            int index = 0;
            _breakableBody.Update();
            foreach (var b in _breakableBody.Parts)
            {
                result.Add(new BodyPieceMaterial()
                {
                    BodyMaterial = new BodyMaterial(b.Body, String.Format("{0}_p{1}",Id, index++)),
                    Polygon = ((PolygonShape)b.Body.FixtureList.First().Shape).Vertices.ToSM()
                });
            }
            return result;
        }
        public IEnumerable<BodyPieceMaterial> GetPieces()
        {
            if(_pieces == null)
            {
                _pieces = getPieces();
            }
            return _pieces;
        }
    }
}
