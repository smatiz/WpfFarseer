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

        public Materials(IMaterialCreator materiaCreator, IShapeMaterialCreator shapeCreator, Info info)
        {
            _bodies = new List<IBodyMaterial>();
            _breakableBodies = new List<IBreakableBodyMaterial>();
            foreach (var b in info.Bodies)
            {
                var br = materiaCreator.Create(b, shapeCreator);
                if (br is IBreakableBodyMaterial)
                {
                    _breakableBodies.Add((IBreakableBodyMaterial)br);
                }
                else
                {
                    _bodies.Add((IBodyMaterial)br);
                }
            }

            _joints = new List<IJointMaterial>();
            foreach (var j in info.Joints)
            {
                _joints.Add(materiaCreator.Create(j, info));
            }




            foreach (var j in Joints)
            {
                if (j is IToBeFinalized)
                {
                    ((IToBeFinalized)j).Finalize(this);
                }

            }
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

        public object Find(string id) 
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

        public T Find<T>(string name) where T : class
        {
            var y = Find(name);
            if (y == null) return null;
            if (typeof(T) != y.GetType()) return null;
            return (T)y;
        }
    }
}
