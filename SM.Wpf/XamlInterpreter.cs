﻿using SM;
using SM.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Controls;

namespace SM.Wpf
{
   

    public class XamlInterpreter
    {
        class Flag
        {
            public float X { get; set; }
            public float Y { get; set; }
            public string Id { get; set; }
            public string ParentId { get; set; }
        }

        private static IEnumerable<Flag> FindAllFlag(IEnumerable<BasicControl> objects)
        {
            foreach (var child in objects)
            {
                var fc = child as FlagControl;
                if (fc != null)
                {
                    var flag = new Flag()
                    {
                        X = fc.X,
                        Y = fc.Y,
                        Id = fc.Id,
                        ParentId = null,
                    };
                    yield return flag;
                }
                var fcp = child as IFlaggable;
                if (fcp != null)
                {
                    foreach (var f in fcp.Flags)
                    {
                        var flag = new Flag()
                        {
                            X = f.X,
                            Y = f.Y,
                            Id = f.Id,
                            ParentId = fcp.Id,
                        };
                        yield return flag;
                    }
                }
            }
        }
        private static Flag FindFlag(IEnumerable<Flag> flags, string name)
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

        public static __Views __BuildViews(UIElementCollection parentChildrens, IEnumerable<BasicControl> objects)
        {
            var _views = new __Views();



            var flags = FindAllFlag(objects);
            var tobeadded = new List<BasicControl>();

            foreach (var child in objects)
            {
                bool handled = false;

                BreakableBodyControl breakableBodyControl = null;
                var autoBreakableBodyControl = child as AutoBreakableBodyControl;
                if (autoBreakableBodyControl != null)
                {
                    //var polyF = autoBreakableBodyControl.Shape.Points.ToFarseerVertices();
                    //var vss = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(polyF, (FarseerPhysics.Common.Decomposition.TriangulationAlgorithm)autoBreakableBodyControl.TriangulationAlgorithm);
                    //var bbc = new BreakableBodyControl();
                    //bbc.DefaultBrush = new SolidColorBrush(Colors.AliceBlue);
                    //foreach (var p in vss)
                    //{
                    //    var shape = new ShapeControl();

                    //    shape.Points = p.ToWpf();
                    //    bbc.Shapes.Add(shape);
                    //}

                    //breakableBodyControl = bbc;
                    //tobeadded.Add(breakableBodyControl);
                }

                if (breakableBodyControl == null)
                {
                    breakableBodyControl = child as BreakableBodyControl;
                }
                if (breakableBodyControl != null)
                {
                    _views.BreakableBodies.Add(breakableBodyControl);
                    handled = true;
                }
                //if (!handled)
                //{
                //    var bodyControl = child as BodyControl;
                //    if (bodyControl != null)
                //    {
                //        _views.Bodies.Add(bodyControl);
                //        handled = true;
                //    }
                //}

                if (!handled)
                {
                    var bodyControl = child as __BodyControl;
                    if (bodyControl != null)
                    {
                        _views.Bodies.Add(bodyControl);
                        handled = true;
                    }
                }

                //if (!handled)
                //{
                //    var bodyControl = child as SkinnedBodyControl;
                //    if (bodyControl != null)
                //    {
                //        _views.Bodies.Add(bodyControl);
                //        handled = true;
                //    }
                //}


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

                        _views.Joints.Add(jointControl);

                        handled = true;
                    }
                }
            }

            foreach (var x in tobeadded)
            {
                ((BasicControl)x).AddToUIElementCollection(parentChildrens);
            }

            return _views;

        }




        public static Views BuildViews(UIElementCollection parentChildrens, IEnumerable<BasicControl> objects)
        {
            Views _views = new Views();

            

            var flags = FindAllFlag(objects);
            var tobeadded = new List<BasicControl>();

            foreach (var child in objects)
            {
                bool handled = false;

                    BreakableBodyControl breakableBodyControl = null;
                    var autoBreakableBodyControl = child as AutoBreakableBodyControl;
                    if (autoBreakableBodyControl != null)
                    {
                        //var polyF = autoBreakableBodyControl.Shape.Points.ToFarseerVertices();
                        //var vss = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(polyF, (FarseerPhysics.Common.Decomposition.TriangulationAlgorithm)autoBreakableBodyControl.TriangulationAlgorithm);
                        //var bbc = new BreakableBodyControl();
                        //bbc.DefaultBrush = new SolidColorBrush(Colors.AliceBlue);
                        //foreach (var p in vss)
                        //{
                        //    var shape = new ShapeControl();

                        //    shape.Points = p.ToWpf();
                        //    bbc.Shapes.Add(shape);
                        //}

                        //breakableBodyControl = bbc;
                        //tobeadded.Add(breakableBodyControl);
                    }

                    if (breakableBodyControl == null)
                    {
                        breakableBodyControl = child as BreakableBodyControl;
                    }
                    if (breakableBodyControl != null)
                    {
                        _views.BreakableBodies.Add(breakableBodyControl);
                            handled = true;
                    }
                    if (!handled)
                    {
                        var bodyControl = child as BodyControl;
                        if (bodyControl != null)
                        {
                            _views.Bodies.Add(bodyControl);
                            handled = true;
                        }
                    }

                   

                    //if (!handled)
                    //{
                    //    var bodyControl = child as SkinnedBodyControl;
                    //    if (bodyControl != null)
                    //    {
                    //        _views.Bodies.Add(bodyControl);
                    //        handled = true;
                    //    }
                    //}
                

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

                        _views.Joints.Add(jointControl);
                        
                        handled = true;
                    }
                }
            }

            foreach (var x in tobeadded)
            {
                ((BasicControl)x).AddToUIElementCollection(parentChildrens);
            }

            return _views;
            
        }
    }
}