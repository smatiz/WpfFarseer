using SM;
using SM.Wpf;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SM.Wpf
{
    [ContentPropertyAttribute("Shapes")]
    public class BodyControl : BasicBodyControl, IBodyView
    {

        public BodyControl()
        {
            Shapes = new ObservableCollection<ShapeControl>();
            Shapes.CollectionChanged += Shapes_CollectionChanged;
        }
        
        protected override Brush Brush { get { return DefaultBrush; } }
        public Brush DefaultBrush
        {
            private get { return (Brush)GetValue(DefaultBrushProperty); }
            set { SetValue(DefaultBrushProperty, value); }
        }
        public static readonly DependencyProperty DefaultBrushProperty =
            DependencyProperty.Register("DefaultBrush", typeof(Brush), typeof(BodyControl), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

        public virtual BodyType BodyType
        {
            get { return (BodyType)GetValue(BodyTypeProperty); }
            set { SetValue(BodyTypeProperty, value); }
        }
        public static readonly DependencyProperty BodyTypeProperty =
            DependencyProperty.Register("BodyType", typeof(BodyType), typeof(BodyControl), new PropertyMetadata(BodyType.Static));

        public ObservableCollection<ShapeControl> Shapes
        {
            get { return (ObservableCollection<ShapeControl>)GetValue(ShapesProperty); }
            set { SetValue(ShapesProperty, value); }
        }
        public static readonly DependencyProperty ShapesProperty =
            DependencyProperty.Register("Shapes", typeof(ObservableCollection<ShapeControl>), typeof(BodyControl), new PropertyMetadata(null));

        void Shapes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {
                    _canvas.Children.Add(((ShapeControl)x).Shape);
                }
            }
        }
        
        public IEnumerable<IShape> Shapes_X
        {
            get { return from x in Shapes select x; }
        }

        protected override IEnumerable<Polygon> Polygons
        {
            get
            {
                return from x in Shapes select (Polygon)x.Shape;
            }
        }
    }
}
