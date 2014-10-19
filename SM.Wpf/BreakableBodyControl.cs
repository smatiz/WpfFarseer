using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SM.Wpf
{
    [ContentPropertyAttribute("Shapes")]
    public class BreakableBodyControl : BodyControl, IBreakableBodyView
    {
        public BreakableBodyControl()
        {
            //Shapes = new ObservableCollection<ShapeControl>();
            //Shapes.CollectionChanged += Shapes_CollectionChanged;
        }

        


        //public ObservableCollection<ShapeControl> Shapes
        //{
        //    get { return (ObservableCollection<ShapeControl>)GetValue(ShapesProperty); }
        //    set { SetValue(ShapesProperty, value); }
        //}
        //public static readonly DependencyProperty ShapesProperty =
        //    DependencyProperty.Register("Shapes", typeof(ObservableCollection<ShapeControl>), typeof(BreakableBodyControl), new PropertyMetadata(null));

        //void Shapes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
        //    {
        //        foreach (var x in e.NewItems)
        //        {
        //            _canvas.Children.Add(((ShapeControl)x).Shape);
        //        }
        //    }
        //}

        public IEnumerable<IBodyView> Break()
        {
            _canvas.Children.Clear();
            foreach (var shape in Shapes)
            {
                var bc = new BodyControl();
                bc.BodyType = SM.BodyType.Dynamic;
                //bc.Id = part.Id;
                bc.Shapes.Add(shape);
                bc.RegisterToParent(_parent);
                bc.RotoTranslation = RotoTranslation;
                yield return bc;
            }
            //_parent = null;
        }

       

        //public IEnumerable<IShapeView> Shapes_Y
        //{
        //    get 
        //    {
        //        return from x in Shapes select x;
        //    }
        //}

        //protected override Brush Brush
        //{
        //    get { return new SolidColorBrush(Colors.Blue); }
        //}

        //protected override IEnumerable<Polygon> Polygons
        //{
        //    get
        //    {
        //        return from x in Shapes select (Polygon)x.Shape;
        //    }
        //}
    }
}
