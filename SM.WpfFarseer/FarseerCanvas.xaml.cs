﻿
using SM;
using SM.Farseer;
using SM.WpfFarseer;
//using Newtonsoft.Json;
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
using System.Windows.Threading;

namespace WpfFarseer
{
    using xna = Microsoft.Xna.Framework;
    using FShape = FarseerPhysics.Collision.Shapes;
    public partial class FarseerCanvas : Canvas
    {
        List<TwoPointJointControlManager> _ropeJointManager = new List<TwoPointJointControlManager>();
        DispatcherTimer _timer = new DispatcherTimer();

        //public event Action<FarseerWorldManager> WorldReady;

        //FarseerBehaviour _farseerBehaviour = new FarseerBehaviour();
        //public Action<FarseerWorldManager> WorldStarted { private get; set; }
        //public Func<FarseerWorldManager, IEnumerator<BasicCoroutine>> WorldLoop { private get; set; }

        FarseerWorldManager _worldManager;

        private List<IFarseerBehaviourWpf> _farseerBehaviours = new List<IFarseerBehaviourWpf>();

        private List<BasicCoroutine> _startCoroutine = new List<BasicCoroutine>();
        private List<BasicCoroutine> _updateCoroutine = new List<BasicCoroutine>();

        public void AddFarseerBehaviour(IFarseerBehaviourWpf x)
        {
            _startCoroutine.Add(new StartCoroutine(_worldManager, x.Start));
            _updateCoroutine.Add(new UpdateCoroutine(x.Update));
            _farseerBehaviours.Add(x);
            _worldManager.AddFarseerBehaviour(x);
        }
        public FarseerCanvas()
        {
            InitializeComponent();

            _timer.Tick += (s, e) => Update();
            _timer.Interval = new TimeSpan(0,0,0,0, 40);
            _worldManager = new FarseerWorldManager();

            //var eventCoroutine = new EventCoroutine();
            //eventCoroutine.Event += eventCoroutine_Event;
            //_loopCoroutines.Add(eventCoroutine);

            Loaded += (s, e) =>
            {
                _controlUpdate();
                _timer.Start();
                //if (WorldReady != null)
                //{
                //    WorldReady(_worldManager);
                //}
                foreach (var x in _farseerBehaviours)
                {
                    x.Start(_worldManager);
                }
            };
        }

        xna.Vector2 GetOrigin(FrameworkElement elem)
        {
            return WpfFarseerHelper.ToFarseer(elem.TranslatePoint(new System.Windows.Point(0, 0), (System.Windows.UIElement)elem.Parent));
        }

        void _controlUpdate()
        {
            var tobeadded = new List<UIElement>();
           
            foreach (var child in Children)
            {
                bool handled = false;
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {

                    {
                        var breakableBodyControl = child as BreakableBodyControl;
                        if (breakableBodyControl != null)
                        {
                            var controlParts = breakableBodyControl.Children.OfType<BreakableBodyPartControl>();
                            var shapes = controlParts.SelectMany<BodyControl, FShape.Shape>(c => (from x in c.Shapes select x.ToFarseer()));
                            _worldManager.AddBreakableBodyControl(breakableBodyControl, controlParts, shapes, GetOrigin(breakableBodyControl));
                            handled = true;
                        }
                    }
                    if(!handled)
                    {
                        var bodyControl = child as BodyControl;
                        if (bodyControl != null)
                        {
                            _worldManager.AddBodyControl(bodyControl, GetOrigin(bodyControl));
                            handled = true;
                        }
                    }
                   
                }

                if (!handled)
                {
                    var jointControl = child as RopeJointControl;
                    if (jointControl != null)
                    {
                        var ropeJointControlInfo = _resolve(jointControl);

                        var line = new Line();
                        line.Stroke = new SolidColorBrush(Colors.Green);
                        line.StrokeThickness = 1;
                        tobeadded.Add(line);

                        _ropeJointManager.Add(new TwoPointJointControlManager(this, ropeJointControlInfo, line));

                        if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                        {
                            _worldManager.AddRopeJoint(jointControl);
                        }
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
            foreach (var c in _farseerBehaviours)
            {
                c.Update();
            }
        }

        private TwoPointJointControlInfo _resolve(RopeJointControl jointControl)
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
                            if( crossControl.Id == jointControl.TargetNameA)
                            {
                                bodyControlA = bodyControl;
                                anchorA = crossControl.TranslatePoint(new Point(0, 0), bodyControl);
                            }
                            else if (crossControl.Id == jointControl.TargetNameB)
                            {
                                bodyControlB = bodyControl;
                                anchorB = crossControl.TranslatePoint(new Point(0, 0), bodyControl);
                            }
                        }
                    }
                }
            }

            return new TwoPointJointControlInfo(bodyControlA, bodyControlB, anchorA, anchorB);
        }
#if DEBUG
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }
#endif
        
        public StepViewModel StepViewModel
        { 
            get
            { 
                return new StepViewModel(_worldManager);
            }
        }


        public static string GetAngleJoint(DependencyObject obj)
        {
            return (string)obj.GetValue(AngleJointProperty);
        }

        public static void SetAngleJoint(DependencyObject obj, string value)
        {
            obj.SetValue(AngleJointProperty, value);
        }
        public static readonly DependencyProperty AngleJointProperty =
            DependencyProperty.RegisterAttached("AngleJoint", typeof(string), typeof(FarseerCanvas), new PropertyMetadata(null));
    }
}

