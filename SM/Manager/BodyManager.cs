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
        void AddShape(IShapeView shape);
    }

    public class BodyManager : IManager
    {
        rotoTranslation _rotoTranslation;

        public IBodyView BodyView { get; private set; }
        public IBodyMaterial BodyMaterial { get; private set; }

        public BodyManager(IBodyView bodyView, IBodyMaterial bodyMaterial)
        {
            BodyView = bodyView;
            BodyMaterial = bodyMaterial;
        }

        public void Build()
        {
            BodyMaterial.Build(BodyView.Id, BodyView.BodyType);
            foreach (var shape in BodyView.Shapes_Y)
            {
                BodyMaterial.AddShape(shape);

            }
        }

        public void UpdateMaterial()
        {
            _rotoTranslation = BodyMaterial.RotoTranslation;
        }
        public void UpdateView()
        {
            BodyView.RotoTranslation = _rotoTranslation;
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
