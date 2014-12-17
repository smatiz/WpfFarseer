using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class Synchronizers
    {
        private List<ISynchronizer> _synchronizers = new List<ISynchronizer>();
        Materials _materials;
        public Synchronizers(Views views, Materials materials)
        {

            _materials = materials;
            {
                _synchronizers.AddRange(views.Bodies.Zip(materials.Bodies, (v, m) => new BodySynchronizer(v, m)));
                _synchronizers.AddRange(views.BreakableBodies.Zip(materials.BreakableBodies, (v, m) => new BreakableBodySynchronizer(v, m)));
                _synchronizers.AddRange(views.Joints.Zip(materials.Joints, (v, m) => new RopeJointSynchronizer((IRopeJointView)v, (IRopeJointMaterial)m)));
            }
        }

        public T Find<T>(IdInfo name) where T : class
        {
            return _materials.Find<T>(name);
        }
        public object Find(IdInfo name) 
        {
            return _materials.Find(name);
        }
        public void UpdateMaterial()
        {
            lock (_synchronizers)
            {
                List<BodySynchronizer> managerstobeadded = new List<BodySynchronizer>();
                List<BreakableBodySynchronizer> managersIdtoberemoved = new List<BreakableBodySynchronizer>();
                foreach (var y in _synchronizers)
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
                                managersIdtoberemoved.Add(z);
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
                    _synchronizers.Remove(s);
                }

                _synchronizers.AddRange(managerstobeadded);
            }
        }
        public void UpdateView()
        {
            lock (_synchronizers)
            {
                foreach (var y in _synchronizers)
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
}
