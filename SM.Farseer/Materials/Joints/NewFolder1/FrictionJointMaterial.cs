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
    class FrictionJointMaterial : BasicJointMaterial
    {

        FrictionJoint __joint;

        public FrictionJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }


        public void Build(string id, string targetNameA, string targetNameB, float2 anchorA)
        {
            _id = id;
            _joint = Build(_farseerWorldManager.Find<Body>(targetNameA), _farseerWorldManager.Find<Body>(targetNameB), anchorA.ToFarseer());
        }

        Joint Build(Body targetA, Body targetB, Vector2 anchorA)
        {
            __joint = JointFactory.CreateFrictionJoint(_world, targetA, targetB, anchorA);
            return __joint;
        }

    }
}
