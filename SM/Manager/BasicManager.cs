﻿using System;
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

        private Dictionary<string, IIdentifiable> _map = new Dictionary<string, IIdentifiable>();
        private List<LoopCoroutine> _materialLoopCoroutine = new List<LoopCoroutine>();

        private List<BasicCoroutine> _startCoroutine = new List<BasicCoroutine>();
        private List<BasicCoroutine> _updateCoroutine = new List<BasicCoroutine>();

        public BasicManager(IViewWatch viewWatch)
        {
            _materialWatch = new MaterialWatch(dt => updateMaterial(dt));
            _viewWatch = viewWatch;// new ViewWatch(() => updateView());
            _viewWatch.Callback = () => updateView();
        }

        private object Find(string name)
        {
            if (_map.ContainsKey(name))
                return _map[name];
            // WARNING
            return null;
        }
        private T Find<T>(string name) where T : class
        {
            var x = Find(name);
            if (typeof(T) != x.GetType()) return null;
            return (T)x;
        }
        public T FindObject<T>(string name) where T : class
        {
            var x = Find(name) as IMaterial;
            if(x == null) return null;
            var y = x.Object;
            if (typeof(T) != y.GetType()) return null;
            return (T)y;
        }

        public void AddMaterialBehaviour(IMaterialBehaviour x)
        {
            _materialLoopCoroutine.Add(new LoopCoroutine(x.Loop));
        }

        public void AddViewBehaviour(IViewBehaviour x)
        {
            _startCoroutine.Add(new StartCoroutine(this, x.Start));
            _updateCoroutine.Add(new UpdateCoroutine(x.Update));
            //_farseerBehaviours.Add(x);
        }
        protected void AddObject(IIdentifiable obj)
        {
            _map.Add(obj.Id, obj);
            var x = obj as IManager;
            if (x != null)
            {
                x.Build();
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

            foreach (var y in _map.Values)
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
            foreach (var y in _map.Values)
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