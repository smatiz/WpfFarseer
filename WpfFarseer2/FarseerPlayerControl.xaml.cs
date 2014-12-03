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

namespace WpfFarseer
{
    public partial class FarseerPlayerControl : CanvasId
    {
        FarseerWorldManager _worldManager;
        Context _context;
        RootView _root;

        public FarseerPlayerControl()
        {
            InitializeComponent();

            Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(1, 0, 0, 0));
            Loaded += FarseerPlayerControl_Loaded;
           
            Settings.MaxPolygonVertices = 100;

            
        }


        public Farseer Farseer
        {
            get { return (Farseer)GetValue(FarseerProperty); }
            set { SetValue(FarseerProperty, value); }
        }
        public static readonly DependencyProperty FarseerProperty =
            DependencyProperty.Register("Farseer", typeof(Farseer), typeof(FarseerPlayerControl), new PropertyMetadata(null, onFarseerChanged));
        private static void onFarseerChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ((FarseerPlayerControl)dependencyObject).onFarseerChanged();
        }
        private void onFarseerChanged()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;

            loadView();

            World world = new World(new Vector2(0, 10));

            // creo un farseer materials creator che Materials pilotera' e usera' per popolare le sue strutture a partire da info
            var farseerMaterialsCreator = new FarseerMaterialsCreator(world);

            var farseerMaterialsShapeCreator = new XamlShapeMaterialCreator(_context);
            // Materials e' completamente agnostico 
            var materials = new Materials(farseerMaterialsCreator, farseerMaterialsShapeCreator, _farseerInfo);
            // Synchronizers e' la struttura agnostica per tenere sincronizzato views e materials
            var synchronizers = new Synchronizers(_farseerViews, materials);

            _worldManager = new FarseerWorldManager(Id, synchronizers, new WatchView(), world);

            // devo farlo alla fine per risolvere le connessioni tra joint e body
            //materials.Finalize(_worldManager);

            stepControl.DataContext = new StepViewModel(_worldManager);

#if DEBUG
            if (Debug)
            {
                var debugCanvas = new Canvas();
                debugCanvas.IsHitTestVisible = false;
                var debugTextBlock = new TextBlock();
                debugCanvas.Width = Width;
                debugCanvas.Height = Height;
                farseerContainer.Children.Add(debugCanvas);
                farseerContainer.Children.Add(debugTextBlock);
                var debugView = new DebugViewWPF(debugCanvas, debugTextBlock, _worldManager.World);
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
        }
        private void loadView()
        {
            _context = new Context(Zoom);
            _root = new RootView(_context, farseerCanvas);
            loadFarseer();
            //farseerContainer.Children.Clear();
        }

        Info _farseerInfo;
        Views _farseerViews;

        private void loadFarseer()
        {

            // prendo lo xaml e lo passo a Info che e' completamente agnostico
            _farseerInfo = new Info(Id, Farseer.Children);
            // creo un wpf views creator che Views pilotera' e usera' per popolare le sue strutture a partire da info
            var wpfViewsCreator = new WpfViewsCreator(_root);
            var wpfViewsShapeCreator = new WpfShapeCreator();

            //var ftools = new WpfFarseerTools();
            // Views e' completamente agnostico 
            _farseerViews = new Views(wpfViewsCreator, wpfViewsShapeCreator, _farseerInfo);
        }
        


        public void AddBehaviour(IBehaviourView x)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
            _worldManager.AddViewBehaviour(x);
        }
        public void AddBehaviour(IBehaviourMaterial x)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
            _worldManager.AddMaterialBehaviour(x);
        }

        private void FarseerPlayerControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                _context = new Context(Zoom);
                _root = new RootView(_context, this);
                if (Farseer == null) throw new Exception("Farseer == null");
                loadFarseer();
                //farseerContainer.Children.Clear();

            }
            else if (MouseEnabled)
            {
                MouseDown += Farseer_MouseDown;
                MouseUp += Farseer_MouseUp;
                MouseMove += Farseer_MouseMove;
            }
        }
        private void Farseer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string id = getId(Mouse.DirectlyOver);
            if (id == null) return;
            var o = _worldManager.FindObject(id);

            fdyn.Body body;
            if (o is fdyn.Body)
            {
                body = (fdyn.Body)o;
            }
            else if (o is fdyn.BreakableBody)
            {
                body = ((fdyn.BreakableBody)o).MainBody;
            }
            else
            {
                return;
            }
            _worldManager.StartMouseJoint(body, new xna.Vector2((float)Mouse.GetPosition(this).X / Zoom, (float)Mouse.GetPosition(this).Y / Zoom));

        }
        private void Farseer_MouseMove(object sender, MouseEventArgs e)
        {
            _worldManager.UpdateMouseJoint(new xna.Vector2((float)Mouse.GetPosition(this).X / Zoom, (float)Mouse.GetPosition(this).Y / Zoom));
        }
        private void Farseer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _worldManager.StopMouseJoint();
        }
        private string getId(object x)
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

        public float Zoom
        {
            get { return (float)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }
        public static readonly DependencyProperty ZoomProperty =
            DependencyProperty.Register("Zoom", typeof(float), typeof(FarseerPlayerControl), new PropertyMetadata(1f));

        public bool Debug
        {
            get { return (bool)GetValue(DebugProperty); }
            set { SetValue(DebugProperty, value); }
        }
        public static readonly DependencyProperty DebugProperty =
            DependencyProperty.Register("Debug", typeof(bool), typeof(FarseerPlayerControl), new PropertyMetadata(false));

        public bool MouseEnabled
        {
            get { return (bool)GetValue(MouseEnabledProperty); }
            set { SetValue(MouseEnabledProperty, value); }
        }
        public static readonly DependencyProperty MouseEnabledProperty =
            DependencyProperty.Register("MouseEnabled", typeof(bool), typeof(FarseerPlayerControl), new PropertyMetadata(true));
    }
}
