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
    public class Info //: BasicInfo
    {
        public IEnumerable<FlagInfo> Flags { get { return _flags; } }
        public IEnumerable<BodyInfo> Bodies { get { return _bodies; } }
        public IEnumerable<JointInfo> Joints { get { return _joints; } }
        
        List<FlagInfo> _flags = new List<FlagInfo>();
        List<BodyInfo> _bodies = new List<BodyInfo>();
        List<JointInfo> _joints = new List<JointInfo>();

       // transform2d _currentTransform2d = transform2d.Null;

        private void scan(IDescriptor descriptor, transform2d currentTransform, string currentId)
        {
            var container = descriptor as IContainer;
            if (container != null)
            {

                var transformer = container as ITransformer;
                if (transformer != null)
                {
                    currentTransform = transformer.Transform * currentTransform;
                }
                var newId = container as IIdentifiable;
                if (newId != null)
                {
                    currentId = String.Format("{0}.{1}", newId.Id, currentId);
                }

                foreach (var childDescriptor in container.Descriptors)
                {
                    scan(childDescriptor, currentTransform, currentId);
                }

                return;
            }

            var entity = descriptor as IEntity;
            if (entity != null)
            {

                var body = entity as IBody;
                if (body != null)
                {
                    // _bodies.Add(new BodyInfo(body));
                    return;
                }

                var joint = entity as IRopeJoint;
                if (joint != null)
                {
                    // _joints.Add(new JointInfo(joint));
                    return;
                }
            }

        }



        public Info(IContainer container)
          //  : base(id)
        {
            //fillFlagInfoList(objects);
            //scan(objects);
        }

        private void fillFlagInfoList(IEnumerable<IEntity> objects)
        {
            //foreach (var child in objects)
            //{
            //    var fc = child as IFlag;
            //    if (fc != null)
            //    {
            //       // _flags.Add(new FlagInfo(fc, null));
            //    }
            //    var fcp = child as IFlaggable;
            //    if (fcp != null)
            //    {
            //        foreach (var f in fcp.Flags)
            //        {
            //            _flags.Add(new FlagInfo(f, fcp.Id));
            //        }
            //    }
            //    var c = child as IContainer;
            //    if (c != null)
            //    {
            //        fillFlagInfoList(c.Entities);
            //    }
            //}

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
