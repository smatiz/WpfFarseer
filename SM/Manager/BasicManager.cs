using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public abstract class BasicManager
    {
        private WatchMaterial _materialWatch;
        private IWatchView _viewWatch;

        bool _built = true;
        bool _started = false;

        private Synchronizers _synchronizers;

        private List<BasicCoroutine> _materialLoopCoroutine = new List<BasicCoroutine>();

        private List<BasicCoroutine> _startCoroutine = new List<BasicCoroutine>();
        private List<BasicCoroutine> _updateCoroutine = new List<BasicCoroutine>();

        public BasicManager(Views views, Materials materials, IWatchView viewWatch)
            : this(new Synchronizers(views, materials), viewWatch)
        {
        }

        public BasicManager(Synchronizers synchronizers, IWatchView viewWatch)
        {
            _synchronizers = synchronizers;
            _materialWatch = new WatchMaterial(() => updateMaterial());
            _viewWatch = viewWatch;
            _viewWatch.Callback = () => updateView();
        }

        public object FindObject(string name) 
        {
            var x = _synchronizers.Find(name);
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

        public void AddMaterialBehaviour(IBehaviourMaterial x)
        {
            if (_built) return;
            _materialLoopCoroutine.Add(new FuncCoroutine(x.Step));
        }
        public void AddViewBehaviour(IBehaviourView x)
        {
            if (_built) return;
            _startCoroutine.Add(new StartCoroutine(this, x.Start));
            _updateCoroutine.Add(new UpdateCoroutine(x.Update));
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

            Step(WatchMaterial.DT);

            foreach (var c in _materialLoopCoroutine)
            {
                c.Do();
            }

            _synchronizers.UpdateMaterial();

        }

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

            _synchronizers.UpdateView();
            Loop();
        }
    }
}
