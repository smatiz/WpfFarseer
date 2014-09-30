using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication1
{
    public class SaverCanvas : Canvas, IObjectAddable
    {
        public SaverCanvas()
        {
            Background = new SolidColorBrush(Colors.White);
            MouseDown += SaverCanvas_MouseDown;
            MouseMove += SaverCanvas_MouseMove;
            MouseUp += SaverCanvas_MouseUp;
        }


        IObjectAddable _selectedAddable;

        private Point _startingPoint;
        private Point _startingCanvasPos;
        private Cross _selectedCross;

        void SaverCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if(_selectedCross != null)
                {
                    var p = Mouse.GetPosition(this);
                    Canvas.SetLeft(_selectedCross, _startingCanvasPos.X + p.X - _startingPoint.X);
                    Canvas.SetTop(_selectedCross, _startingCanvasPos.Y + p.Y - _startingPoint.Y);
                }
            }
            else
            {
                _selectedCross = null;
            }
        }

        void SaverCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _selectedCross = null;
        }
       
      
        void SaverCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _selectedCross = null;
            if (Keyboard.Modifiers== ModifierKeys.None)
            {
                trySelect(Mouse.DirectlyOver as FrameworkElement);
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                var x = Mouse.DirectlyOver;
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    var cross = x as Cross;
                    if (cross != null)
                    {
                        _startingCanvasPos = new Point(Canvas.GetLeft(cross), Canvas.GetTop(cross));
                        _startingPoint = Mouse.GetPosition(this);
                        _selectedCross = cross;
                    }
                    else
                    {
                        if (_selectedAddable != null)
                        {
                            _selectedAddable.AddOnMouse();
                        }
                    }

                }
                else if (e.MiddleButton == MouseButtonState.Pressed)
                {
                    var cross = x as Cross;
                    if (cross != null)
                    {
                        ((IObjectAddable)cross.Parent).Remove(cross);
                    }
                }
                else if (e.RightButton == MouseButtonState.Pressed)
                {
                    var cross = x as Cross;
                    if (cross != null)
                    {
                        _startingCanvasPos = new Point(Canvas.GetLeft(cross), Canvas.GetTop(cross));
                        _startingPoint = Mouse.GetPosition(this);
                        _selectedCross = cross;
                    }
                }
            }
        }

        public string Save()
        {
            return System.Windows.Markup.XamlWriter.Save(this);
        }


        public void AddOnMouse()
        {
            System.Windows.Point p = Mouse.GetPosition(this);
            var cross = new Cross() { ABC = 1 };
            Canvas.SetLeft(cross, p.X);
            Canvas.SetTop(cross, p.Y);
            Children.Add(cross);
        }

        IObjectAddable findAddable(FrameworkElement fe)
        {
            if (fe == null) return null;
            var addable = fe as IObjectAddable;
            if (addable != null) return addable;
            return findAddable(fe.Parent as FrameworkElement);
        }

        void trySelect(FrameworkElement fe)
        {
            deselectAll();
            _selectedAddable = findAddable(fe);
            if (_selectedAddable != null)
            {
                _selectedAddable.Selected = true;
            }
        }

        void deselectAll()
        {
            if (_selectedAddable != null)
            {
                _selectedAddable.Selected = false;
            }
            _selectedAddable = null;
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
                    Background = _selected ? new SolidColorBrush(Colors.LightPink) : new SolidColorBrush(Colors.White);
                    //PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
                }
            }
        }
    }
}
