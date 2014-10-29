using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IJointView
    {
        string Id { get; }
    }


    public interface IJointMaterial : IMaterial
    {
        float Breakpoint { get; set; }
        bool CollideConnected { get; set; }
        float2 AnchorA { get; }
        float2 AnchorB { get; }
    }

    public interface ITwoPointJointView : IJointView
    {
        string TargetBodyIdA { get; }
        string TargetBodyIdB { get; }
        float2 AnchorA { get; set; }
        float2 AnchorB { get; set; }
    }


    public interface ITwoPointJointMaterial : IJointMaterial 
    {
        void Build(string id, string targetNameA, float2 anchorA, string targetNameB, float2 anchorB);
    }

    public class TwoPointJointManager : IManager
    { 
       protected ITwoPointJointView _jointView;
       protected ITwoPointJointMaterial _jointMaterial;

        float2 _anchorA, _anchorB;

        public TwoPointJointManager(ITwoPointJointView view, ITwoPointJointMaterial material)
        {
            _jointView = view;
            _jointMaterial = material;
        }


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
            _jointMaterial.Build(_jointView.Id,
                 _jointView.TargetBodyIdA, _jointView.AnchorA,
               _jointView.TargetBodyIdB, _jointView.AnchorB);
        }

        public string Id
        {
            get
            {
                return _jointView.Id;
            }
        }


        public new object Object
        {
            get
            { 
                return _jointMaterial.Object;
            }
        }
    }
}
