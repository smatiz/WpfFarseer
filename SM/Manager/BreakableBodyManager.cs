﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IBreakableBodyView
    {
        IEnumerable<IShape> Shapes_X { get; }
        rotoTranslation RotoTranslation { set; }
        IEnumerable<IBodyView> Break();
        string Id { get; }
    }

    public interface __IBreakableBodyMaterial : IMaterial
    {
        void Build(string id, rotoTranslation rt, IEnumerable<__IShape> shapes);
        rotoTranslation RotoTranslation { get; }
        bool IsBroken { get; }
        IEnumerable<__IBodyMaterial> GetPieces();
    }

    public interface IBreakableBodyMaterial : IMaterial
    {
        void Build(string id, IEnumerable<IShape> shapes);
        rotoTranslation RotoTranslation { get; }
        bool IsBroken { get; }
        IEnumerable<IBodyMaterial> GetPieces();
    }

    public class __BreakableBodyManager : IManager
    {
        rotoTranslation _rotoTranslation;
        __BodyManager[] _bodyManagers = null;
        IEnumerable<__IBodyMaterial> _bodyMaterials = null;

        __IBodyView _breakableBodyView;
        __IBreakableBodyMaterial _breakableBodyMaterial;

        public __BreakableBodyManager(__IBodyView breakableBodyView, __IBreakableBodyMaterial breakableBodyMaterial)
        {
            _breakableBodyView = breakableBodyView;
            _breakableBodyMaterial = breakableBodyMaterial;
        }

        public void Build()
        {
            _breakableBodyMaterial.Build(_breakableBodyView.Id, _breakableBodyView.RotoTranslation, _breakableBodyView.Shapes_X);
        }

        enum Status { Entire, MaterialBroking, ViewBroking, Broken }
        Status _status = Status.Entire;
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
                    _breakableBodyView.RotoTranslation = _rotoTranslation;
                    break;
                case Status.ViewBroking:
                    if (_bodyMaterials != null)
                    {
                        var bodyviews = _breakableBodyView.Break();
                        _bodyManagers = bodyviews.Zip(_bodyMaterials, (v, m) => new __BodyManager(v, m)).ToArray();
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



    public class BreakableBodyManager : IManager
    {
        rotoTranslation _rotoTranslation;
        BodyManager[] _bodyManagers = null;
        IEnumerable<IBodyMaterial> _bodyMaterials = null;

        IBreakableBodyView _breakableBodyView;
        IBreakableBodyMaterial _breakableBodyMaterial;

        public BreakableBodyManager(IBreakableBodyView breakableBodyView, IBreakableBodyMaterial breakableBodyMaterial)
        {
            _breakableBodyView = breakableBodyView;
            _breakableBodyMaterial = breakableBodyMaterial;
        }

        public void Build()
        {
            _breakableBodyMaterial.Build(_breakableBodyView.Id, _breakableBodyView.Shapes_X);
        }

        enum Status { Entire, MaterialBroking, ViewBroking, Broken }
        Status _status = Status.Entire;
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
                    _breakableBodyView.RotoTranslation = _rotoTranslation;
                    break;
                case Status.ViewBroking:
                    if (_bodyMaterials != null)
                    {
                        var bodyviews = _breakableBodyView.Break();
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
