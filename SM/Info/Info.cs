﻿using SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Controls;

namespace SM
{


    public class Info : BasicInfo
    {
        public IEnumerable<FlagInfo> Flags { get; private set; }
        public IEnumerable<BodyInfo> Bodies { get; private set; }
        public IEnumerable<IJoint> Joints { get; private set; }

        public Info(IEnumerable<IDescriptor> objects)
        {
            var bodies = new List<BodyInfo>();
            var joints = new List<IJoint>();

            findAllFlag(objects);

            foreach (var child in objects)
            {
                bool handled = false;

                if (!handled)
                {
                    var body = child as IBody;
                    if (body != null)
                    {
                        bodies.Add(new BodyInfo(body));
                        handled = true;
                    }
                }

                if (false && !handled)
                {
                    var joint = child as IRopeJoint;
                    if (joint != null)
                    {
                        var targetA = findFlag(joint.TargetFlagIdA);
                        var targetB = findFlag(joint.TargetFlagIdB);

                        joints.Add(joint);

                        handled = true;
                    }
                }
            }


            Bodies = bodies;
            Joints = joints;

        }

        private void findAllFlag(IEnumerable<IDescriptor> objects)
        {
            var flags = new List<FlagInfo>();
            foreach (var child in objects)
            {
                var fc = child as IFlag;
                if (fc != null)
                {
                    flags.Add(new FlagInfo()
                    {
                        ParentId = null,
                        Id = fc.Id,
                        P = new float2(fc.X, fc.Y),
                    });
                }
                var fcp = child as IFlaggable;
                if (fcp != null)
                {
                    foreach (var f in fcp.Flags)
                    {
                        flags.Add(new FlagInfo()
                        {
                            ParentId = fcp.Id,
                            Id = f.Id,
                            P = new float2(f.X, f.Y),
                        });
                    }
                }
            }

            Flags = flags;
        }
        private FlagInfo findFlag(string name)
        {
            foreach (var f in Flags)
            {
                if (f.Id == name)
                {
                    return f;
                }
            }
            return null;
        }
    }


}