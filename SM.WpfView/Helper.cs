using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.WpfView
{
    public static class Helper
    {
        public static void LoadFarseer(string id, IEnumerable<IDescriptor> descriptors, RootView root, out Info info, out Views views)
        {
            // prendo lo xaml e lo passo a Info che e' completamente agnostico
            info = new Info(id, descriptors);
            // creo un wpf views creator che Views pilotera' e usera' per popolare le sue strutture a partire da info
            var wpfViewsCreator = new WpfViewsCreator(root);
            var wpfViewsShapeCreator = new WpfShapeCreator();

            //var ftools = new WpfFarseerTools();
            // Views e' completamente agnostico 
            views = new Views(wpfViewsCreator, wpfViewsShapeCreator, info);
        }
    }
}
