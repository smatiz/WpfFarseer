using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class Synchronizers
    {
        private Dictionary<string, ISynchronizer> _synchronizersMap = new Dictionary<string, ISynchronizer>();
        
        public Synchronizers(Views views, Materials materials)
        {
            {
                var r = views.Bodies.Zip(materials.Bodies, (v, m) => new BodySynchronizer(v, m));
                foreach (var s in r)
                {
                    _synchronizersMap.Add(s.Id, s);
                }
            }

            {
                var r = views.BreakableBodies.Zip(materials.BreakableBodies, (v, m) => new BreakableBodySynchronizer(v, m));
                foreach (var s in r)
                {
                    _synchronizersMap.Add(s.Id, s);
                }
            }

            {
                var r = views.Joints.Zip(materials.Joints, (v, m) => new RopeJointSynchronizer((IRopeJointView)v, (IRopeJointMaterial)m));
                foreach (var s in r)
                {
                    _synchronizersMap.Add(s.Id, s);
                }
            }
        }

        public ISynchronizer Find(string name)
        {
            if (_synchronizersMap.ContainsKey(name))
                return _synchronizersMap[name];
            return null;
        }
        public void UpdateMaterial()
        {
            List<BodySynchronizer> managerstobeadded = new List<BodySynchronizer>();
            List<string> managersIdtoberemoved = new List<string>();
            foreach (var y in _synchronizersMap.Values)
            {
                var x = y as ISynchronizer;
                if (x != null)
                {
                    var z = y as BreakableBodySynchronizer;
                    if (z != null)
                    {
                        IEnumerable<BodySynchronizer> bodyPiecesSynchronizers;
                        if (z.IsBroken(out bodyPiecesSynchronizers))
                        {
                            managersIdtoberemoved.Add(z.Id);
                            foreach (var m in bodyPiecesSynchronizers)
                            {
                                managerstobeadded.Add(m);
                            }
                        }
                    }
                    x.UpdateMaterial();
                }
            }
            foreach (var s in managersIdtoberemoved)
            {
                _synchronizersMap.Remove(s);
            }
            foreach (var m in managerstobeadded)
            {
                _synchronizersMap.Add(m.Id, m);
            }
        }
        public void UpdateView()
        {
            foreach (var y in _synchronizersMap.Values)
            {
                var x = y as ISynchronizer;
                if (x != null)
                {
                    x.UpdateView();
                }
            }

        }
    }
}
