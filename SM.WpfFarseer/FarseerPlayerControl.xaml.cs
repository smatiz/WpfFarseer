﻿using FarseerPhysics;
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
        //Context _context;

        string Id { get;set; }
        public FarseerPlayerControl()
        {
            InitializeComponent();

            Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(1, 0, 0, 0));
            Loaded += FarseerPlayerControl_Loaded;
           
            Settings.MaxPolygonVertices = 100;
        }

        Info _farseerInfo;
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

        [Conditional("DEBUG")]
        void debugCanvas()
        {
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
        }

        WpfFarseerWorldManager _manager = new WpfFarseerWorldManager();


        private bool oneTimeCalled = false;
        private void FarseerPlayerControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (oneTimeCalled) return;
            oneTimeCalled = true;
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            Settings.MaxPolygonVertices = MaxPolygonVertices;

            var context = new Context(Zoom);

            // creo un wpf views creator che Views pilotera' e usera' per popolare le sue strutture a partire da info
            var wpfViewsCreator = new WpfViewsCreator(_farseerCanvas, context);
            var wpfViewsShapeCreator = new WpfShapeCreator();

            World world = new World(Gravity.ToFarseer());
            // creo un farseer materials creator che Materials pilotera' e usera' per popolare le sue strutture a partire da info
            var farseerMaterialsCreator = new FarseerMaterialsCreator(world);
            var farseerMaterialsShapeCreator = new XamlShapeMaterialCreator(context);
            // Views e' completamente agnostico 
            var farseerViews = new Views(wpfViewsCreator, wpfViewsShapeCreator);
            // Materials e' completamente agnostico 
            var materials = new Materials(farseerMaterialsCreator, farseerMaterialsShapeCreator);

            // Synchronizers e' la struttura agnostica per tenere sincronizzato views e materials
            var synchronizers = new Synchronizers(farseerViews, materials);




            // prendo lo xaml e lo passo a Info che e' completamente agnostico
            _farseerInfo = new Info(Farseer);



            //farseerViews.Add(_farseerInfo);
            //materials.Add(_farseerInfo);

            synchronizers.Add(_farseerInfo);

            _worldManager = new FarseerWorldManager(Id, synchronizers, _farseerInfo.Bodies.Select(x=> x.Id).Concat(_farseerInfo.Joints.Select(x=> x.Id)), new WatchView(), world);






            _stepControl.DataContext = new StepViewModel(_worldManager);

            debugCanvas();

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
            if (e.LeftButton == MouseButtonState.Pressed)
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
            else
            {
                var canvas = getFirstCanvasId(Mouse.DirectlyOver as FrameworkElement);
                if (canvas == null) return;
                var o = _worldManager.FindObject(canvas.Id);

            }
        }
        private void Farseer_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _worldManager.UpdateMouseJoint(new xna.Vector2((float)Mouse.GetPosition(_farseerCanvas).X / Zoom, (float)Mouse.GetPosition(_farseerCanvas).Y / Zoom));
            }
            else
            {
                _worldManager.StopMouseJoint();
            }
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

        public int MaxPolygonVertices
        {
            get { return (int)GetValue(MaxPolygonVerticesProperty); }
            set { SetValue(MaxPolygonVerticesProperty, value); }
        }
        public static readonly DependencyProperty MaxPolygonVerticesProperty =
            DependencyProperty.Register("MaxPolygonVertices", typeof(int), typeof(FarseerPlayerControl), new PropertyMetadata(100));

        public float2 Gravity
        {
            get { return (float2)GetValue(GravityProperty); }
            set { SetValue(GravityProperty, value); }
        }
        public static readonly DependencyProperty GravityProperty =
            DependencyProperty.Register("Gravity", typeof(float2), typeof(FarseerPlayerControl), new PropertyMetadata(float2.Null));

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
