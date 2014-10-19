using SM;
using SM.Wpf;
using SM.Farseer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Media;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace SM.WpfFarseer
{
    public class XamlInterpreter
    {

        public static void BuildFarseerWorldManager(FarseerWorldManager worldManager, IEnumerable<BasicControl> objects, bool isInDesignMode = false)
        {
            CodeGenerator.Header = "Farseer Code Generator" + " : " + worldManager.Id;

            var tobeadded = new List<UIElement>();

            foreach (var child in objects)
            {
                bool handled = false;
                if (!isInDesignMode)
                {
                    var breakableBodyControl = child as BreakableBodyControl;
                    if (breakableBodyControl != null)
                    {
                        var autoBreakableBodyControl = child as AutoBreakableBodyControl;
                        if (autoBreakableBodyControl != null)
                        {
                            var polyF = autoBreakableBodyControl.Shape.Points.ToFarseerVertices();
                            var vss = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(polyF, (FarseerPhysics.Common.Decomposition.TriangulationAlgorithm)autoBreakableBodyControl.TriangulationAlgorithm);
                            var bbc = new BreakableBodyControl();

                            foreach (var p in vss)
                            {
                                var bbcp = new BreakableBodyPartControl();
                                bbcp.Shape.Points = p.ToWpf();
                                bbc.Parts.Add(bbcp);
                            }

                            breakableBodyControl = bbc;
                            //autoBreakableBodyControl._canvas.Visibility = System.Windows.Visibility.Hidden;
                            //tobeadded.Add(breakableBodyControl._canvas);
                        }

                        if (breakableBodyControl != null)
                        {
                            worldManager.AddBreakableBodyView(breakableBodyControl);
                            handled = true;
                        }
                    }
                    if (!handled)
                    {
                        var bodyControl = child as BodyControl;
                        if (bodyControl != null)
                        {
                            worldManager.AddBodyView(bodyControl);

                            //_worldManager.AddBodyControl(bodyControl, bodyControl._canvas.GetOrigin());
                            handled = true;
                        }
                    }
                }

                //if (!handled)
                //{
                //    var jointControl = child as RopeJointControl;
                //    if (jointControl != null)
                //    {
                //        var ropeJointControlInfo = _resolve(jointControl);

                //        var line = new Line();
                //        line.Stroke = new SolidColorBrush(Colors.Green);
                //        line.StrokeThickness = 1;
                //        tobeadded.Add(line);

                //        _ropeJointManager.Add(new TwoPointJointControlManager(this, ropeJointControlInfo, line));

                //        if (!isInDesignMode)
                //        {
                //            _worldManager.AddRopeJoint(jointControl);
                //        }
                //    }
                //}
            }

            foreach (var tba in tobeadded)
            {
                //Children.Add(tba);
            }


          
        }
    }
}
