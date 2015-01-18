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

        public dynamic Entity;

        bool _started = false;

        private Synchronizers _synchronizers;

        private List<BasicCoroutine> _materialLoopCoroutine = new List<BasicCoroutine>();

        private List<BasicCoroutine> _startCoroutine = new List<BasicCoroutine>();
        private List<BasicCoroutine> _updateCoroutine = new List<BasicCoroutine>();

        //public BasicManager(Views views, Materials materials, IWatchView viewWatch)
        //    : this(new Synchronizers(views, materials), viewWatch)
        //{
        //}

        public BasicManager(Synchronizers synchronizers, IWatchView viewWatch)
        {
            _synchronizers = synchronizers;
            _materialWatch = new WatchMaterial(() => updateMaterial());
            _viewWatch = viewWatch;
            _viewWatch.Callback = () => updateView();

            Entity = new IdInfoExpando(s => ((IMaterial)_synchronizers.Find(s)).Object);
        }


        public void Add(Info info)
        {
            _synchronizers.Add(info);
            Entity.Add(info.Bodies.Select(x => x.Id).Concat(info.Joints.Select(x => x.Id)));
        }

        public virtual void Clear()
        {
            _synchronizers.Clear();
            Entity.Clear();
        }

       

        public object FindObject(IdInfo name) 
        {
            return ((IMaterial)_synchronizers.Find(name)).Object;
        }
        public T FindObject<T>(IdInfo name) where T : class
        {
            return (T)FindObject(name);
        }
        

        public void AddMaterialBehaviour(IBehaviourMaterial x)
        {
            _materialLoopCoroutine.Add(new FuncCoroutine(x.Step));
        }
        public void AddViewBehaviour(IBehaviourView x)
        {
            _startCoroutine.Add(new StartCoroutine(this, x.Start));
            _updateCoroutine.Add(new UpdateCoroutine(x.Update));
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

        private void updateMaterial()
        {
            Step(WatchMaterial.DT);

            foreach (var c in _materialLoopCoroutine)
            {
                c.Do();
            }

            _synchronizers.UpdateMaterial();

        }

        private void updateView()
        {
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
