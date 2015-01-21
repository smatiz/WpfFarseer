using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using SM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SM.Farseer
{
   
    public class FarseerWorldManager : BasicManager
    {
        World _world;
        
        [Conditional("DEBUG")]
        public void GetWorld(ref World world) { world =  _world;  }


        public override void Clear()
        {
            _world.Clear();
            base.Clear();
        }

        protected override void Step(float dt)
        {
            _world.Step(dt);
        }

        protected override void Loop()
        {
        }

        public FarseerWorldManager(string id, Synchronizers synchronizers, IWatchView viewWatch, World world)
            : base(synchronizers, viewWatch)
        {
            Id = id;
            _world = world;
        }

        public string Id { get; private set; }

        //public static Materials CreateMaterials(World world, IShapeMaterialCreator farseerMaterialsShapeCreator)
        //{
        //    // creo un farseer materials creator che Materials pilotera' e usera' per popolare le sue strutture a partire da info
        //    var farseerMaterialsCreator = new FarseerMaterialsCreator(world);
        //    // Materials e' completamente agnostico 
        //    return new Materials(farseerMaterialsCreator, farseerMaterialsShapeCreator);
        //}
    }
}
