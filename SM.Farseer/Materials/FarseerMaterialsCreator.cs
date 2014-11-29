﻿using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public class FarseerMaterialsCreator : IMaterialCreator
    {
        private World _world;
        public FarseerMaterialsCreator(World world)
        {
            _world = world;
        }


        public IMaterial Create(BodyInfo body, IShapeMaterialCreator shapeCreator)
        {
            if (body.BodyType == BodyType.Breakable)
            {
                return new BreakableBodyMaterial(_world, body, shapeCreator);
            }
            else
            {
                return new BodyMaterial(_world, body, shapeCreator);
            }
        }

        public IJointMaterial Create(IJoint body)
        {
            throw new NotImplementedException();
        }


    }
}