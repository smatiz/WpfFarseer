using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class Materials
    {
        List<IBodyMaterial> _bodies;
        List<IBreakableBodyMaterial> _breakableBodies;
        List<IJointMaterial> _joints;

        public IEnumerable<IBodyMaterial> Bodies { get { return _bodies; } }
        public IEnumerable<IBreakableBodyMaterial> BreakableBodies { get { return _breakableBodies; } }
        public IEnumerable<IJointMaterial> Joints { get { return _joints; } }

        IMaterialCreator _materiaCreator;
        IShapeMaterialCreator _shapeCreator;
        public Materials(IMaterialCreator materiaCreator, IShapeMaterialCreator shapeCreator)
        {
            _materiaCreator = materiaCreator;
            _shapeCreator = shapeCreator;
            _bodies = new List<IBodyMaterial>();
            _breakableBodies = new List<IBreakableBodyMaterial>();
            _joints = new List<IJointMaterial>();
        }



        public IMaterial Add(BodyInfo b)
        {
            IMaterial result;
            result = _materiaCreator.Create(b, _shapeCreator);
            if (result is IBreakableBodyMaterial)
            {
                _breakableBodies.Add((IBreakableBodyMaterial)result);
            }
            else
            {
                _bodies.Add((IBodyMaterial)result);
            }
            return result;
        }

        public IMaterial Add(JointInfo j, IEnumerable<FlagInfo> flagInfos)
        {
            IMaterial result;
            result = _materiaCreator.Create(j, flagInfos);
            _joints.Add((IJointMaterial)result);
            return result;
        }

        private void checkForBrokenBodies()
        {
            var toberemoved = new List<IBreakableBodyMaterial>();
            foreach (var x in BreakableBodies)
            {
                if (x.IsBroken)
                {
                    _bodies.AddRange(x.GetPieces().Select(p => p.BodyMaterial));
                }
            }
            foreach (var x in toberemoved)
            {
                _breakableBodies.Remove(x);
            }
        }

        public object Find(IdInfo id) 
        {
            checkForBrokenBodies();
            foreach (var x in Bodies)
            {
                if (x.Id == id)
                {
                    return x;
                }
            }
            foreach (var x in BreakableBodies)
            {
                if (x.Id == id)
                {
                    return x;
                }
            }
            foreach (var x in Joints)
            {
                if (x.Id == id)
                {
                    return x;
                }
            }
            return null;
        }

        public T Find<T>(IdInfo name) where T : class
        {
            var y = Find(name);
            if (y == null) return null;
            if (typeof(T) != y.GetType()) return null;
            return (T)y;
        }
    }
}
