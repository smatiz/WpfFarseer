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
    public class Info 
    {
        public IEnumerable<FlagInfo> Flags { get { return _flags; } }
        public IEnumerable<BodyInfo> Bodies { get { return _bodies; } }
        public IEnumerable<JointInfo> Joints { get { return _joints; } }
        
        List<FlagInfo> _flags = new List<FlagInfo>();
        List<BodyInfo> _bodies = new List<BodyInfo>();
        List<JointInfo> _joints = new List<JointInfo>();

        private void scan(IDescriptor descriptor, transform2d currentTransform, IdInfo currentId)
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
                    currentId = currentId + newId.Id;
                }

                foreach (var childDescriptor in container.Descriptors)
                {
                    scan(childDescriptor, currentTransform, currentId);
                }
            }

            var entity = descriptor as IEntity;
            if (entity != null)
            {

                var body = entity as IBody;
                if (body != null)
                {
                    _bodies.Add(new BodyInfo()
                    {
                        Id = currentId + body.Id,
                        //BodyType = body.BodyType,
                        Transform = currentTransform * body.Transform,
                        Body = body,
                        //Flags = body.Flags,
                        //Shapes = body.Shapes
                    });
                }

                var joint = entity as IJoint;
                if (joint != null)
                {

                    var ropeJoint = entity as IRopeJoint;
                    if (ropeJoint != null)
                    {
                        _joints.Add(new RopeJointInfo()
                        {
                            Id = currentId + ropeJoint.Id,
                            CollideConnected = ropeJoint.CollideConnected,
                            TargetFlagIdA = ropeJoint.TargetFlagIdA,
                            TargetFlagIdB = ropeJoint.TargetFlagIdB,
                        });

                    }
                }

                var flaggable = entity as IFlaggable;
                if (flaggable != null)
                {
                    var identifiable = entity as IIdentifiable;
                    foreach (var flag in flaggable.Flags)
                    {
                        _flags.Add(new FlagInfo()
                        {
                            Id = (IdInfo)entity.Id + (IdInfo)flag.Id,
                            P = new float2(flag.X, flag.Y),
                        });
                    }
                }
                    

            }

        }

        public Info(IContainer container)
        {
            scan(container, transform2d.Null, IdInfo.Null);
        }
    }


}
