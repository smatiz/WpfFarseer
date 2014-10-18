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

        enum Status { Entire, MaterialBroking, ViewBroking, Broken }
        Status _status = Status.Entire;
        public void UpdateMaterial()
        {
            switch (_status)
            {
                case Status.Entire:
                    _rotoTranslation = BreakableBodyMaterial.RotoTranslation;
                    if (BreakableBodyMaterial.IsBroken)
                    {
                        _status = Status.MaterialBroking;
                        _bodyMaterials = BreakableBodyMaterial.GetPieces();
                        _status = Status.ViewBroking;
                    }
                    break;
                case Status.Broken:
                    foreach (var m in _bodyManagers)
                    {
                        m.UpdateMaterial();
                    }
                    break;
                default:
                    break;
            }


          
        }

        public void UpdateView()
        {

            switch (_status)
            {
                case Status.Entire:
                    BreakableBodyView.RotoTranslation = _rotoTranslation;
                    break;
                case Status.ViewBroking:
                    if (_bodyMaterials != null)
                    {
                        var bodyviews = BreakableBodyView.Break();
                        _bodyManagers = bodyviews.Zip(_bodyMaterials, (v, m) => new BodyManager(v, m)).ToArray();
                        _bodyMaterials = null;
                    }
                    _status = Status.Broken;
                    break;
                case Status.Broken:
                    foreach (var m in _bodyManagers)
                    {
                        m.UpdateView();
                    }
                    break;
                default:
                    break;
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
