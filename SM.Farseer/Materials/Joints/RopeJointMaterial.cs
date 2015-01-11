using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public class RopeJointMaterial : BasicJointMaterial, IRopeJointMaterial
    {
        protected FlagInfo _flagA;
        protected FlagInfo _flagB;

    

        public RopeJointMaterial(World world, JointInfo jointInfo, IEnumerable<FlagInfo> flagInfos)
            : base(world, jointInfo)
        {
            var targetNameA = ((RopeJointInfo)_jointInfo).TargetFlagIdA;
            var targetNameB = ((RopeJointInfo)_jointInfo).TargetFlagIdB;
            _flagA = flagInfos.FindFlagInfo(targetNameA);
            _flagB = flagInfos.FindFlagInfo(targetNameB);
        }

        protected override Joint CreateJoint(Materials material)
        {
            return JointFactory.CreateRopeJoint(_world, (Body)material.Find<BodyMaterial>(_flagA.Id.Parent).Object, (Body)material.Find<BodyMaterial>(_flagB.Id.Parent).Object, _flagA.P.ToFarseer(), _flagB.P.ToFarseer());
        }

        private Vector2 get(BasicManager basicManager, IdInfo name)
        {
            var flag = basicManager.FindObject<IFlag>(name);
            return new Vector2(flag.X, flag.Y);
        }

       

        public float2 AnchorA
        {
            get
            {
                return _joint.WorldAnchorA.ToSM();
            }
        }
        public float2 AnchorB
        {
            get
            {
                return _joint.WorldAnchorB.ToSM();
            }
        }
    }
}
