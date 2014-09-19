using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FarseerPhysics.Samples.SM
{
    class HookableWalls
    {
        List<Body> _walls = new List<Body>();

        private void SimpleWall(World world, float density)
        {
            var wall = BodyFactory.CreateRectangle(world, 10, 1, density);
            wall.Position = new Vector2(0, -10);
            _walls.Add(wall);
        }
        private void LateralWall(World world, float density)
        {
            float w = 3;
            float h = 30;
            float lx = 25;
            float r = 0.2f;
            /*{
                var wall = BodyFactory.CreateRectangle(world, w, h, density);
                wall.Position = new Vector2(-lx, 0);
                wall.Restitution = r;
                _walls.Add(wall);
            }
            {
                var wall = BodyFactory.CreateRectangle(world, w, h, density);
                wall.Position = new Vector2(lx, 0);
                wall.Restitution = r;
                _walls.Add(wall);
            */

            var xml = XDocument.Load(@"C:\Users\Developer\Desktop\SVG\drawing3.svg");

            //xml.Descendants()

            // Query the data and write out a subset of contacts
            var q = from c in xml.Descendants()
                    where c.Name.LocalName == "rect" && c.Attribute("style").Value.Contains("008000")
                    select c;
            foreach (var rect in q)
            {
                var scale = 0.1f;
                var t = new Vector2(900, 500) * scale;
                var width = float.Parse(rect.Attribute("width").Value) * scale;
                var height = float.Parse(rect.Attribute("height").Value) * scale;

                var x = float.Parse(rect.Attribute("x").Value) * scale + width * 0.5f;
                var y = float.Parse(rect.Attribute("y").Value) * scale + height * 0.5f;

                var wall = BodyFactory.CreateRectangle(world, width, height, density);
                wall.Position = new Vector2(x, y) - t;
                wall.Restitution = r;
                _walls.Add(wall);

            }
        }


        public HookableWalls(World world, float density)
        {
            LateralWall(world, density);
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
