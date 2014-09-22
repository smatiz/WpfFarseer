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
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        public FarseerCanvas()
        {
            _timer.Tick += (s, e) => Update();
            _timer.Interval = 40;
            _worldManager = new WorldManager();//() => this.Dispatcher.BeginInvoke(new Action(Update)));
            Loaded += (s, e) =>
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return; 
                foreach (var child in Children)
                {
                    var bodyControl = child as BodyControl;
                    if (bodyControl != null)
                    {

                        _worldManager.AddBodyControl(bodyControl);
                        //bodyControl.Initialize(_worldManager);

                        /*var angleJoint = FarseerCanvas.GetAngleJoint(bodyControl);
                        if(angleJoint != null)
                        {
                            var bodyControl2 = Find(angleJoint);
                            _worldManager.CreateAngleJoint(bodyControl.BodyManager, bodyControl2.BodyManager);
                        }*/
                           


                    }

                    /*var jointControl = child as RopeJointControl;
                    if (jointControl != null)
                    {
                        var targets = jointControl.Targets.ToArray();
                        _worldManager.CreateRopeJoint(Find(targets[0].Name).BodyManager, Find(targets[1].Name).BodyManager, targets[0].Point.ToFarseer(), targets[1].Point.ToFarseer());
                    }*/
                }




                _timer.Start();
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


                    if (_worldManager == null) return;
                    _worldManager.Update();
                }
            }
#if DEBUG
            InvalidateVisual();
#endif
        }

       /* private void clear()
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
        }*/


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


        public void Restart()
        {

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
            _this.StepViewModel.WorldManager = _this._worldManager;
        }




        public static string GetAngleJoint(DependencyObject obj)
        {
            return (string)obj.GetValue(AngleJointProperty);
        }

        public static void SetAngleJoint(DependencyObject obj, string value)
        {
            obj.SetValue(AngleJointProperty, value);
        }

        // Using a DependencyProperty as the backing store for AngleJoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleJointProperty =
            DependencyProperty.RegisterAttached("AngleJoint", typeof(string), typeof(FarseerCanvas), new PropertyMetadata(null));

        

    }
}

