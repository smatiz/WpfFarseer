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
                    //_farseerXaml.Clear();
                    //_farseerCanvas.Children.Remove(_farseer); 
                    _farseer = value;
                    if (_farseer != null)
                    {
                        //_farseerXaml.Add(_farseer);
                        //_farseerCanvas.Children.Add(_farseer);
                    }
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
                return;
            }

            Settings.MaxPolygonVertices = MaxPolygonVertices;

            //var context = new Context(Zoom);

            _farseerXaml = new FarseerXaml(Id, Zoom, Gravity.ToFarseer(), _farseerResultContainers);
            _stepControl.DataContext = _farseerXaml.StepViewModel;

            _farseerXaml.Add(Farseer);
//            var Code = @"
//        <Page
//            xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006"" 
//            xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
//            xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
//            xmlns:sm=""clr-namespace:SM.Xaml;assembly=SM.Xaml""
//            xmlns:smf=""clr-namespace:SM.WpfFarseer;assembly=SM.WpfFarseer""
//            xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">
//                <sm:Farseer  Id=""s9"">
//                    <sm:Body BodyType=""Dynamic"">
//                        <sm:Polygon  Points=""5,0,5,5,10,4"" Fill=""GreenYellow""  Stroke=""Black"" StrokeThickness=""1""/>
//                    </sm:Body>
//                    <sm:Body BodyType=""Static"">
//                        <sm:Polygon  Points=""5,15,5,20,10,24"" Fill=""Red""  Stroke=""Black"" StrokeThickness=""1""/>
//                    </sm:Body>
//                </sm:Farseer>
//        </Page>";
//            var page = (Page)System.Windows.Markup.XamlReader.Load(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(Code ?? "")));
//            var farseer = page.Content as BasicContainer;

//           _farseerXaml.Add(page.Content as BasicContainer);
            

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
                var canvas = GetFirstCanvasId(Mouse.DirectlyOver as FrameworkElement);
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
        public static CanvasId GetFirstCanvasId(FrameworkElement frameworkElement)
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
                    return GetFirstCanvasId(parentFrameworkElement);
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
