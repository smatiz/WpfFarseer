using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfFarseer
{
    public class FarseerCanvas : Canvas
    {
        WorldManager _worldManager;
        public FarseerCanvas()
        {
            Loaded += (s, e) =>
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return; 
                _worldManager = new WorldManager();
                foreach (var child in Children)
                {
                    var body = child as BodyControl;
                    if (body != null)
                    {
                        body.Initialize(_worldManager);
                    }
                }
            };
        }

        public void Update()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
            foreach (var child in Children)
            {
                var bodyControl = child as BodyControl;
                if (bodyControl != null)
                {
                    bodyControl.Update(_worldManager);
                }
            }
#if DEBUG
            InvalidateVisual();
#endif
        }

        private void clear()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
            _worldManager.Clear();
            
            foreach (var child in Children)
            {
                var body = child as BodyControl;
                if (body != null)
                {
                    body.Initialize(_worldManager);
                }
            }

            //_world.Clear();
            //_world = new World(new Microsoft.Xna.Framework.Vector2(0, 10));
            Update();
        }


        public bool Savable
        {
            get
            {
                if (_worldManager == null) return false;
                return _worldManager.Savable;
            }
        }

        public void Save()
        {
            _worldManager.Save();
        }

        public void Play()
        {
            _worldManager.Play();
        }

        public void Pause()
        {
            _worldManager.Pause();
        }

      

        public void Load()
        {
            _worldManager.Load();
        }
 private BodyControl Find(string name) 
        {
            foreach (var child in Children)
            {
                var bodyControl = child as BodyControl;
                if (bodyControl != null && bodyControl.Name == name)
                {
                    return bodyControl;
                }
            }
            return null;
        }
#if DEBUG
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            WpfDebugView.Instance.Draw(drawingContext);
        }
#endif
        

        public StepViewModel StepViewModel
        {
            get { return (StepViewModel)GetValue(StepViewModelProperty); }
            set { SetValue(StepViewModelProperty, value); }
        }
        public static readonly DependencyProperty StepViewModelProperty =
            DependencyProperty.Register("StepViewModel", typeof(StepViewModel), typeof(FarseerCanvas), new PropertyMetadata(null, OnStepViewModelChanged));
        private static void OnStepViewModelChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var _this = (FarseerCanvas)dependencyObject;
            _this.StepViewModel.FarseerCanvas = _this;
        }
       
    }
}

