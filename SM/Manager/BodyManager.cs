using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    // __IBodyView => 

    public enum __BodyType
    {
        Static,
        Kinematic,
        Dynamic,
        Breakable
    }

    public interface __IShape : IIdentifiable
    {
        IEnumerable<float2> Points_X { get; }
        float Density_X { get; }
    }

    public interface __IBodyView : IIdentifiable
    {
        __BodyType BodyType { get; }
        IEnumerable<IShape> Shapes { get; }
        rotoTranslation RotoTranslation { get; }
    }

    public interface __IBodyMaterial : IMaterial
    {
        void Build(string id, float2 position, SM.BodyType bodyType);
        rotoTranslation RotoTranslation { get; }
        void AddShape(IShape shape);
    }

    public interface IBodyView : IIdentifiable
    {
        BodyType BodyType { get; }
        float2 Position { get; }
        IEnumerable<IShape> Shapes_X { get; }
        rotoTranslation RotoTranslation { set; }
    }

    public interface IBodyMaterial : IMaterial
    {
        void Build(string id, float2 position, SM.BodyType bodyType); 
        rotoTranslation RotoTranslation { get; }
        void AddShape(IShape shape);
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
            foreach (var shape in _bodyView.Shapes_X)
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
