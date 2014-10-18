using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IBreakableBodyView
    {
        IEnumerable<IShapeView> Shapes_Y { get; }
        rotoTranslation RotoTranslation { set; }
        IEnumerable<IBodyView> Break();
        string Id { get; }
    }

    public interface IBreakableBodyMaterial
    {
        void Build(string id, IEnumerable<IShapeView> shapes);
        rotoTranslation RotoTranslation { get; }
        bool IsBroken { get; }
        IEnumerable<IBodyMaterial> GetPieces();
    }

    public class BreakableBodyManager : IManager
    {
        rotoTranslation _rotoTranslation;
        BodyManager[] _bodyManagers = null;
        IEnumerable<IBodyMaterial> _bodyMaterials = null;

        public IBreakableBodyView BreakableBodyView { get; private set; }
        public IBreakableBodyMaterial BreakableBodyMaterial { get; private set; }



        public BreakableBodyManager(IBreakableBodyView breakableBodyView, IBreakableBodyMaterial breakableBodyMaterial)
        {
            BreakableBodyView = breakableBodyView;
            BreakableBodyMaterial = breakableBodyMaterial;
        }

        public void Build()
        {
            BreakableBodyMaterial.Build(BreakableBodyView.Id, BreakableBodyView.Shapes_Y);
        }

        public void UpdateMaterial()
        {
            if (_bodyMaterials == null && _bodyManagers == null)
            {
                _rotoTranslation = BreakableBodyMaterial.RotoTranslation;
                _bodyMaterials = BreakableBodyMaterial.GetPieces();
                if (BreakableBodyMaterial.IsBroken)
                {
                    _bodyMaterials = BreakableBodyMaterial.GetPieces();
                }
            }
            else
            {
                foreach (var m in _bodyManagers)
                {
                    m.UpdateMaterial();
                }
            }
        }

        public void UpdateView()
        {
            if (_bodyManagers == null)
            {
                BreakableBodyView.RotoTranslation = _rotoTranslation;
                if (_bodyMaterials != null)
                {
                    var bodyviews = BreakableBodyView.Break();
                    _bodyManagers = bodyviews.Zip(_bodyMaterials, (v, m) => new BodyManager(v, m)).ToArray();
                    _bodyMaterials = null;
                }
            }
            else
            {
                foreach(var m in _bodyManagers)
                {
                    m.UpdateView();
                }
            }
        }

        public string Id
        {
            get
            { 
                return BreakableBodyView.Id; 
            }
        }
    }
}
