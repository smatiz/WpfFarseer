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
using System.Windows.Data;
using System.Windows.Controls;

namespace SM.WpfFarseer
{
    public class XamlInterpreter
    {
        private static IEnumerable<FlagControl> FindAllFlag(IEnumerable<BasicControl> objects)
        {
            foreach (var child in objects)
            {
                var fc = child as FlagControl;
                if (fc != null)
                {
                    fc.ParentId = null;
                    yield return fc;
                }
                var fcp = child as IFlaggable;
                if (fcp != null)
                {
                    foreach (var flag in fcp.Flags)
                    {
                        flag.ParentId = fcp.Id;
                        yield return flag;
                    }
                }
            }
        }
        private static FlagControl FindFlag(IEnumerable<FlagControl> flags, string name)
        {
            foreach (var f in flags)
            {
                if (f.Id == name)
                {
                    return f;
                }
            }
            return null;
        }

        public static void BuildFarseerWorldManager(UIElementCollection parentChildrens, FarseerWorldManager worldManager, 
            IEnumerable<BasicControl> objects, bool isInDesignMode = false)
        {
            CodeGenerator.Header = "Farseer Code Generator" + " : " + worldManager.Id;

            var flags = FindAllFlag(objects);
            //foreach(var flag in flags)
            //{
            //    flag.Set(root);
            //}

            var tobeadded = new List<BasicControl>();

            foreach (var child in objects)
            {
                bool handled = false;
                if (!isInDesignMode)
                {
                    BreakableBodyControl breakableBodyControl = null;
                    var autoBreakableBodyControl = child as AutoBreakableBodyControl;
                    if (autoBreakableBodyControl != null)
                    {
                        var polyF = autoBreakableBodyControl.Shape.Points.ToFarseerVertices();
                        var vss = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(polyF, (FarseerPhysics.Common.Decomposition.TriangulationAlgorithm)autoBreakableBodyControl.TriangulationAlgorithm);
                        var bbc = new BreakableBodyControl();
                        bbc.DefaultBrush = new SolidColorBrush(Colors.AliceBlue);
                        foreach (var p in vss)
                        {
                            var shape = new ShapeControl();
                            
                            shape.Points = p.ToWpf();
                            bbc.Shapes.Add(shape);
                        }

                        breakableBodyControl = bbc;
                        //autoBreakableBodyControl._canvas.Visibility = System.Windows.Visibility.Hidden;
                        tobeadded.Add(breakableBodyControl);
                    }

                    if (breakableBodyControl == null)
                    {
                        breakableBodyControl = child as BreakableBodyControl;
                    }
                    if (breakableBodyControl != null)
                    {
                            worldManager.AddBreakableBodyView(breakableBodyControl);
                            handled = true;
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

                    if (!handled)
                    {
                        var bodyControl = child as SkinnedBodyControl;
                        if (bodyControl != null)
                        {
                            worldManager.AddBodyView(bodyControl);

                            //_worldManager.AddBodyControl(bodyControl, bodyControl._canvas.GetOrigin());
                            handled = true;
                        }
                    }
                }

                if (!handled)
                {
                    var jointControl = child as RopeJointControl;
                    if (jointControl != null)
                    {
                        var targetA = FindFlag(flags, jointControl.TargetFlagIdA);
                        var targetB = FindFlag(flags, jointControl.TargetFlagIdB);

                        var line = new Line();
                        line.Stroke = new SolidColorBrush(Colors.Green);
                        line.StrokeThickness = 1;
                        parentChildrens.Add(line);

                        line.X1 = targetA.X;
                        line.Y1 = targetA.Y;
                        line.X2 = targetB.X;
                        line.Y2 = targetB.Y;

                        jointControl.SetLine(line);
                        jointControl.SetTargets(targetA.ParentId, targetB.ParentId);

                        if (!isInDesignMode)
                        {
                            worldManager.AddRopeJointControl(jointControl);
                        }
                        handled = true;

                       
                        //var bindingX1 = new Binding { Source = targetA, Path = new PropertyPath("AbsoluteLocation.X"), Mode = BindingMode.OneWay };
                        //line.SetBinding(Line.X1Property, bindingX1);
                        //var bindingY1 = new Binding { Source = targetA, Path = new PropertyPath("AbsoluteLocation.Y"), Mode = BindingMode.OneWay };
                        //line.SetBinding(Line.Y1Property, bindingY1);
                        //var bindingX2 = new Binding { Source = targetB, Path = new PropertyPath("AbsoluteLocation.X"), Mode = BindingMode.OneWay };
                        //line.SetBinding(Line.X2Property, bindingX2);
                        //var bindingY2 = new Binding { Source = targetB, Path = new PropertyPath("AbsoluteLocation.Y"), Mode = BindingMode.OneWay };
                        //line.SetBinding(Line.Y2Property, bindingY2);

                        
                    }
                }
            }

            foreach (var x in tobeadded)
            {
                ((BasicControl)x).AddToUIElementCollection(parentChildrens);
            }

            
        }
    }
}
