using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication1
{



    public class AddableCanvas : Canvas, IObjectAddable
    {
        static int i = 10;


        public AddableCanvas()
        {
        }


        public void AddOnMouse()
        {
            System.Windows.Point p = Mouse.GetPosition(this);
            var cross = new Cross() { ABC = i++ };
            Canvas.SetLeft(cross, p.X);
            Canvas.SetTop(cross, p.Y);
            Children.Add(cross);
        }


        public void Remove(Cross p)
        {
            Children.Remove(p);
        }


        bool _selected;
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    //BorderThickness = _selected ? new Thickness(2) : new Thickness(0);
                    //PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
                }
            }
        }


    }





    //public class AddableCanvas : Border, IObjectAddable
    //{
    //    Canvas _canvas = new Canvas();
    //    static int i = 10;


    //    public AddableCanvas()
    //    {
    //        _canvas.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
    //        _canvas.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
    //        Child = _canvas;
    //        BorderBrush = new SolidColorBrush(Colors.Red);
    //        CornerRadius = new CornerRadius(2);
    //    }


    //    public void AddOnMouse()
    //    {
    //        System.Windows.Point p = Mouse.GetPosition(this);
    //        var cross = new Cross() { ABC = i++};
    //        Canvas.SetLeft(cross, p.X);
    //        Canvas.SetTop(cross, p.Y);
    //        _canvas.Children.Add(cross);
    //    }


    //    public void Remove(Cross p)
    //    {
    //        _canvas.Children.Remove(p);
    //    }


    //    bool _selected;
    //    public bool Selected
    //    {
    //        get
    //        {
    //            return _selected;
    //        }
    //        set
    //        {
    //            if (_selected != value)
    //            {
    //                _selected = value;
    //                BorderThickness = _selected ? new Thickness(2) : new Thickness(0);
    //                //PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
    //            }
    //        }
    //    }

    //    //public event PropertyChangedEventHandler PropertyChanged;

    //}
}
