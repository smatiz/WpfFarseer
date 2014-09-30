using Kaxaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfFarseerEditor.wpf
{




    public class _XamlTextEditor : KaxamlTextEditor
    {
        public _XamlTextEditor()
        {
            TextChanged += XamlTextEditor_TextChanged;
        }

        void XamlTextEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(TextChangedCommand != null)
            {
                TextChangedCommand();
            }
        }





        public Action TextChangedCommand
        {
            get { return (Action)GetValue(TextChangedCommandProperty); }
            set { SetValue(TextChangedCommandProperty, value); }
        }
        public static readonly DependencyProperty TextChangedCommandProperty =
            DependencyProperty.Register("TextChangedCommand", typeof(Action), typeof(XamlTextEditor), new PropertyMetadata(null));

        
        
    }
}
