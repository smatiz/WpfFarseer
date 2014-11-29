using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SM.WpfView
{
    public interface IDrawable
    {
        UIElement UIElement { get; }
    }
}
