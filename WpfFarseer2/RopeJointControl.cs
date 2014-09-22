using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfFarseer
{
    public class RopeJointControl : Canvas
    {
        public class Target
        {
            public Point Point { get; set; }
            public string Name { get; set; }
        }


        public IEnumerable<Target> Targets
        {
            get
            {
                foreach (var child in Children)
                {
                    var control = child as CrossControl;
                    if (control != null)
                    {
                        yield return new Target() { Name = control.TargetName, Point = control.PointToScreen(new Point()) };
                    }
                }
            }

        }
    }
}
