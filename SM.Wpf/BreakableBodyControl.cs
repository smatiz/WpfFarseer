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
    [ContentPropertyAttribute("Parts")]
    public class BreakableBodyControl : BasicBodyControl, IBreakableBodyView
    {
        public BreakableBodyControl()
        {
            Parts = new ObservableCollection<BreakableBodyPartControl>();
        }
        
        public ObservableCollection<BreakableBodyPartControl> Parts
        {
            get { return (ObservableCollection<BreakableBodyPartControl>)GetValue(PartsProperty); }
            set { SetValue(PartsProperty, value); }
        }
        public static readonly DependencyProperty PartsProperty =
            DependencyProperty.Register("Parts", typeof(ObservableCollection<BreakableBodyPartControl>), typeof(BreakableBodyControl), new PropertyMetadata(null));


        public IEnumerable<IBodyView> Break()
        {
            foreach(var part in Parts)
            {
                var bc = new BodyControl();
                bc.BodyType = SM.BodyType.Dynamic;
                bc.Id = part.Id;
                bc.Shapes.Add(part.Shape);
                bc.RegisterTo(_parent);
                yield return bc;
            }
        }

       

        public IEnumerable<IShapeView> Shapes_Y
        {
            get 
            { 
                return from x in Parts select x.Shape;
            }
        }

        protected override Brush Brush
        {
            get { return new SolidColorBrush(Colors.Blue); }
        }

        protected override IEnumerable<Polygon> Polygons
        {
            get
            {
                return from x in Parts select (Polygon)x.Shape.Shape;
            }
        }
    }
}
