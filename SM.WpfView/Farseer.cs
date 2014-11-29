using SM;
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
using SM.WpfView;
using SM.Wpf;

namespace SM.WpfView
{
    [ContentPropertyAttribute("Children")]
    public partial class Farseer : BasicControl
    {
        public Farseer()
        {
            Children = new List<BasicControl>();
        }
      
        public void Load(RootView root)
        {
            // prendo lo xaml e lo passo a Info che e' completamente agnostico
            Info = new Info(Children);
            // creo un wpf views creator che Views pilotera' e usera' per popolare le sue strutture a partire da info
            var wpfViewsCreator = new WpfViewsCreator(root);
            var wpfViewsShapeCreator = new WpfShapeCreator();

            //var ftools = new WpfFarseerTools();
            // Views e' completamente agnostico 
            Views = new Views(wpfViewsCreator, wpfViewsShapeCreator, Info);
        }


        public Info Info { get; private set; }
        public Views Views { get; private set; }


        public bool Debug { get; set; }
    
        public static string GetAngleJoint(DependencyObject obj)
        {
            return (string)obj.GetValue(AngleJointProperty);
        }

        public static void SetAngleJoint(DependencyObject obj, string value)
        {
            obj.SetValue(AngleJointProperty, value);
        }
        public static readonly DependencyProperty AngleJointProperty =
            DependencyProperty.RegisterAttached("AngleJoint", typeof(string), typeof(Farseer), new PropertyMetadata(null));

        public List<BasicControl> Children
        {
            get { return (List<BasicControl>)GetValue(FarseerObjectsProperty); }
            set { SetValue(FarseerObjectsProperty, value); }
        }
        public static readonly DependencyProperty FarseerObjectsProperty =
            DependencyProperty.Register("Children", typeof(List<BasicControl>), typeof(Farseer), new PropertyMetadata(null));


    }
}
