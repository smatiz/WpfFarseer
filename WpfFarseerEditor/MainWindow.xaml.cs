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
using System.Windows.Threading;

namespace WpfFarseerEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer _dispatcherTimer;
        public MainWindow()
        {
            InitializeComponent();


            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string schemafile = @"C:\ROOT\OneDrive\NET_DLL\Kaxaml\Schemas\XamlPresentation2006.xsd";
            Kaxaml.CodeCompletion.XmlCompletionDataProvider.LoadSchema(schemafile);

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += t_Tick;
           // xamlEditor.IsEnabled = false;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            _dispatcherTimer.Start();
        }

        void t_Tick(object sender, EventArgs e)
        {
            if (Kaxaml.CodeCompletion.XmlCompletionDataProvider.IsSchemaLoaded)
            {
                //xamlEditor.IsEnabled = true;
                _dispatcherTimer.Tick -= t_Tick;
            }
        }
    }
}
