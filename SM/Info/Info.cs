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
        public IEnumerable<FlagInfo> Flags { get { return _flags; } }
        public IEnumerable<BodyInfo> Bodies { get { return _bodies; } }
        public IEnumerable<JointInfo> Joints { get { return _joints; } }
        
        List<FlagInfo> _flags = new List<FlagInfo>();
        List<BodyInfo> _bodies = new List<BodyInfo>();
        List<JointInfo> _joints = new List<JointInfo>();

       // transform2d _currentTransform2d = transform2d.Null;

        private void scan(IEnumerable<IEntity> objects)
        {
            foreach (var child in objects)
            {
                bool handled = false;

                if (!handled)
                {
                    var body = child as IBody;
                    if (body != null)
                    {
                        _bodies.Add(new BodyInfo(body));
                        handled = true;
                    }
                }

                if (!handled)
                {
                    var joint = child as IRopeJoint;
                    if (joint != null)
                    {
                        _joints.Add(new JointInfo(joint));
                        handled = true;
                    }
                }

                if (!handled)
                {
                    var container = child as IContainer;


                    if (container != null)
                    {
                        scan(container.Entities);
                        handled = true;
                    }

                }
            }
        }



        public Info(string id, IEnumerable<IEntity> objects)
            : base(id)
        {
            fillFlagInfoList(objects);

           // var xxxx = objects.ToArray();


            scan(objects);
            //foreach (var child in objects)
            //{
            //    bool handled = false;

            //    if (!handled)
            //    {
            //        var body = child as IBody;
            //        if (body != null)
            //        {
            //            _bodies.Add(new BodyInfo(body));
            //            handled = true;
            //        }
            //    }

            //    if (!handled)
            //    {
            //        var joint = child as IRopeJoint;
            //        if (joint != null)
            //        {
            //            _joints.Add(new JointInfo(joint));
            //            handled = true;
            //        }
            //    }
            //}

        }

        private void fillFlagInfoList(IEnumerable<IEntity> objects)
        {
            foreach (var child in objects)
            {
                var fc = child as IFlag;
                if (fc != null)
                {
                    _flags.Add(new FlagInfo(fc, null));
                }
                var fcp = child as IFlaggable;
                if (fcp != null)
                {
                    foreach (var f in fcp.Flags)
                    {
                        _flags.Add(new FlagInfo(f, fcp.Id));
                    }
                }
                var c = child as IContainer;
                if (c != null)
                {
                    fillFlagInfoList(c.Entities);
                }
            }

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
