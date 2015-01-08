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

        public RopeJointMaterial(World world, JointInfo jointInfo, Info info)
            : base(world, jointInfo)
        {
            var targetNameA = ((IRopeJoint)_jointInfo).TargetFlagIdA;
            var targetNameB = ((IRopeJoint)_jointInfo).TargetFlagIdB;
             _flagA = info.FindFlag(targetNameA);
             _flagB = info.FindFlag(targetNameB);
        }

        protected override Joint CreateJoint(Materials material)
        {
            return JointFactory.CreateRopeJoint(_world, (Body)material.Find<BodyMaterial>(_flagA.Id.Parent).Object, (Body)material.Find<BodyMaterial>(_flagB.Id.Parent).Object, ToFarseer(_flagA), ToFarseer(_flagB));
        }

        private Vector2 get(BasicManager basicManager, IdInfo name)
        {
            var flag = basicManager.FindObject<IFlag>(name);
            return new Vector2(flag.X, flag.Y);
        }

        Vector2 ToFarseer(FlagInfo f)
        {
            return new Vector2(f.Flag.X, f.Flag.Y);
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
