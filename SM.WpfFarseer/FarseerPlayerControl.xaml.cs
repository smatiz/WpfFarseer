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
using SM.Xaml;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using SM.Wpf;
using System.Windows.Markup;
using System.Diagnostics;
using xna = Microsoft.Xna.Framework;
using fdyn = FarseerPhysics.Dynamics;

namespace SM.WpfFarseer
{
    [ContentPropertyAttribute("Farseer")]
    public partial class FarseerPlayerControl : UserControl
    {
        public event Action<FarseerWorldManager> Ready;

        private FarseerXaml _farseerXaml;



        public FarseerXaml FarseerXaml { get { return _farseerXaml; } }

        string Id { get;set; }
        public FarseerPlayerControl()
        {
            InitializeComponent();

            Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(1, 0, 0, 0));
            Loaded += FarseerPlayerControl_Loaded;
           
            Settings.MaxPolygonVertices = 100;
        }
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
                    _farseer = value;
                }
            }

        }

        [Conditional("DEBUG")]
        void debugCanvas(FarseerXaml fx)
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
                fx.ActivateDebug(debugCanvas, debugTextBlock);
            }
        }

        private bool oneTimeCalled = false;
        private void FarseerPlayerControl_Loaded(object sender, RoutedEventArgs e)
        {
          
            if (oneTimeCalled) return;
            oneTimeCalled = true;
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //return;
            }


            _farseerXaml = new FarseerXaml(Id, Zoom, MaxPolygonVertices, Gravity.ToFarseer(), _farseerResultContainers);

            _stepControl.DataContext = _farseerXaml.StepViewModel;

            _farseerXaml.Add(Farseer);

            debugCanvas(_farseerXaml);

            MouseDown += Farseer_MouseDown;
            MouseUp += Farseer_MouseUp;
            MouseMove += Farseer_MouseMove;

            if (Ready != null)
            {
                Ready(_farseerXaml. FarseerWorldManager);
            }
        }


        private void Farseer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _farseerXaml.StartMouseJoint();
            }
            else
            {
                var canvas = CanvasId.GetFirstCanvasId(Mouse.DirectlyOver as FrameworkElement);
                if (canvas == null) return;
                //var o = _worldManager.FindObject(canvas.Id);

            }
        }
        private void Farseer_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _farseerXaml.UpdateMouseJoint();
            }
            else
            {
                _farseerXaml.StopMouseJoint();
            }
        }
        private void Farseer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _farseerXaml.StopMouseJoint();
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
