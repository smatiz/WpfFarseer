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
        DebugContext _context;
        RootView _root;
        public FarseerPlayerControl()
        {
            InitializeComponent();

            Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(1, 0, 0, 0));

            MouseDown += Farseer_MouseDown;
            MouseUp += Farseer_MouseUp;
            MouseMove += Farseer_MouseMove;

            Settings.MaxPolygonVertices = 100;

            _context = new DebugContext() { Zoom = 1 };
            _root = new RootView(_context, this);

            //SM.WpfView.Helper.FarseerTools = new FarseerTools();

            //_context = new DefaultContext();
            //_root = new RootView(_context, this);

#if DEBUG
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                Loaded += (sender, e) =>
                {
                    Farseer.Load(_root);
                };
            }
#endif
        }

        void FarseerPlayerControl_Loaded(object sender, RoutedEventArgs e)
        {

           
            //if (Farseer == null)
            //    throw new Exception("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            //else
            //    throw new Exception("bbbbbbbbbbbbbbbbbbbbb");
            //Farseer.Load(this);
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


            Farseer.Load(_root);


            //_this.stepControl.DataContext = _this.Farseer.StepViewModel;
            {



                World world = new World(new Vector2(0, 10));

                

                // creo un farseer materials creator che Materials pilotera' e usera' per popolare le sue strutture a partire da info
                var farseerMaterialsCreator = new FarseerMaterialsCreator(world);

                var farseerMaterialsShapeCreator = new XamlShapeMaterialCreator(_context);
                // Materials e' completamente agnostico 
                var materials = new Materials(farseerMaterialsCreator, farseerMaterialsShapeCreator, Farseer.Info);
                // Synchronizers e' la struttura agnostica per tenere sincronizzato views e materials
                var synchronizers = new Synchronizers(Farseer.Views, materials);

                _worldManager = new FarseerWorldManager(Id, synchronizers, new WatchView(), world);
                stepControl.DataContext = new StepViewModel(_worldManager);

            }

            farseerContainer.Children.Clear();
            //farseerContainer.Children.Add(Farseer);

            //bool loaded = false;
            //Loaded += (s, e) =>
            {
                //if (loaded) return;
                //loaded = true;


#if DEBUG

                var debugCanvas = new Canvas();
                debugCanvas.IsHitTestVisible = false;
                var debugTextBlock = new TextBlock();
                debugCanvas.Width = Width;
                debugCanvas.Height = Height;
                farseerContainer.Children.Add(debugCanvas);
                farseerContainer.Children.Add(debugTextBlock);
                var debugView = new DebugViewWPF(debugCanvas, debugTextBlock, _worldManager.World);



                //Grid.SetRow(debugCanvas, 1);
                //debugCanvas.Background = new SolidColorBrush(Color.FromArgb(150,250,0,0));
                Zoom = 1;


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




     
        public void AddBehaviour(IBehaviourView x)
        {
            _worldManager.AddViewBehaviour(x);
        }

        public void AddBehaviour(IBehaviourMaterial x)
        {
            _worldManager.AddMaterialBehaviour(x);
        }



        void Farseer_MouseDown(object sender, MouseButtonEventArgs e)
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
        void Farseer_MouseMove(object sender, MouseEventArgs e)
        {
            _worldManager.UpdateMouseJoint(new xna.Vector2((float)Mouse.GetPosition(this).X / Zoom, (float)Mouse.GetPosition(this).Y / Zoom));
        }

        void Farseer_MouseUp(object sender, MouseButtonEventArgs e)
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
                    //_root.AddChild(bc);
                   // bc.Context = this;

                }
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
            DependencyProperty.Register("Zoom", typeof(float), typeof(FarseerPlayerControl), new PropertyMetadata(1f, new PropertyChangedCallback(ZoomPropertyChanged)));
        private static void ZoomPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((FarseerPlayerControl)obj).OnZoomChanged(); }
        private void OnZoomChanged()
        {
            _context.Zoom = Zoom;
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
            DependencyProperty.RegisterAttached("AngleJoint", typeof(string), typeof(FarseerPlayerControl), new PropertyMetadata(null));
       
    }
}
