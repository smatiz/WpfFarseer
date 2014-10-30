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
    [ContentPropertyAttribute("Content")]
    public class SkinnedBodyControl : BasicBodyControl, IBodyView
    {
        public static ISkinnableCanvas Skinner { private get; set; }


        public Canvas Content
        {
            get { return (Canvas)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(Canvas), typeof(SkinnedBodyControl), new PropertyMetadata(null, new PropertyChangedCallback(ContentPropertyChanged)));
        private static void ContentPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((SkinnedBodyControl)obj).OnContentChanged(); }
        VisualBrush _brush;
        List<ShapeControl> _shapes;
        private void OnContentChanged()
        {
            _brush = Content.GetVisualBrush();
            Content.UpdateLayout();

            var polys = Skinner.FindBorder(_brush, Content.RenderSize.Width, Content.RenderSize.Height, 1);

        }


        protected override Brush Brush { get { return _brush; } }
       
        public virtual BodyType BodyType
        {
            get { return (BodyType)GetValue(BodyTypeProperty); }
            set { SetValue(BodyTypeProperty, value); }
        }
        public static readonly DependencyProperty BodyTypeProperty =
            DependencyProperty.Register("BodyType", typeof(BodyType), typeof(SkinnedBodyControl), new PropertyMetadata(BodyType.Static));

        private IEnumerable<ShapeControl> Shapes_X__()
        {
            var s =  new ShapeControl();
            s.Points.Add(new Point(0, 0));
            s.Points.Add(new Point(0, 100));
            s.Points.Add(new Point(100, 100));
            s.Points.Add(new Point(100, 0));
            yield return s;
        }
        
        public IEnumerable<IShape> Shapes_X
        {
            get { return from x in Shapes_X__() select x; }
        }

        protected override IEnumerable<Polygon> Polygons
        {
            get
            {
                return from x in Shapes_X__() select (Polygon)x.Shape;
            }
        }
    }
}
