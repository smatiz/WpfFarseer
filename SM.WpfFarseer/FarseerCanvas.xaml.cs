﻿
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

    [ContentPropertyAttribute("FarseerObjects")]
    public partial class FarseerCanvas : Canvas
    {
        FarseerWorldManager _worldManager;
        
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

            Settings.MaxPolygonVertices = 100;

            SkinnedBodyControl.Skinner = new CanvasSkinner();

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
                var views = XamlInterpreter.BuildViews(Children, FarseerObjects);

                foreach(var x in views.Bodies)
                {
                    _worldManager.AddBodyView(x);
                }
                foreach (var x in views.BreakableBodies)
                {
                    _worldManager.AddBreakableBodyView(x);
                }
                foreach (var x in views.Joints)
                {
                    _worldManager.AddRopeJointControl((IRopeJointView)x);
                }

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

