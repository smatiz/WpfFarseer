
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
    using FarseerPhysics;
    using FarseerPhysics.Dynamics.Joints;
    using FarseerPhysics.Dynamics;
    using FarseerPhysics.Factories;

    [ContentPropertyAttribute("FarseerObjects")]
    public partial class FarseerCanvas : CanvasId, IContext
    {
        FarseerWorldManager _worldManager;
        RootControl _root;
     
        public void AddBehaviour(IViewBehaviour x)
        {
            _worldManager.AddViewBehaviour(x);
        }

        public void AddBehaviour(IMaterialBehaviour x)
        {
            _worldManager.AddMaterialBehaviour(x);
        }

        public FarseerCanvas()
        {
            InitializeComponent();

            Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));

            MouseDown += FarseerCanvas_MouseDown;
            MouseUp += FarseerCanvas_MouseUp;
            MouseMove += FarseerCanvas_MouseMove;

            Settings.MaxPolygonVertices = 100;

            Helper.FarseerTools = new FarseerTools();

            
            

            FarseerObjects = new ObservableCollection<BasicControl>();
            FarseerObjects.CollectionChanged += FarseerObjects_CollectionChanged;

            _worldManager = new FarseerWorldManager(Id, new ViewWatch());

            _root = new RootControl(this);

            bool loaded = false;
            Loaded += (s, e) =>
            {
                if (loaded) return;
                loaded = true;


                var views = XamlInterpreter.BuildViews(FarseerObjects);

                foreach (var x in views.Joints)
                {
                    Children.Add(((RopeJointControl)x).UIElement);
                }

                foreach (var x in views.Bodies)
                {
                    _worldManager.AddBodyView(x);
                }

                foreach (var x in views.Joints)
                {
                    _worldManager.AddRopeJointControl((IRopeJointView)x);
                }
                


#if DEBUG

                var debugCanvas = new Canvas();
                debugCanvas.IsHitTestVisible = false;
                var debugTextBlock = new TextBlock();
                debugCanvas.Width = Width;
                debugCanvas.Height = Height;
                Children.Add(debugCanvas);
                Children.Add(debugTextBlock);
                var debugView = new DebugViewWPF(debugCanvas, debugTextBlock, _worldManager.World);
                debugView.ScaleTransform = new ScaleTransform(Zoom, Zoom);
                var timer = new DispatcherTimer(DispatcherPriority.Render);
                timer.Interval = TimeSpan.FromMilliseconds(300);
                timer.Tick += (_s, _e) =>
                {
                    debugCanvas.Children.Clear();
                    if (Debug)
                    {
                        debugView.DrawDebugData();
                    }
                };
                timer.Start();
#endif
            };
        }


        void FarseerCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string id = getId(Mouse.DirectlyOver);
            if (id == null) return;
            var o = _worldManager.FindObject(id);

            Body body;
            if (o is Body)
            {
                body = (Body)o;
            }
            else if (o is BreakableBody)
            {
                body = ((BreakableBody)o).MainBody;
            }
            else
            {
                return;
            }
            _worldManager.StartMouseJoint(body, new xna.Vector2((float)Mouse.GetPosition(this).X / Zoom, (float)Mouse.GetPosition(this).Y / Zoom));

        } 
        void FarseerCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            _worldManager.UpdateMouseJoint(new xna.Vector2((float)Mouse.GetPosition(this).X / Zoom, (float)Mouse.GetPosition(this).Y / Zoom));
        }

        void FarseerCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _worldManager.StopMouseJoint();
        }


        string getId(object x)
        {
            var canvasId = x as CanvasId;
            if (canvasId != null)
            {
                return canvasId.Id;
            }
            var frameworkElement = x as FrameworkElement;
            if (frameworkElement != null)
            {
                return getId(frameworkElement.Parent);
            }

            return null;
        }


        void FarseerObjects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {
                    var bc = (BasicControl)x;
                    _root.AddChild(bc);
                    bc.Context = this;
                    //((BasicControl)x).RegisterCanvas(this.Children);
                }
            }
        }
        public StepViewModel StepViewModel
        {
            get
            {
                return new StepViewModel(_worldManager);
            }
        }


        public bool Debug
        {
            get;
            set;
        }


        public float Zoom
        {
            get { return (float)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }
        public static readonly DependencyProperty ZoomProperty =
            DependencyProperty.Register("Zoom", typeof(float), typeof(FarseerCanvas), new PropertyMetadata(1f, new PropertyChangedCallback(ZoomPropertyChanged)));
        private static void ZoomPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((FarseerCanvas)obj).OnZoomChanged(); }
        private void OnZoomChanged()
        {
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

