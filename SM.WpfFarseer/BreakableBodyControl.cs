using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using SM.Farseer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Shapes;
using WpfFarseer;

namespace WpfFarseer
{
    [ContentPropertyAttribute("Parts")]
    public class BreakableBodyControl : BodyControl
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
    }
}
