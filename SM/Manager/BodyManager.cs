using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IBodyView : IIdentifiable
    {
        __BodyType BodyType { get; }
        IEnumerable<__IShape> Shapes_X { get; }
        rotoTranslation RotoTranslation { get; set; }

        IEnumerable<IBodyView> Break();
    }

    public interface IBodyMaterial : IMaterial
    {
        void Build(string id, SM.__BodyType bodyType, rotoTranslation rt, IEnumerable<__IShape> shapes);
        rotoTranslation RotoTranslation { get; }
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
            _bodyMaterial.Build(_bodyView.Id, _bodyView.BodyType, _bodyView.RotoTranslation, _bodyView.Shapes_X);
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
