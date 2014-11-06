using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{

    public interface IRopeJointView : IJointView
    {
        string TargetBodyIdA { get; }
        string TargetBodyIdB { get; }
        float2 AnchorA { get; set; }
        float2 AnchorB { get; set; }
        float MaxLength { get; }
        float MaxLengthFactor { get; }
    }
    public interface IRopeJointMaterial : IJointMaterial
    {
        float MaxLength { get; set; }
        float2 AnchorA { get; set; }
        float2 AnchorB { get; set; }
        void Build(string id);

        string TargetNameA { get; set; }
        string TargetNameB { get; set; }
    }

    public class RopeJointManager : IManager
    {
        protected IRopeJointView _jointView;
        protected IRopeJointMaterial _jointMaterial;
        public RopeJointManager(IRopeJointView view, IRopeJointMaterial material)
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
        }

        public virtual void Build()
        {
            var ropeJointView = (IRopeJointView)_jointView;
            var ropeJointMaterial = (IRopeJointMaterial)_jointMaterial;

            ropeJointMaterial.TargetNameA = ropeJointView.TargetBodyIdA;
            ropeJointMaterial.AnchorA = ropeJointView.AnchorA;
            ropeJointMaterial.TargetNameB = ropeJointView.TargetBodyIdB;
            ropeJointMaterial.AnchorB = ropeJointView.AnchorB;


            _jointMaterial.Build(_jointView.Id);


            if (ropeJointView.MaxLength != -1)
            {
                ropeJointMaterial.MaxLength = ropeJointView.MaxLength;
            }
            else if (ropeJointView.MaxLengthFactor != -1)
            {
                ropeJointMaterial.MaxLength *= ropeJointView.MaxLengthFactor;
            }
        }

        public string Id
        {
            get
            {
                return _jointView.Id;
            }
        }


        public object Object
        {
            get
            { 
                return _jointMaterial.Object;
            }
        }

    }
}
