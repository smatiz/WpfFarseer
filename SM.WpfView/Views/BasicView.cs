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
    public abstract class BasicView : UserControl, IView
    {
        private IdInfo _id;
        private CanvasId _canvasId;

        public List<IFlagView> Flags { get; private set; }

        protected void AddChild(Canvas canvas)
        {
            _canvasId.Children.Add(canvas);
        }
        protected void RemoveChild(Canvas canvas)
        {
            _canvasId.Children.Remove(canvas);
        }

        //// RootView Constructor
        //protected BasicView(Canvas parentCanvas)
        //{
        //    _canvasId = new CanvasId();
        //    parentCanvas.Children.Add(_canvasId);
        //}
        protected BasicView(Canvas parentCanvas, IContext context, IdInfo id)
        {
            Flags = new List<IFlagView>();
            _id = id;
            _canvasId = new CanvasId();
            Context = context;
            //if (_parentView != null)
            {
                parentCanvas.Children.Add(_canvasId);
                _canvasId.Id = _id;
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

        public IContext Context;// { get { return _parentView.Context; } }
        public IdInfo Id
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
