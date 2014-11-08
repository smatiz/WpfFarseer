﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IBodyView : IIdentifiable
    {
        BodyType BodyType { get; }
        List<IShape> Shapes { get; }
        rotoTranslation RotoTranslation { get; set; }

        IEnumerable<IBodyView> Break();
    }

    public interface IBodyMaterial : IMaterial
    {
        void Build(string id, SM.BodyType bodyType, rotoTranslation rt, IEnumerable<IShape> shapes);
        rotoTranslation RotoTranslation { get; }
    }

    public class BodyManager : IManager, IMaterial
    {
        rotoTranslation _rotoTranslationMaterial;

        IBodyView _bodyView;
        IBodyMaterial _bodyMaterial;

        public BodyManager(IBodyView bodyView, IBodyMaterial bodyMaterial)
        {
            _bodyView = bodyView;
            _bodyMaterial = bodyMaterial;
        }

        public void Build()
        {
            _bodyMaterial.Build(_bodyView.Id, _bodyView.BodyType, _bodyView.RotoTranslation, _bodyView.Shapes);
        }

        public void UpdateMaterial()
        {
            _rotoTranslationMaterial = _bodyMaterial.RotoTranslation;
        }
        public void UpdateView()
        {
            _bodyView.RotoTranslation = _rotoTranslationMaterial;
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
