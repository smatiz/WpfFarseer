using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{

    public interface IBreakableBodyMaterial : IMaterial
    {
        void Build(string id, rotoTranslation rt, IEnumerable<IShape> shapes);
        rotoTranslation RotoTranslation { get; }
        bool IsBroken { get; }
        IEnumerable<IBodyMaterial> GetPieces();
    }


    public class BreakableBodyManager : IManager
    {
        rotoTranslation _rotoTranslation;
        public BodyManager[] BodyManagers { get; private set; }
        IEnumerable<IBodyMaterial> _bodyMaterials = null;

        IBodyView _breakableBodyView;
        IBreakableBodyMaterial _breakableBodyMaterial;

        public BreakableBodyManager(IBodyView breakableBodyView, IBreakableBodyMaterial breakableBodyMaterial)
        {
            _breakableBodyView = breakableBodyView;
            _breakableBodyMaterial = breakableBodyMaterial;
        }

        public void Build()
        {
            _breakableBodyMaterial.Build(_breakableBodyView.Id, _breakableBodyView.RotoTranslation, _breakableBodyView.Shapes);
        }

        enum Status { Entire, MaterialBroking, ViewBroking, Broken }
        Status _status = Status.Entire;
        //bool _added = false;
        public void UpdateMaterial()
        {
            switch (_status)
            {
                case Status.Entire:
                    _rotoTranslation = _breakableBodyMaterial.RotoTranslation;
                    if (_breakableBodyMaterial.IsBroken)
                    {
                        _status = Status.MaterialBroking;
                        _bodyMaterials = _breakableBodyMaterial.GetPieces();
                        _status = Status.ViewBroking;
                    }
                    break;
                case Status.Broken:

                    //if (!_added)
                    //{
                    //    foreach (var bm in _bodyManagers)
                    //    {
                    //        _manager.AddManager(bm);
                    //    }
                    //    _added = true;
                    //}
                    foreach (var m in BodyManagers)
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
                    _breakableBodyView.RotoTranslation = _rotoTranslation;
                    break;
                case Status.ViewBroking:
                    if (_bodyMaterials != null)
                    {
                        var bodyviews = _breakableBodyView.Break();
                        BodyManagers = bodyviews.Zip(_bodyMaterials, (v, m) => new BodyManager(v, m)).ToArray();
                        //Broken(_bodyManagers);
                        _bodyMaterials = null;
                    }

                    _status = Status.Broken;
                    break;
                case Status.Broken:
                    foreach (var m in BodyManagers)
                    {
                        m.UpdateView();
                    }
                    break;
                default:
                    break;
            }
        }

        public bool IsBroken
        {
            get
            {
                return _status == Status.Broken;
            }
        }

        public string Id
        {
            get
            {
                return _breakableBodyView.Id;
            }
        }

        public object Object
        {
            get
            {
                return _breakableBodyMaterial.Object;
            }
        }
    }

}
