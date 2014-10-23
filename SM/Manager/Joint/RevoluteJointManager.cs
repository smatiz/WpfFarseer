using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IRevoluteJointView : ITwoPointJointView
    {
    }
    public interface IRevoluteJointMaterial : ITwoPointJointMaterial
    {
    }
    class RevoluteJointManager : TwoPointJointManager
    {
        public RevoluteJointManager(IRevoluteJointView view, IRevoluteJointMaterial material)
            : base(view, material)
        {
        }

        public override void Build()
        {
            base.Build();
        }
    }
}
