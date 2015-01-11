using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SM.WpfView
{
    public static class Helper
    {

        public static void LoadFarseer(IContainer container, Canvas rootCanvas, IContext context, out Info info, out Views views)
        {
            // prendo lo xaml e lo passo a Info che e' completamente agnostico
            info = new Info(container);

            // creo un wpf views creator che Views pilotera' e usera' per popolare le sue strutture a partire da info
            var wpfViewsCreator = new WpfViewsCreator(rootCanvas, context);

            var wpfViewsShapeCreator = new WpfShapeCreator();

            // Views e' completamente agnostico 
            views = new Views(wpfViewsCreator, wpfViewsShapeCreator);
        }
    }
}
