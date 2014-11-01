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
    class PrismaticJointMaterial : BasicAxedJointMaterial
    {
        PrismaticJoint __joint;

        public PrismaticJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        protected override Joint Build(Body targetA, Body targetB, Vector2 anchor, Vector2 axis)
        {
            __joint = JointFactory.CreatePrismaticJoint(_world, targetA, targetB, anchor, axis);
            return __joint;
        }
    }
}
