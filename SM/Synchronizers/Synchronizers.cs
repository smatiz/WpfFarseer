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
        Views _views;
        public Synchronizers(Views views, Materials materials)
        {

            _views = views;
            _materials = materials;
        }

        public void Clear()
        {
            _materials.Clear();
            _views.Clear();
        }

        public void Add(Info info)
        {
            foreach (var b in info.Bodies)
            {
                var v = _views.Add(b);
                var m = _materials.Add(b);
                if(v is IBreakableBodyView )
                {
                    _synchronizers.Add(new BreakableBodySynchronizer((IBreakableBodyView)v, (IBreakableBodyMaterial)m));
                }
                else //if (v is IBodyView)
                {
                    _synchronizers.Add(new BodySynchronizer((IBodyView)v, (IBodyMaterial)m));
                }
            }

            foreach (var j in info.Joints)
            {
                var v = _views.Add(j, info.Flags);
                var m = _materials.Add(j, info.Flags);
                if (v is IRopeJointView)
                {
                    _synchronizers.Add(new RopeJointSynchronizer((IRopeJointView)v, (IRopeJointMaterial)m));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            foreach (var y in _synchronizers)
            {
                var tobefinalized = y as IToBeFinalized;
                if (tobefinalized != null)
                {
                    tobefinalized.Finalize(_materials);
                }
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
                    var tobefinalized = y as IToBeFinalized;
                    if (tobefinalized != null)
                    {
                        tobefinalized.Finalize(_materials);
                    }

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
