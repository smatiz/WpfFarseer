using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace SM.Wpf
{
    [ContentPropertyAttribute("Parts")]
    public class BreakableBodyControl : BodyControl, IBreakableBodyView
    {
        public override SM.BodyType BodyType
        {
            get
            {
                return SM.BodyType.Dynamic;
            }
         }

        public BreakableBodyControl()
        {
            Parts = new ObservableCollection<BreakableBodyPartControl>();
            Parts.CollectionChanged += Parts_CollectionChanged;
        }

        void Parts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {
                    _canvas.Children.Add(((BreakableBodyPartControl)x)._canvas);
                }
            }
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
                yield return bc;
            }
        }
    }
}
