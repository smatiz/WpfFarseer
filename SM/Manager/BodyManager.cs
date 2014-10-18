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
        void Build(string id, SM.BodyType bodyType); 
        rotoTranslation RotoTranslation { get; }
        void AddShape(IEnumerable<IVector2> points, float density);
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
            //var shapeManagers = from shape in BodyView.Shapes_Y
            //                     select new ShapeManager
            //                         {
            //                             ShapeMaterial = BodyMaterial.Build(BodyView.Id, BodyView.BodyType),
            //                             ShapeView = shape,
            //                         };


            //// la parte successiva e' giusto venga fatta dentro il BodyMaterial o qui ??
            //// qui se riesco a evitare che un XxxMaterial contenga e conosca un YyyManager
            //// dentro nel ShapeManager
            //foreach (var sm in shapeManagers)
            //{
            //    sm.Build();
            //}


            BodyMaterial.Build(BodyView.Id, BodyView.BodyType);
            foreach (var shape in BodyView.Shapes_Y)
            {
                BodyMaterial.AddShape(shape.Points_X, shape.Density_X);

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
