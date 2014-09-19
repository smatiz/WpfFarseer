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
    class XmlFarseer
    {
        Dictionary<string, object> _objects = new Dictionary<string, object>();
        public XmlFarseer(World world)
        {
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

                var wall = BodyFactory.CreateRectangle(world, width, height, 1);
                wall.Position = new Vector2(x, y) - t;
                

                _objects.Add(rect.Attribute("name").Value, wall);

                //_walls.Add(wall);

            }
        }
    }
}
