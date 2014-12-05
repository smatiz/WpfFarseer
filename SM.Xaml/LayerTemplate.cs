using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SM.Xaml
{
    public class LayerTemplate : BasicContainer, ILayer
    {
        
        public LayerTemplate()
        {
            ItemsSource = new List<FrameworkElement>();
        }

        void refresh()
        {
            if (Children == null) return;
            if (DataTemplate == null) return;
            Children.Clear();
            foreach (var item in ItemsSource)
            {
                var dp = DataTemplate.LoadContent() as UserControl;
                dp.DataContext = item;
                Children.Add((IDescriptor)dp);
            }
        }

        public transform2d Transform
        {
            get { return (transform2d)GetValue(TransformProperty); }
            set { SetValue(TransformProperty, value); }
        }
        public static readonly DependencyProperty TransformProperty =
            DependencyProperty.Register("Transform", typeof(transform2d), typeof(LayerTemplate), new PropertyMetadata(transform2d.Null));

        public IEnumerable<FrameworkElement> ItemsSource
        {
            get { return (IEnumerable<FrameworkElement>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<FrameworkElement>), typeof(LayerTemplate), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSourcePropertyChanged)));
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
