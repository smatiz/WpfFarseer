using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class BreakableBodySynchronizer : ISynchronizer
    {
        private enum Status { Entire, MaterialBroking, ViewBroking, Broken }
        private Status _status = Status.Entire;

        private rotoTranslation _rotoTranslation;
        private IBreakableBodyView _breakableBodyView;
        private IBreakableBodyMaterial _breakableBodyMaterial;
        private IEnumerable<BodyPieceMaterial> _pieceMaterials;
        private IEnumerable<BodySynchronizer> _bodyPiecesSynchronizers;

        public BreakableBodySynchronizer(IBreakableBodyView breakableBodyView, IBreakableBodyMaterial breakableBodyMaterial)
        {
            _breakableBodyView = breakableBodyView;
            _breakableBodyMaterial = breakableBodyMaterial;
        }

        public void UpdateMaterial()
        {
            switch (_status)
            {
                case Status.Entire:
                    _rotoTranslation = _breakableBodyMaterial.RotoTranslation;
                    if (_breakableBodyMaterial.IsBroken)
                    {
                        _status = Status.MaterialBroking;
                        _pieceMaterials = _breakableBodyMaterial.GetPieces();
                        _status = Status.ViewBroking;
                    }
                    break;
                case Status.Broken:
                    foreach (var m in _bodyPiecesSynchronizers)
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
                    _breakableBodyView.Update();
                    break;
                case Status.ViewBroking:
                    if (_pieceMaterials != null)
                    {
                        var bodyviews = _breakableBodyView.BreakAndGetPieces(_pieceMaterials);
                        _bodyPiecesSynchronizers = bodyviews.Zip(_pieceMaterials.Select(x=>x.BodyMaterial), (v, m) => new BodySynchronizer(v, m));
                    }
                    _status = Status.Broken;
                    break;
                case Status.Broken:
                    foreach (var m in _bodyPiecesSynchronizers)
                    {
                        m.UpdateView();
                    }
                    break;
                default:
                    break;
            }
        }
        public bool IsBroken(out IEnumerable<BodySynchronizer> bodyPiecesSynchronizers)
        {
            bodyPiecesSynchronizers = _bodyPiecesSynchronizers;
            return _status == Status.Broken;
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
