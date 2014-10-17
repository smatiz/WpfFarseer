using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IBodyView
    {
        IEnumerable<IShapeView> Shapes_X { get; }
        rotoTranslation RotoTranslation { set; }
    }

    public interface IBodyMaterial
    {
        IEnumerable<ShapeManager> Build(IEnumerable<IShapeView> shapes); 
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
            var shapeMaterials = BodyMaterial.Build(BodyView.Shapes_X);


            // la parte successiva e' giusto venga fatta dentro il BodyMaterial o qui ??
            foreach (var shapeMaterial in shapeMaterials)
            {
                shapeMaterial.Build();
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
            get { throw new NotImplementedException(); }
        }
    }
}
