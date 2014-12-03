using SM;
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
        public IEnumerable<JointInfo> Joints { get; private set; }

        transform2d _currentTransform2d = transform2d.Null;


        //private void 



        public Info(string id, IEnumerable<IDescriptor> objects)
            : base(id)
        {
            var bodies = new List<BodyInfo>();
            var joints = new List<JointInfo>();

            fillFlagInfoList(objects);

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

                if (!handled)
                {
                    var joint = child as IRopeJoint;
                    if (joint != null)
                    {
                        joints.Add(new JointInfo(joint));
                        handled = true;
                    }
                }
            }


            Bodies = bodies;
            Joints = joints;

        }

        private void fillFlagInfoList(IEnumerable<IDescriptor> objects)
        {
            var flags = new List<FlagInfo>();
            foreach (var child in objects)
            {
                var fc = child as IFlag;
                if (fc != null)
                {
                    flags.Add(new FlagInfo(fc, null));
                }
                var fcp = child as IFlaggable;
                if (fcp != null)
                {
                    foreach (var f in fcp.Flags)
                    {
                        flags.Add(new FlagInfo(f, fcp.Id));
                    }
                }
            }

            Flags = flags;
        }




        public FlagInfo FindFlag(string name)
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
