using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SM.WpfView
{
    public abstract class BasicView : Visual, IView
    {
        private string _id;
        private CanvasId _canvasId;
        private BasicView _parentView;

        protected void AddChild(Canvas canvas)
        {
            _canvasId.Children.Add(canvas);
        }
        protected void RemoveChild(Canvas canvas)
        {
            _canvasId.Children.Remove(canvas);
        }

        // RootView Constructor
        protected BasicView(Canvas parentCanvas)
        {
            _canvasId = new CanvasId();
            _parentView = null;
            parentCanvas.Children.Add(_canvasId);
        }
        protected BasicView(BasicView parent)
        {
            _canvasId = new CanvasId();
            _parentView = parent;
            if (_parentView != null)
            {
                _parentView._canvasId.Children.Add(_canvasId);
                _id = _canvasId.Id;
                bool loaded = false;
                _canvasId.Loaded += (s, e) =>
                {
                    if (loaded) return;
                    loaded = true;
                    OnFirstLoad();
                };
            }
        }

        protected virtual void OnFirstLoad() { }
        public abstract void Update();

        public virtual IContext Context { get { return _parentView.Context; } }
        public string Id
        {
            get
            {
                return _id; 
            }
            set
            {
                _id = value;
                _canvasId.Id = _id;
            }
        }
    }
      
}
