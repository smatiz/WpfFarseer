using SM;
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

        public static Views BuildViews(IEnumerable<BasicControl> objects)
        {
            var _views = new Views();

            var flags = FindAllFlag(objects);

            foreach (var child in objects)
            {
                bool handled = false;

                

                if (!handled)
                {
                    var bodyControl = child as BodyControl;
                    if (bodyControl != null)
                    {
                        _views.Bodies.Add(bodyControl);
                        handled = true;
                    }
                }

               

                if (!handled)
                {
                    var jointControl = child as RopeJointControl;
                    if (jointControl != null)
                    {
                        var targetA = FindFlag(flags, jointControl.TargetFlagIdA);
                        var targetB = FindFlag(flags, jointControl.TargetFlagIdB);

                        //parentChildrens.Add(jointControl.UIElement);


                        //line.X1 = targetA.X;
                        //line.Y1 = targetA.Y;
                        //line.X2 = targetB.X;
                        //line.Y2 = targetB.Y;

                        //jointControl.SetLine(line);
                        jointControl.AnchorA = new float2(targetA.X, targetA.Y);
                        jointControl.AnchorB = new float2(targetB.X, targetB.Y);
                        jointControl.SetTargets(targetA.ParentId, targetB.ParentId);

                        _views.Joints.Add(jointControl);

                        handled = true;
                    }
                }
            }

            

            return _views;

        }

    }
}
