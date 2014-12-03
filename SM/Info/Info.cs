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

        transform2d _currentTransform2d = transform2d.Null;

        private void scan(IEnumerable<IDescriptor> objects)
        {
            foreach (var child in objects)
            {
                bool handled = false;

                if (!handled)
                {
                    var body = child as IBody;
                    if (body != null)
                    {
                        _bodies.Add(new BodyInfo(body, _currentTransform2d));
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
                        var layer = container as ILayer;

                        var currentTransform2dSaved = _currentTransform2d;
                        if (layer != null)
                        {
                           // _currentTransform2d = layer.Transform * _currentTransform2d;
                        }

                        scan(container.Descriptors);
                        _currentTransform2d = currentTransform2dSaved;
                        handled = true;
                    }

                }
            }
        }



        public Info(string id, IEnumerable<IDescriptor> objects)
            : base(id)
        {
            fillFlagInfoList(objects);
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

        private void fillFlagInfoList(IEnumerable<IDescriptor> objects)
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
                    fillFlagInfoList(c.Descriptors);
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
