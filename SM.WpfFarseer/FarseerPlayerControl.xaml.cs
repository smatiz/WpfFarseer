using FarseerPhysics;
using SM;
using SM.Farseer;
using SM.WpfView;
using SM.WpfFarseer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using xna = Microsoft.Xna.Framework;
using fdyn = FarseerPhysics.Dynamics;
using SM.Xaml;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using SM.Wpf;
using System.Windows.Markup;
using System.Diagnostics;

namespace SM.WpfFarseer
{
    [ContentPropertyAttribute("Farseer")]
    public partial class FarseerPlayerControl : UserControl
    {
        public event Action<FarseerWorldManager> Ready;

        FarseerWorldManager _worldManager;
        Context _context;


        string Id { get;set; }
        public FarseerPlayerControl()
        {
            InitializeComponent();

            Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(1, 0, 0, 0));
            Loaded += FarseerPlayerControl_Loaded;
           
            Settings.MaxPolygonVertices = 100;
        }

        Info _farseerInfo;
        Views _farseerViews;
        private BasicContainer _farseer;
        public BasicContainer Farseer
        {
            get
            {
                return _farseer;
            }
            set
            {
                if (_farseer != value)
                {
                    _farseerCanvas.Children.Remove(_farseer); 
                    _farseer = value;
                    _farseerCanvas.Children.Add(_farseer); 

                }
            }

        }


        //public void AddBehaviour(IBehaviourView x)
        //{
        //    if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
        //    _worldManager.AddViewBehaviour(x);
        //}
        //public void AddBehaviour(IBehaviourMaterial x)
        //{
        //    if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
        //    _worldManager.AddMaterialBehaviour(x);
        //}

        private bool oneTimeCalled = false;
        private void FarseerPlayerControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (oneTimeCalled) return;
            oneTimeCalled = true;

            _context = new Context(Zoom);

            SM.WpfView.Helper.LoadFarseer(Farseer, _farseerCanvas, _context, out _farseerInfo, out _farseerViews);

            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            World world = new World(new Vector2(0, 10));

            // creo un farseer materials creator che Materials pilotera' e usera' per popolare le sue strutture a partire da info
            var farseerMaterialsCreator = new FarseerMaterialsCreator(world);

            var farseerMaterialsShapeCreator = new XamlShapeMaterialCreator(_context);
            // Materials e' completamente agnostico 
            var materials = new Materials(farseerMaterialsCreator, farseerMaterialsShapeCreator, _farseerInfo);
            // Synchronizers e' la struttura agnostica per tenere sincronizzato views e materials
            var synchronizers = new Synchronizers(_farseerViews, materials);

            _worldManager = new FarseerWorldManager(Id, synchronizers, _farseerInfo, new WatchView(), world);

            _stepControl.DataContext = new StepViewModel(_worldManager);

#if DEBUG
            if (InDebug)
            {
                var debugCanvas = new Canvas();
                debugCanvas.IsHitTestVisible = false;
                var debugTextBlock = new TextBlock();
                debugTextBlock.IsHitTestVisible = false;
                debugTextBlock.Name = "debugTextBlock";
                _farseerContainer.Children.Add(debugTextBlock);
                debugCanvas.Width = Width;
                debugCanvas.Height = Height;
                _farseerContainer.Children.Add(debugCanvas);
                var debugView = new DebugViewWPF(debugCanvas, debugTextBlock, _worldManager.World);
                debugView.Flags = debugView.Flags | DebugViewFlags.DebugPanel;
                debugView.ScaleTransform = new ScaleTransform(Zoom, Zoom);
                var timer = new DispatcherTimer(DispatcherPriority.Render);
                timer.Interval = TimeSpan.FromMilliseconds(300);
                timer.Tick += (_s, _e) =>
                {
                    debugCanvas.Children.Clear();
                    debugView.DrawDebugData();
                };
                timer.Start();
            }
#endif
            MouseDown += Farseer_MouseDown;
            MouseUp += Farseer_MouseUp;
            MouseMove += Farseer_MouseMove;

            if (Ready != null)
            {
                Ready(_worldManager);
            }
        }
        private void Farseer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var canvas = getFirstCanvasId(Mouse.DirectlyOver as FrameworkElement);
            if (canvas == null) return;
            var o = _worldManager.FindObject(canvas.Id);

            fdyn.Body body;
            if (o is fdyn.Body)
            {
                body = (fdyn.Body)o;
                Debug.WriteLine("Mouse Down : Body {0} : FullId {1}", canvas.Id, canvas.Id);
            }
            else if (o is fdyn.BreakableBody)
            {
                body = ((fdyn.BreakableBody)o).MainBody;
                Debug.WriteLine("Mouse Down : BreakableBody {0} : FullId {1}", canvas.Id, canvas.Id);
            }
            else
            {
                Debug.WriteLine("Mouse Down : null {0} : FullId {1}", canvas.Id, canvas.Id);
                return;
            }

            _worldManager.StartMouseJoint(body, new xna.Vector2((float)Mouse.GetPosition(_farseerCanvas).X / Zoom, (float)Mouse.GetPosition(_farseerCanvas).Y / Zoom));

        }
        private void Farseer_MouseMove(object sender, MouseEventArgs e)
        {
            _worldManager.UpdateMouseJoint(new xna.Vector2((float)Mouse.GetPosition(_farseerCanvas).X / Zoom, (float)Mouse.GetPosition(_farseerCanvas).Y / Zoom));
        }
        private void Farseer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _worldManager.StopMouseJoint();
        }
        private CanvasId getFirstCanvasId(FrameworkElement frameworkElement)
        {
            var canvasId = frameworkElement as CanvasId;
            if (canvasId != null)
            {
                return canvasId;
            }
            if (frameworkElement != null)
            {
                var parentFrameworkElement = frameworkElement.Parent as FrameworkElement;
                if (frameworkElement != null)
                {
                    return getFirstCanvasId(parentFrameworkElement);
                }
            }

            return null;
        }
        //private string getId(object x)
        //{
        //    var canvasId = x as CanvasId;
        //    if (canvasId != null)
        //    {
        //        return canvasId.Id;
        //    }
        //    var frameworkElement = x as FrameworkElement;
        //    if (frameworkElement != null)
        //    {
        //        return getId(frameworkElement.Parent);
        //    }

        //    return null;
        //}

        public float Zoom
        {
            get { return (float)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }
        public static readonly DependencyProperty ZoomProperty =
            DependencyProperty.Register("Zoom", typeof(float), typeof(FarseerPlayerControl), new PropertyMetadata(1f));

        public bool InDebug
        {
            get { return (bool)GetValue(InDebugProperty); }
            set { SetValue(InDebugProperty, value); }
        }
        public static readonly DependencyProperty InDebugProperty =
            DependencyProperty.Register("InDebug", typeof(bool), typeof(FarseerPlayerControl), new PropertyMetadata(false));

        public bool MouseEnabled
        {
            get { return (bool)GetValue(MouseEnabledProperty); }
            set { SetValue(MouseEnabledProperty, value); }
        }
        public static readonly DependencyProperty MouseEnabledProperty =
            DependencyProperty.Register("MouseEnabled", typeof(bool), typeof(FarseerPlayerControl), new PropertyMetadata(true));
    }
}
