using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class BodySynchronizer : ISynchronizer
    {
        rotoTranslation _rotoTranslationMem;
        IBodyView _bodyView;
        IBodyMaterial _bodyMaterial;

        public BodySynchronizer(IBodyView bodyView, IBodyMaterial bodyMaterial)
        {
            _bodyView = bodyView;
            _bodyMaterial = bodyMaterial;
        }

        public void UpdateMaterial()
        {
            _rotoTranslationMem = _bodyMaterial.RotoTranslation;
        }
        public void UpdateView()
        {
            _bodyView.RotoTranslation = _rotoTranslationMem;
            _bodyView.Update();
        }
        public IdInfo Id
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
