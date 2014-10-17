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

namespace SM.WpfFarseer
{

    public class WorldManager_X : WorldManager
    {
        protected override void Step(float dt)
        {
            throw new NotImplementedException();
        }

        protected override void Loop()
        {
            throw new NotImplementedException();
        }

        public WorldManager_X(IViewWatch viewWatch)
            : base(viewWatch)
        {
         }

    }
    class XamlInterpreter
    {

        public static void xxx(Dispatcher dispacher, IEnumerable<BasicControl> objects, bool isInDesignMode = false)
        {
            WorldManager _worldManager = new WorldManager_X(new ViewWatch(dispacher));



            var tobeadded = new List<UIElement>();

            foreach (var child in objects)
            {
                bool handled = false;
                if (!isInDesignMode)
                {

                    {
                        var breakableBodyControl = child as BreakableBodyControl;
                        if (breakableBodyControl == null)
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
                                autoBreakableBodyControl._canvas.Visibility = System.Windows.Visibility.Hidden;
                                tobeadded.Add(breakableBodyControl._canvas);
                            }
                        }

                        //if (breakableBodyControl != null)
                        //{
                        //    // var shapes = breakableBodyControl.Parts.SelectMany<BodyControl, FShape.Shape>(c => (from x in c.Shapes select breakableBodyControl.ToFarseerShape(x)));

                        //    var shapes = from x in breakableBodyControl.Parts select breakableBodyControl._canvas.ToFarseerShape((Polygon)x.Shape.Shape);
                        //    _worldManager.AddBreakableBodyControl(breakableBodyControl, breakableBodyControl.Parts, shapes, breakableBodyControl._canvas.GetOrigin());
                        //    handled = true;
                        //}
                    }
                    if (!handled)
                    {
                        var bodyControl = child as BodyControl;
                        if (bodyControl != null)
                        {
                            _worldManager.AddObject(new BodyManager(bodyControl, new BodyMaterial()));

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
