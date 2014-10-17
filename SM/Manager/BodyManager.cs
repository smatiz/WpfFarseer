using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IBodyView
    {
        BodyType BodyType { get; }
        IEnumerable<IShapeView> Shapes_Y { get; }
        rotoTranslation RotoTranslation { set; }
        string Id { get; }
    }

    public interface IBodyMaterial
    {
        IShapeMaterial Build(string id, SM.BodyType bodyType, IShapeView shapes); 
        rotoTranslation RotoTranslation { get; }
    }

    public class BodyManager : IManager
    {
        rotoTranslation RotoTranslation;

        public IBodyView BodyView { get; private set; }
        public IBodyMaterial BodyMaterial { get; private set; }

        public BodyManager(IBodyView bodyView, IBodyMaterial bodyMaterial)
        {
            BodyView = bodyView;
            BodyMaterial = bodyMaterial;
        }

        public void Build()
        {
            var shapes = from shape in BodyView.Shapes_Y
                                 select new
                                     {
                                         ShapeMaterial = BodyMaterial.Build(BodyView.Id, BodyView.BodyType, shape),
                                         ShapeView = shape,
                                     };


            // la parte successiva e' giusto venga fatta dentro il BodyMaterial o qui ??
            // qui se riesco a evitare che un XxxMaterial contenga e conosca un YyyManager
            foreach (var shape in shapes)
            {
                shape.ShapeMaterial.Build(shape.ShapeView.Points_X, shape.ShapeView.Density_X);
            }
        }

        public void UpdateMaterial()
        {
            RotoTranslation = BodyMaterial.RotoTranslation;
        }
        public void UpdateView()
        {
            BodyView.RotoTranslation = RotoTranslation;
        }

        public string Id
        {
            get
            { 
                return BodyView.Id; 
            }
        }
    }
}
