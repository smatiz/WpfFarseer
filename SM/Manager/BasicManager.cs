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

        private Dictionary<string, IManager> _managers = new Dictionary<string, IManager>();
        private List<LoopCoroutine> _materialLoopCoroutine = new List<LoopCoroutine>();

        private List<BasicCoroutine> _updateCoroutine = new List<BasicCoroutine>();

        public BasicManager(IViewWatch viewWatch)
        {
            _materialWatch = new MaterialWatch(dt => updateMaterial(dt));
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
        public T Find<T>(string name) where T : class
        {
            var x = Find(name);
            if (x == null) return null;
            var y = x.Object;
            if (typeof(T) != y.GetType()) return null;
            return (T)y;
        }
        //public T FindObject<T>(string name) where T : class
        //{
        //    var x = Find(name) as IMaterial;
        //    if(x == null) return null;
        //    var y = x.Object;
        //    if (typeof(T) != y.GetType()) return null;
        //    return (T)y;
        //}

        public void AddMaterialBehaviour(IMaterialBehaviour x)
        {
            _materialLoopCoroutine.Add(new LoopCoroutine(x.Loop));
        }

        public void AddViewBehaviour(IViewBehaviour x)
        {
            _updateCoroutine.Add(new UpdateCoroutine(x.Update));
            //_farseerBehaviours.Add(x);
        }
        protected void AddManager(IManager manager)
        {
            _managers.Add(manager.Id, manager);
        }

        public void Build()
        {
            foreach(var manager in _managers.Values)
            {
                manager.Build();
            }
        }
       
        public void Play()
        {
            _viewWatch.Play();
            _materialWatch.Play();
        }
        public void Pause()
        {
            _materialWatch.Pause();
            _viewWatch.Pause();
        }
        public void Back()
        {
        }

        protected abstract void Step(float dt);
        protected abstract void Loop();

        
        private void updateMaterial(float dt)
        {
            Step(dt);

            foreach (var c in _materialLoopCoroutine)
            {
                c.Do(dt);
            }

            foreach (var y in _managers.Values)
            {
                var x = y as IManager;
                if (x != null)
                {
                    x.UpdateMaterial();
                }
            }
        }
        private void updateView()
        {

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
