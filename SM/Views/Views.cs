using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class Views
    {
        public IEnumerable<IBodyView> Bodies { get { return _bodies; } }
        public IEnumerable<IBreakableBodyView> BreakableBodies { get; private set; }
        public IEnumerable<IJointView> Joints { get; private set; }


        List<IBodyView> _bodies = new List<IBodyView>();
        List<IBreakableBodyView> _breakablebodies = new List<IBreakableBodyView>();
        List<IJointView> _joints = new List<IJointView>();

        IViewCreator _viewCreator;
        IShapeViewCreator _shapeCreator;
        public Views(IViewCreator viewCreator, IShapeViewCreator shapeCreator)
        {
            _viewCreator = viewCreator;
            _shapeCreator = shapeCreator;
            
        }

        public IView Add(BodyInfo b)
        {
            IView result;
            if (b.Body.BodyType != BodyType.Breakable)
            {
                result = _viewCreator.CreateBody(b, _shapeCreator);
                _bodies.Add((IBodyView)result);
            }
            else
            {
                result = _viewCreator.CreateBreakableBody(b, _shapeCreator);
                _breakablebodies.Add((IBreakableBodyView)result);
            }
            return result;
        }

        public IView Add(JointInfo j, IEnumerable<FlagInfo> flagInfos)
        {
            IView result;
            result = _viewCreator.CreateJoint(j, flagInfos);
            _joints.Add((IJointView)result);
            return result;
        }
    }
}
