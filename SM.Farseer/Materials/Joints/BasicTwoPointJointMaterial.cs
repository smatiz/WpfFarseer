using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public abstract class BasicTwoPointJointMaterial : BasicJointMaterial, ITwoPointJointMaterial
    {
        public BasicTwoPointJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        public void Build(string id, string targetNameA, float2 anchorA, string targetNameB, float2 anchorB)
        {
            _id = id;
            _joint = Build(
                _farseerWorldManager.Find<Body>(targetNameA), anchorA.ToFarseer(),
                _farseerWorldManager.Find<Body>(targetNameB), anchorB.ToFarseer());
        }

        protected abstract Joint Build(Body targetA, Vector2 anchorA, Body targetB, Vector2 anchorB);
    }
}