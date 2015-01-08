using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class RopeJointSynchronizer : ISynchronizer
    {
        protected IRopeJointView _jointView;
        protected IRopeJointMaterial _jointMaterial;
        public RopeJointSynchronizer(IRopeJointView view, IRopeJointMaterial material)
        {
            _jointView = view;
            _jointMaterial = material;
        }

        float2 _anchorA, _anchorB;



        public void UpdateMaterial()
        {
            _anchorA = _jointMaterial.AnchorA;
            _anchorB = _jointMaterial.AnchorB;
        }

        public void UpdateView()
        {
            _jointView.AnchorA = _anchorA;
            _jointView.AnchorB = _anchorB;
            _jointView.Update();
        }

        //public IdInfo Id
        //{
        //    get
        //    {
        //        return _jointView.Id;
        //    }
        //}


        //public object Object
        //{
        //    get
        //    { 
        //        return _jointMaterial.Object;
        //    }
        //}

    }
}
