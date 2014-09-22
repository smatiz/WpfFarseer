using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfFarseer
{
    public class WpfDebugView
    {
        private static WpfDebugView _instance = new WpfDebugView();
        public static WpfDebugView Instance
        {
            get
            {
                return _instance;
            }
        }


        private List<BodyManager> _bodies = new List<BodyManager>();
        /*public World World
        {
            get;
            set;
        }*/

        public void Add(BodyManager body)
        {
            _bodies.Add(body);
        }

        


     
       

        public void Draw(DrawingContext drawingContext)
        {
            foreach(var b in _bodies)
            {
                b.Draw(drawingContext);


                
            }
        }
    }
}
