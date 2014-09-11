using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarseerPhysics.Samples.SM
{
    class HookableWalls
    {
        List<Body> _walls = new List<Body>();

        public HookableWalls(World world, float density)
        {
            var wall = BodyFactory.CreateRectangle(world, 10, 1, density);
            wall.Position = new Vector2(0, -10);
            _walls.Add(wall);
        }

        internal Body GetHook(Vector2 p)
        {
            foreach (var w in _walls)
            {
                foreach (var f in w.FixtureList)
                {
                    if (f.TestPoint(ref p))
                    {
                        return w;
                    }
                }
            }
            return null;
        }
    }
}
