using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    // __IBodyView => 


   
    public interface __IBodyView : IIdentifiable
    {
        __BodyType BodyType { get; }
        IEnumerable<__IShape> Shapes_X { get; }
        rotoTranslation RotoTranslation { get; set; }
    }

    public interface __IBodyMaterial : IMaterial
    {
        void Build(string id, SM.__BodyType bodyType);
        rotoTranslation RotoTranslation { get; }
        void AddShape(__IShape shape);
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


    public class __BodyManager : IManager, IMaterial
    {
        rotoTranslation _rotoTranslation;

        __IBodyView _bodyView;
        __IBodyMaterial _bodyMaterial;

        public __BodyManager(__IBodyView bodyView, __IBodyMaterial bodyMaterial)
        {
            _bodyView = bodyView;
            _bodyMaterial = bodyMaterial;
        }

        public void Build()
        {
            _bodyMaterial.Build(_bodyView.Id, _bodyView.BodyType);
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
