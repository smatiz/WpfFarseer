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
    abstract class BasicAxedJointMaterial : BasicJointMaterial
    {
        public BasicAxedJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        public void Build(string id, string targetNameA, string targetNameB, float2 anchor, float2 axis)
        {
            _id = id;
            _joint = Build(_farseerWorldManager.Find<Body>(targetNameA), _farseerWorldManager.Find<Body>(targetNameB), anchor.ToFarseer(), axis.ToFarseer());
        }

        protected abstract Joint Build(Body targetA, Body targetB, Vector2 anchor, Vector2 axis);
    }
}
