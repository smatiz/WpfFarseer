using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public abstract class BasicManager
    {
        private MaterialWatch _materialWatch;
        private IViewWatch _viewWatch;

        bool _built = false;

        private Dictionary<string, IManager> _managers = new Dictionary<string, IManager>();

        private List<BasicCoroutine> _materialLoopCoroutine = new List<BasicCoroutine>();

        private List<BasicCoroutine> _startCoroutine = new List<BasicCoroutine>();
        private List<BasicCoroutine> _updateCoroutine = new List<BasicCoroutine>();

        public BasicManager(IViewWatch viewWatch)
        {
            _materialWatch = new MaterialWatch(() => updateMaterial());
            _viewWatch = viewWatch;// new ViewWatch(() => updateView());
            _viewWatch.Callback = () => updateView();
        }

        private IManager Find(string name)
        {
            if (_managers.ContainsKey(name))
                return _managers[name];
            // WARNING
            return null;
        }
        public object FindObject(string name) 
        {
            var x = Find(name);
            if (x == null) return null;
            return x.Object;
        }
        public T FindObject<T>(string name) where T : class
        {
            var y = FindObject(name);
            if (y == null ) return null;
            if (typeof(T) != y.GetType()) return null;
            return (T)y;
        }

        public void AddMaterialBehaviour(IMaterialBehaviour x)
        {
            if (_built) return;
            _materialLoopCoroutine.Add(new FuncCoroutine(x.Step));
        }

        public void AddViewBehaviour(IViewBehaviour x)
        {
            if (_built) return;
            _startCoroutine.Add(new StartCoroutine(this, x.Start));
            _updateCoroutine.Add(new UpdateCoroutine(x.Update));
            //_farseerBehaviours.Add(x);
        }
        protected void AddManager(IManager manager)
        {
            //if (_built) return;
            _managers.Add(manager.Id, manager);
        }

        public virtual void Build()
        {
            if (_built) return;
            foreach(var manager in _managers.Values)
            {
                manager.Build();
            }
            _built = true;
        }
       
        public void Play()
        {
            if (!_built) return;
            _viewWatch.Play();
            _materialWatch.Play();
        }
        public void Pause()
        {
            if (!_built) return;
            _materialWatch.Pause();
            _viewWatch.Pause();
        }
        public void Back()
        {
        }

        protected abstract void Step(float dt);
        protected abstract void Loop();

        
        private void updateMaterial()
        {
            if (!_built) return;

            Step(MaterialWatch.DT);

            foreach (var c in _materialLoopCoroutine)
            {
                c.Do();
            }

            List<BodyManager> managerstobeadded = new List<BodyManager>();
            List<string> managersIdtoberemoved = new List<string>();
            foreach (var y in _managers.Values)
            {
                var x = y as IManager;
                if (x != null)
                {
                    var z = y as BreakableBodyManager;
                    if (z != null)
                     {
                         if(z.IsBroken)
                         {
                             managersIdtoberemoved.Add(z.Id);
                             foreach (var m in z.BodyManagers)
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
                _managers.Remove(s);
            }
            foreach (var m in managerstobeadded)
            {
                AddManager(m);
            }
        }

        bool _started = false;
        private void updateView()
        {
            if (!_built) return;
            if(!_started)
            {
                foreach (var x in _startCoroutine)
                {
                    x.Do();
                }
                _started = true;
                return;
            }

            foreach (var c in _updateCoroutine)
            {
                c.Do();
            }

            foreach (var y in _managers.Values)
            {
                var x = y as IManager;
                if (x != null)
                {
                    x.UpdateView();
                }
            }
            Loop();
        }


    }
}
