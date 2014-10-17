
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
using System.Collections.ObjectModel;
using System.Windows.Markup;

namespace WpfFarseer
{
    using xna = Microsoft.Xna.Framework;
    using FShape = FarseerPhysics.Collision.Shapes;
    using SM.Wpf;

    [ContentPropertyAttribute("FarseerObjects")]
    public partial class FarseerCanvas : Canvas
    {
        List<TwoPointJointControlManager> _ropeJointManager = new List<TwoPointJointControlManager>();
        DispatcherTimer _timer = new DispatcherTimer();

        FarseerWorldManager_old _worldManager;

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

            FarseerObjects = new ObservableCollection<BasicControl>();
            FarseerObjects.CollectionChanged += FarseerObjects_CollectionChanged;

            _timer.Tick += (s, e) => Update();
            _timer.Interval = new TimeSpan(0,0,0,0, 40);
            _worldManager = new FarseerWorldManager_old();

            Loaded += (s, e) =>
            {
                _controlUpdate();
                _timer.Start();
                foreach (var x in _farseerBehaviours)
                {
                    x.Start(_worldManager);
                }
            };
        }

        void FarseerObjects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {
                    Children.Add(((BasicControl)x)._canvas);
                }
            }
        }

        
        void _controlUpdate()
        {

            XamlInterpreter.xxx(Dispatcher, FarseerObjects);

            return;



            var tobeadded = new List<UIElement>();
           
            foreach (var child in FarseerObjects)
            {
                bool handled = false;
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {

                    {
                        var breakableBodyControl = child as BreakableBodyControl;
                        if (breakableBodyControl == null)
                        {
                            var autoBreakableBodyControl = child as AutoBreakableBodyControl;
                            if (autoBreakableBodyControl != null)
                            {
                                var polyF = autoBreakableBodyControl.Shape.Points.ToFarseerVertices();
                                var vss = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(polyF, (FarseerPhysics.Common.Decomposition.TriangulationAlgorithm)autoBreakableBodyControl.TriangulationAlgorithm);
                                var bbc = new BreakableBodyControl();

                                foreach (var p in vss)
                                {
                                    var bbcp = new BreakableBodyPartControl();
                                    bbcp.Shape.Points = p.ToWpf();
                                    bbc.Parts.Add(bbcp);
                                }

                                breakableBodyControl = bbc;
                                autoBreakableBodyControl._canvas.Visibility = System.Windows.Visibility.Hidden;
                                tobeadded.Add(breakableBodyControl._canvas);
                            }
                        }

                        if (breakableBodyControl != null)
                        {
                           // var shapes = breakableBodyControl.Parts.SelectMany<BodyControl, FShape.Shape>(c => (from x in c.Shapes select breakableBodyControl.ToFarseerShape(x)));

                            var shapes = from x in breakableBodyControl.Parts select breakableBodyControl._canvas.ToFarseerShape((Polygon)x.Shape.Shape);
                            _worldManager.AddBreakableBodyControl(breakableBodyControl, breakableBodyControl.Parts, shapes, breakableBodyControl._canvas.GetOrigin());
                            handled = true;
                        }
                    }
                    if(!handled)
                    {
                        var bodyControl = child as BodyControl;
                        if (bodyControl != null)
                        {
                            _worldManager.AddBodyControl(bodyControl, bodyControl._canvas.GetOrigin());
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
                    foreach (var crossControl in bodyControl.Flags)
                    {
                        if (crossControl != null)
                        {
                            if( crossControl.Id == jointControl.TargetNameA)
                            {
                                bodyControlA = bodyControl;
                                anchorA = crossControl.TranslatePoint(new Point(0, 0), bodyControl._canvas);
                            }
                            else if (crossControl.Id == jointControl.TargetNameB)
                            {
                                bodyControlB = bodyControl;
                                anchorB = crossControl.TranslatePoint(new Point(0, 0), bodyControl._canvas);
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

        public ObservableCollection<BasicControl> FarseerObjects
        {
            get { return (ObservableCollection<BasicControl>)GetValue(FarseerObjectsProperty); }
            set { SetValue(FarseerObjectsProperty, value); }
        }
        public static readonly DependencyProperty FarseerObjectsProperty =
            DependencyProperty.Register("FarseerObjects", typeof(ObservableCollection<BasicControl>), typeof(FarseerCanvas), new PropertyMetadata(null));

        
    }
}

