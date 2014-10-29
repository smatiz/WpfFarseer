using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    abstract class BasicUnanchoredJointMaterial : BasicJointMaterial
    {
        public BasicUnanchoredJointMaterial(World world, FarseerWorldManager farseerWorldManager)
            : base(world, farseerWorldManager)
        {
        }

        public void Build(string id, string targetNameA, string targetNameB)
        {
            _id = id;
            _joint = Build(_farseerWorldManager.Find<Body>(targetNameA), _farseerWorldManager.Find<Body>(targetNameB));
        }

        protected abstract Joint Build(Body targetA, Body targetB);
    }
}
