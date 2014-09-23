using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        FarseerWorldManager _worldManager;
        List<TwoPointJointManager> _ropeJointManager = new List<TwoPointJointManager>();
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        public event Action<FarseerPhysics.Dynamics.World, FarseerWorldManager> OnStep;

        //Stopwatch _watch = new Stopwatch();
        //bool _allDone = false;
        public FarseerCanvas()
        {
            _timer.Tick += (s, e) => Update();
            _timer.Interval = 40;
            _worldManager = new FarseerWorldManager((w) =>
            {
                if (OnStep != null)
                {
                    OnStep(w, _worldManager);
                }
            });//() => this.Dispatcher.BeginInvoke(new Action(Update)));
            Loaded += (s, e) =>
            {
                _timer.Start();
                _controlUpdate();
                //_watch.Start();
            };
        }

        void _controlUpdate()
        {

            var tobeadded = new List<UIElement>();


           
            foreach (var child in Children)
            {
                 //var fControl = child as BasicControl;
                 //if (fControl == null || fControl.Delay.Ticks < 0) break;
                


                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    var bodyControl = child as BodyControl;
                    if (bodyControl != null)
                    {
                        _worldManager.AddBodyControl(bodyControl);
                    }
                }


                var jointControl = child as RopeJointControl;
                if (jointControl != null)
                {
                    var ropeJointInfo = _resolve(jointControl);

                    var line = new Line();
                    line.Stroke = new SolidColorBrush(Colors.Green);
                    line.StrokeThickness = 1;
                    tobeadded.Add(line);

                    _ropeJointManager.Add(new TwoPointJointManager(this, ropeJointInfo, line));

                    if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    {
                        _worldManager.AddRopeJoint(ropeJointInfo, jointControl);
                    }
                }
            }


            foreach (var tba in tobeadded)
            {
                Children.Add(tba);
            }
        }

        public void Update()
        {
            //if(!_allDone)
            //{
            //    _controlUpdate();
            //}

            if (_worldManager == null) return;
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
            _worldManager.Update();
            foreach (var x in _ropeJointManager)
            {
                x.Update();
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
 
        //private BodyControl _find(string name) 
        //{
        //    foreach (var child in Children)
        //    {
        //        var bodyControl = child as BodyControl;
        //        if (bodyControl != null && bodyControl.Name == name)
        //        {
        //            return bodyControl;
        //        }
        //    }
        //    return null;
        //}

        private TwoPointJointInfo _resolve(RopeJointControl jointControl)
        {
            BodyControl bodyControlA = null, bodyControlB = null;
            Point anchorA = new Point(), anchorB = new Point();
            foreach (var child in Children)
            {
                var bodyControl = child as BodyControl;
                if (bodyControl != null)
                {
                    foreach (var bodyChild in bodyControl.Children)
                    {
                        var crossControl = bodyChild as CrossControl;
                        if (crossControl != null)
                        {
                            if( crossControl.Name == jointControl.TargetNameA)
                            {
                                bodyControlA = bodyControl;
                                anchorA = crossControl.TranslatePoint(new Point(0, 0), bodyControl);
                            }
                            else if (crossControl.Name == jointControl.TargetNameB)
                            {
                                bodyControlB = bodyControl;
                                anchorB = crossControl.TranslatePoint(new Point(0, 0), bodyControl);
                            }
                        }
                    }
                }
            }

            return new TwoPointJointInfo(bodyControlA, bodyControlB, anchorA, anchorB);
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

