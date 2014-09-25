using SM;
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

        //IEnumerator<BasicCoroutine> eventCoroutine_Event()
        //{
        //    foreach (var wl in _worldLoops)
        //    {
        //        var x = wl(_worldManager);
        //        while (x.MoveNext())
        //        {
        //            yield return x.Current;
        //        }
        //    }
        //}

        void _controlUpdate()
        {
            var tobeadded = new List<UIElement>();
           
            foreach (var child in Children)
            {
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
                    var ropeJointControlInfo = _resolve(jointControl);

                    var line = new Line();
                    line.Stroke = new SolidColorBrush(Colors.Green);
                    line.StrokeThickness = 1;
                    tobeadded.Add(line);

                    _ropeJointManager.Add(new TwoPointJointControlManager(this, ropeJointControlInfo, line));

                    if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    {
                        _worldManager.AddRopeJoint(ropeJointControlInfo.ToFarseer(_worldManager), jointControl);
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

        //public bool Savable
        //{
        //    get
        //    {
        //        if (WorldManager == null) return false;
        //        return WorldManager.Savable;
        //    }
        //}

 
        //private BodyControl _find(string name) 
        //{
        //    foreach (var child in Children)
        //    {
        //        var bodyControl = child as BodyControl;
        //        if (bodyControl != null && bodyControl.Id == name)
        //        {
        //            return bodyControl;
        //        }
        //    }
        //    return null;
        //}

        /*class WorldCoroutine : EventCoroutine
        {
            protected override IEnumerator<BasicCoroutine> DoIt()
            {
                throw new NotImplementedException();
            }
        }*/


       /* public EventCoroutine WorldCoroutine
        {

        }*/

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
            //WpfDebugView.Instance.Draw(drawingContext);
        }
#endif
        

        //public StepViewModel StepViewModel
        //{
        //    get { return (StepViewModel)GetValue(StepViewModelProperty); }
        //    set { SetValue(StepViewModelProperty, value); }
        //}
        //public static readonly DependencyProperty StepViewModelProperty =
        //    DependencyProperty.Register("StepViewModel", typeof(StepViewModel), typeof(FarseerCanvas), new PropertyMetadata(null, OnStepViewModelChanged));
        //private static void OnStepViewModelChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        //{
        //    var _this = (FarseerCanvas)dependencyObject;
        //    _this.StepViewModel.WorldManager = _this.WorldManager;
        //}

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

        // Using a DependencyProperty as the backing store for AngleJoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleJointProperty =
            DependencyProperty.RegisterAttached("AngleJoint", typeof(string), typeof(FarseerCanvas), new PropertyMetadata(null));

        

    }
}

