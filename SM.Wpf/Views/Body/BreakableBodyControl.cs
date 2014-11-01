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
    public class BreakableBodyControl : BodyControl, IBreakableBodyView
    {
        public IEnumerable<IBodyView> Break()
        {
            _canvas.Children.Clear();
            foreach (var shape in Shapes)
            {
                var bc = new BodyControl();
                bc.BodyType = SM.BodyType.Dynamic;
                bc.Shapes.Add(shape);
                bc.AddToUIElementCollection(_parentChildrens);
                bc.RotoTranslation = RotoTranslation;
                yield return bc;
            }
        }
    }
}
