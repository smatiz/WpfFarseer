using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication1;

namespace WpfFarseerEditor.wpf
{
    /// <summary>
    /// Interaction logic for Screen.xaml
    /// </summary>
    public partial class Screen : UserControl
    {
        public Screen()
        {
            InitializeComponent();
            //MouseDown += Screen_MouseDown;
            PreviewMouseDown += Screen_MouseDown;
            MouseMove += Screen_MouseMove;
            MouseUp += Screen_MouseUp;
        }

        void updated()
        {
            if (ScreenChangedCommand != null)
            {
                ScreenChangedCommand();
            }
        }


        
        enum Status { Selecting, Adding, Moving, None }

        private Status CurrentStatus 
        { 
            get
            {
                if (Keyboard.Modifiers == ModifierKeys.Control) return Status.Moving;
                if (Keyboard.GetKeyStates(Key.Q) == KeyStates.Down) return Status.Moving;
                return Status.Selecting;
                return Status.Adding; 
            } 
        }
        private ISelectableObject _selected;
        private Point _startingPoint;
        private Point _startingCanvasPos;






        void Screen_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentStatus == Status.Moving)
            {
                if (_selected != null)
                {
                    var p = Mouse.GetPosition(this);
                    Canvas.SetLeft(_selected.Movable, _startingCanvasPos.X + p.X - _startingPoint.X);
                    Canvas.SetTop(_selected.Movable, _startingCanvasPos.Y + p.Y - _startingPoint.Y);
                }
            }
            else
            {
                _selected = null;
            }
        }

        void Screen_MouseUp(object sender, MouseButtonEventArgs e)
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


        void Screen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentStatus == Status.Selecting)
            {
                _selected = find(Mouse.DirectlyOver as FrameworkElement);
                if(_selected != null)
                {
                    _startingCanvasPos = new Point(Canvas.GetLeft(_selected.Movable), Canvas.GetTop(_selected.Movable));
                    _startingPoint = Mouse.GetPosition(this);
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
        


        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }
        public static readonly DependencyProperty CodeProperty =
            DependencyProperty.Register("Code", typeof(string), typeof(Screen), new PropertyMetadata(null, new PropertyChangedCallback((_do,_dp) => 
            {
                
            })));

        public Action ScreenChangedCommand
        {
            get { return (Action)GetValue(ScreenChangedCommandProperty); }
            set { SetValue(ScreenChangedCommandProperty, value); }
        }
        public static readonly DependencyProperty ScreenChangedCommandProperty =
            DependencyProperty.Register("ScreenChangedCommand", typeof(Action), typeof(Screen), new PropertyMetadata(null));

       
    }
}
