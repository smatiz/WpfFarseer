using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WpfApplication1
{
    public interface IObjectAddable
    {
        void AddOnMouse();
        void Remove(Cross p);

        bool Selected { set; }
    }

    public interface ISelectableObject
    {
        UIElement Movable { get; }
        UIElement Hover { get; }
        UIElement Selection { get; }
        bool Selected { get;  set; }
    }
}
