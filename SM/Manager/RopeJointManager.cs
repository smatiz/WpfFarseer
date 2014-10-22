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

    public interface ITwoPointJointView : IJointView
    {
        string TargetBodyIdA { get; }
        string TargetBodyIdB { get; }
        //string TargetFlagIdA { set; }
        //string TargetFlagIdB { set; }
        float2 AnchorA { get; set; }
        float2 AnchorB { get; set; }
    }

    public interface IRopeJointView : ITwoPointJointView
    {
    }

    public interface IRopeJointMaterial
    {
        void Build(string id, string targetNameA, float2 anchorA, string targetNameB, float2 anchorB);
        float2 AnchorA { get; }
        float2 AnchorB { get; }
    }

    public class RopeJointManager : IManager
    {
        IRopeJointView _ropeJointView;
        IRopeJointMaterial _ropeJointMaterial;

        float2 _anchorA, _anchorB;

        public RopeJointManager(IRopeJointView view, IRopeJointMaterial material)
        {
            _ropeJointView = view;
            _ropeJointMaterial = material;
        }


        public void UpdateMaterial()
        {
            _anchorA = _ropeJointMaterial.AnchorA;
            _anchorB = _ropeJointMaterial.AnchorB;
        }

        public void UpdateView()
        {
            _ropeJointView.AnchorA = _anchorA;
            _ropeJointView.AnchorB = _anchorB;
        }

        public void Build()
        {
            _ropeJointMaterial.Build(_ropeJointView.Id,
                 _ropeJointView.TargetBodyIdA, _ropeJointView.AnchorA,
               _ropeJointView.TargetBodyIdB, _ropeJointView.AnchorB);
        }

        public string Id
        {
            get
            {
                return _ropeJointView.Id;
            }
        }
    }
}
