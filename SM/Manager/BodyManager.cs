using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{

    public interface IBodyView : IIdentifiable
    {
        BodyType BodyType { get; }
        float2 Position { get; }
        IEnumerable<IShapeView> Shapes_Y { get; }
        rotoTranslation RotoTranslation { set; }
    }

    public interface IBodyMaterial : IMaterial
    {
        void Build(string id, float2 position, SM.BodyType bodyType); 
        rotoTranslation RotoTranslation { get; }
        void AddShape(IShapeView shape);
    }

    public class BodyManager : IManager, IMaterial
    {
        rotoTranslation _rotoTranslation;

        IBodyView _bodyView;
        IBodyMaterial _bodyMaterial;

        public BodyManager(IBodyView bodyView, IBodyMaterial bodyMaterial)
        {
            _bodyView = bodyView;
            _bodyMaterial = bodyMaterial;
        }

        public void Build()
        {
            _bodyMaterial.Build(_bodyView.Id, _bodyView.Position, _bodyView.BodyType);
            foreach (var shape in _bodyView.Shapes_Y)
            {
                _bodyMaterial.AddShape(shape);

            }
        }

        public void UpdateMaterial()
        {
            _rotoTranslation = _bodyMaterial.RotoTranslation;
        }
        public void UpdateView()
        {
            _bodyView.RotoTranslation = _rotoTranslation;
        }

        public string Id
        {
            get
            { 
                return _bodyView.Id; 
            }
        }

        public object Object
        {
            get { return _bodyMaterial.Object; }
        }
    }
}
