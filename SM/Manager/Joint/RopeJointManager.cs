using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{

    public interface IRopeJointView : ITwoPointJointView
    {
        float MaxLength { get; }
        float MaxLengthFactor { get; }
    }
    public interface IRopeJointMaterial : ITwoPointJointMaterial
    {
        float MaxLength { get;  set; }
    }

    public class RopeJointManager : TwoPointJointManager
    {
        public RopeJointManager(IRopeJointView view, IRopeJointMaterial material)
            : base(view, material)
        {
        }

        public override void Build()
        {
            base.Build();

            var ropeJointView = (IRopeJointView)_jointView;
            var ropeJointMaterial = (IRopeJointMaterial)_jointMaterial;

            if (ropeJointView.MaxLength != -1)
            {
                ropeJointMaterial.MaxLength = ropeJointView.MaxLength;
            }
            else if (ropeJointView.MaxLengthFactor != -1)
            {
                ropeJointMaterial.MaxLength *= ropeJointView.MaxLengthFactor;
            }
        }
    }
}
