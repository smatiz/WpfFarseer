using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using SM.Farseer;
using SM.WpfView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using SM.Wpf;

using xna = Microsoft.Xna.Framework;
using fdyn = FarseerPhysics.Dynamics;

namespace SM.WpfFarseer
{
    public class FarseerXaml
    {
        FarseerWorldManager _worldManager;
        StepViewModel _stepViewModel;
        Canvas _farseerResultContainers;
        MouseJointManager _mouseJointManager = null;
        float _zoom;
        public FarseerXaml(string id, float zoom, int maxPolygonVertices, Vector2 gravity, Canvas farseerResultContainers)
        {
            Settings.MaxPolygonVertices = maxPolygonVertices;

            var context = new Context(zoom);
            _zoom = zoom;
            _farseerResultContainers = farseerResultContainers;

            var canvasCreated = new Action<CanvasId>(canvasId => { _farseerResultContainers.Children.Add(canvasId); });

            // creo un wpf views creator che Views pilotera' e usera' per popolare le sue strutture a partire da info
            var wpfViewsCreator = new WpfViewsCreator(canvasCreated, context);
            var wpfViewsShapeCreator = new WpfShapeCreator();

            World world = new World(gravity);

            _mouseJointManager = new MouseJointManager(world);


            // creo un farseer materials creator che Materials pilotera' e usera' per popolare le sue strutture a partire da info
            var farseerMaterialsCreator = new FarseerMaterialsCreator(world);
            var farseerMaterialsShapeCreator = new XamlShapeMaterialCreator(context);

            // Views e' completamente agnostico 
            var farseerViews = new Views(wpfViewsCreator, wpfViewsShapeCreator);
            // Materials e' completamente agnostico 
            var materials = new Materials(farseerMaterialsCreator, farseerMaterialsShapeCreator);

            // Synchronizers e' la struttura agnostica per tenere sincronizzato views e materials
            var synchronizers = new Synchronizers(farseerViews, materials);

            _worldManager = new FarseerWorldManager(id, synchronizers, new WatchView(), world);

            _stepViewModel = new StepViewModel(_worldManager);
        }

        public StepViewModel StepViewModel { get { return _stepViewModel; } }
        public FarseerWorldManager FarseerWorldManager { get { return _worldManager; } }

        public void Add(BasicContainer farseerContainer)
        {
            _worldManager.Add(new Info(farseerContainer));
        }
        public void Add(string xamlCode)
        {

            var xamlCodePage = String.Format("{0}{1}{2}", OpenXamlPage, xamlCode, CloseXamlPage);
            var page = (Page)System.Windows.Markup.XamlReader.Load(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(xamlCodePage ?? "")));
            Add(page.Content as BasicContainer);
        }
        public void Clear()
        {
            _worldManager.Clear();
            _farseerResultContainers.Children.Clear();
        }

        [Conditional("DEBUG")]
        public void ActivateDebug(Canvas debugCanvas, TextBlock debugTextBlock)
        {
            World w = null;
            _worldManager.GetWorld(ref w);
            var debugView = new DebugViewWPF(debugCanvas, debugTextBlock, w);
            debugView.Flags = debugView.Flags | DebugViewFlags.DebugPanel;
            debugView.ScaleTransform = new ScaleTransform(_zoom, _zoom);
            var timer = new DispatcherTimer(DispatcherPriority.Render);
            timer.Interval = TimeSpan.FromMilliseconds(300);
            timer.Tick += (_s, _e) =>
            {
                debugCanvas.Children.Clear();
                debugView.DrawDebugData();
            };
            timer.Start();
        }

        public void StartMouseJoint()
        {
            var canvas = CanvasId.GetFirstCanvasId(Mouse.DirectlyOver as FrameworkElement);
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

            _mouseJointManager.StartMouseJoint(body, new xna.Vector2((float)Mouse.GetPosition(_farseerResultContainers).X / _zoom, (float)Mouse.GetPosition(_farseerResultContainers).Y / _zoom));
        }
        public void UpdateMouseJoint()
        {
            _mouseJointManager.UpdateMouseJoint(new xna.Vector2((float)Mouse.GetPosition(_farseerResultContainers).X / _zoom, (float)Mouse.GetPosition(_farseerResultContainers).Y / _zoom));
        }
        public void StopMouseJoint()
        {
            _mouseJointManager.StopMouseJoint();
        }

        public static string OpenXamlPage
        {
            get
            {
                return @"
        <Page
            xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006"" 
            xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
            xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
            xmlns:sm=""clr-namespace:SM.Xaml;assembly=SM.Xaml""
            xmlns:smf=""clr-namespace:SM.WpfFarseer;assembly=SM.WpfFarseer""
            xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">";
            }
        }

        public static string CloseXamlPage
        {
            get
            {
                return @"</Page>";
            }
        }

        public void Add()
        {
            Add(ExampleXamlCode());
        }
        static int x = 0;
        public static string ExampleXamlCode()
        {
            x += 10;
                return @"
                <sm:Farseer  Id=""s9"">
                    <sm:Layer Transform="""+x.ToString()+@",10"">
                        <sm:Body BodyType=""Dynamic"">
                            <sm:Polygon  Points=""5,0,5,5,10,4"" Fill=""GreenYellow""  Stroke=""Black"" StrokeThickness=""1""/>
                        </sm:Body>
                    </sm:Layer>
                </sm:Farseer>";
        }
    }
}
