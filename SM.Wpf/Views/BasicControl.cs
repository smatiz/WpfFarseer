using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SM.Wpf
{
    // controlli in grado di agganciare il proprio canvas o il canvas di un altro BasicControl al canvas padre
    public abstract class BasicControl : Visual , IContextual
    {
        private string _id;
        private CanvasId _canvasId;
        private List<BasicControl> _children = new List<BasicControl>();

        protected void AddChild(BasicControl basic)
        {
            _canvasId.Children.Add(basic._canvasId);
            _children.Add(basic);
        }

        protected void AddChild(Canvas canvas)
        {
            _canvasId.Children.Add(canvas);
        }
        protected void RemoveChild(Canvas canvas)
        {
            _canvasId.Children.Remove(canvas);
        }
        
        private void initialize()
        {
            bool loaded = false;
            _canvasId.Loaded += (s, e) =>
            {
                if (loaded) return;
                loaded = true;
                OnFirstLoad();
            };
        }
        public BasicControl()
        {
            _canvasId = new CanvasId();
            _id = _canvasId.Id;
            initialize();
        }
        public BasicControl(CanvasId canvasId)
        {
            _canvasId = canvasId;
            _id = canvasId.Id;
            initialize();
        }

        protected virtual void OnFirstLoad() { }

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

        protected IContext _context;
        public virtual IContext Context
        {
            set
            {
                _context = value;
            }
        }
    }
}
