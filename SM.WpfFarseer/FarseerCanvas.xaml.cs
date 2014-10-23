
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
        FarseerWorldManager _worldManager;

        public void AddFarseerBehaviour(IViewBehaviour x)
        {
            _worldManager.AddViewBehaviour(x);
        }
        public FarseerCanvas()
        {
            InitializeComponent();

            if(Id == null || Id == "")
            {
                Id = BasicControl.AutoGenerateName();
            }

            FarseerObjects = new ObservableCollection<BasicControl>();
            FarseerObjects.CollectionChanged += FarseerObjects_CollectionChanged;

            _worldManager = new FarseerWorldManager(Id, new ViewWatch(Dispatcher));
            bool loaded = false;
            Loaded += (s, e) =>
            {
                if (loaded) return;
                loaded = true;
                bool b = System.ComponentModel.DesignerProperties.GetIsInDesignMode(this);
                XamlInterpreter.BuildFarseerWorldManager(Children, _worldManager, FarseerObjects, b);
            };
        }

        void FarseerObjects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {
                    ((BasicControl)x).AddToUIElementCollection(this.Children);
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


        public string Id { get; set; }
    }
}

