using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    //public interface IWeldJointView : ITwoPointJointView
    //{
    //    float ReferenceAngle { get; }
    //    float FrequencyHz{ get; }
    //    float DampingRatio { get; }
    //}
    //public interface IWeldJointMaterial : ITwoPointJointMaterial
    //{
    //    float ReferenceAngle { set; }
    //    float FrequencyHz { set; }
    //    float DampingRatio { set; }
    //}


    //class WeldJointManager : TwoPointJointManager
    //{
    //    public WeldJointManager(IWeldJointView view, IWeldJointMaterial material)
    //        : base(view, material)
    //    {
    //    }

    //    public override void Build()
    //    {
    //        base.Build();
    //        var weldJointView = (IWeldJointView)_jointView;
    //        var weldJointMaterial = (IWeldJointMaterial)_jointView;

    //        weldJointMaterial.ReferenceAngle = weldJointView.ReferenceAngle;
    //        weldJointMaterial.FrequencyHz = weldJointView.FrequencyHz;
    //        weldJointMaterial.DampingRatio = weldJointView.DampingRatio;
    //    }
    //}
}
