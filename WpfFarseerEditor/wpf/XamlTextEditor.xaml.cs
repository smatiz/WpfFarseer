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

namespace WpfFarseerEditor.wpf
{
    /// <summary>
    /// Interaction logic for XamlTextEditor.xaml
    /// </summary>
    public partial class XamlTextEditor : UserControl
    {
        public XamlTextEditor()
        {
            InitializeComponent();
        }

        private void xamlEditor_TextChanged(object sender, Kaxaml.Controls.TextChangedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
            if (TextChangedCommand != null)
            {
                TextChangedCommand();
            }
        }

        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }
        public static readonly DependencyProperty CodeProperty =
            DependencyProperty.Register("Code", typeof(string), typeof(XamlTextEditor), new PropertyMetadata(null));

        public Action TextChangedCommand
        {
            get { return (Action)GetValue(TextChangedCommandProperty); }
            set { SetValue(TextChangedCommandProperty, value); }
        }
        public static readonly DependencyProperty TextChangedCommandProperty =
            DependencyProperty.Register("TextChangedCommand", typeof(Action), typeof(XamlTextEditor), new PropertyMetadata(null));
    }
}
