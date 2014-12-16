using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SM.Xaml
{
    public class LayerTemplate : Layer
    {
        public LayerTemplate()
        {
            ItemsSource = new List<FrameworkElement>();
            Loaded += LayerTemplate_Loaded;
        }

        void LayerTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        void refresh()
        {
            if (ItemsSource == null) return;
            if (Children == null) return;
            if (DataTemplate == null) return;
            foreach (var item in ItemsSource)
            {
                var dp = DataTemplate.LoadContent() as FrameworkElement;
                dp.DataContext = item;
                Children.Add(dp);
            }
        }

        public System.Collections.IEnumerable ItemsSource
        {
            get { return (System.Collections.IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(System.Collections.IEnumerable), typeof(LayerTemplate), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSourcePropertyChanged)));
        private static void ItemsSourcePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((LayerTemplate)obj).refresh(); }

        public DataTemplate DataTemplate
        {
            get { return (DataTemplate)GetValue(DataTemplateProperty); }
            set { SetValue(DataTemplateProperty, value); }
        }
        public static readonly DependencyProperty DataTemplateProperty =
            DependencyProperty.Register("DataTemplate", typeof(DataTemplate), typeof(LayerTemplate), new PropertyMetadata(null, new PropertyChangedCallback(DataTemplatePropertyChanged)));
        private static void DataTemplatePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { ((LayerTemplate)obj).refresh(); }
    }
}
