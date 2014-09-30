using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApplication1;

namespace WpfFarseerEditor.wpf
{
    class _ScreenCanvas : Canvas
    {

        enum Status { Selecting, Adding, Moving, None }

        private Status CurrentStatus 
        { 
            get
            { 
                if(Keyboard.GetKeyStates(Key.Q) == KeyStates.Down) return Status.Moving;
                return Status.Adding; 
            } 
        }
        private ISelectableObject _selected;
        private Point _startingCanvasPos;
   

      

        public _ScreenCanvas()
        {
            MouseDown += SaverCanvas_MouseDown;
            MouseMove += SaverCanvas_MouseMove;
            MouseUp += SaverCanvas_MouseUp;
        }


        void SaverCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentStatus == Status.Moving)
            {
                if (_selected != null)
                {
                    var p = Mouse.GetPosition(this);
                    //Canvas.SetLeft(_selected, _startingCanvasPos.X + p.X - _startingPoint.X);
                   // Canvas.SetTop(_selected, _startingCanvasPos.Y + p.Y - _startingPoint.Y);
                }
            }
            else
            {
                _selected = null;
            }
        }

        void SaverCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _selected = null;
        }


        ISelectableObject find(FrameworkElement fe)
        {
            if (fe == null) return null;
            var sel = fe as ISelectableObject;
            if (sel != null) return sel;
            return find(fe.Parent as FrameworkElement);
        }


        void SaverCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentStatus == Status.Selecting)
            {
                _selected = find(Mouse.DirectlyOver as FrameworkElement);
                if(_selected != null)
                {
                  //  _selected.Hover
                }
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
               /* var x = Mouse.DirectlyOver;
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
                }*/
            }
        }
    }
}
