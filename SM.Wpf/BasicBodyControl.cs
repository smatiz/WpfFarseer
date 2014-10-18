using SM;
using SM.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SM.Wpf
{
    public abstract class BasicBodyControl : BasicControl
    {
        private RotateTransform _rotation; 
        private TranslateTransform _traslation;

        public BasicBodyControl()
        {
            Flags = new ObservableCollection<FlagControl>();
            Flags.CollectionChanged += Flags_CollectionChanged;

            _rotation = new RotateTransform();
            _traslation = new TranslateTransform();

            _canvas.Loaded += (s, e) =>
            {
                var gt = new TransformGroup();
                gt.Children.Add(_rotation);
                gt.Children.Add(_traslation);
                _canvas.RenderTransform = gt;
                OnLoaded();
            };
        }

        protected virtual void OnLoaded()
        {
            foreach (var poly in Polygons)
            {
                _canvas.Children.Add(poly);
                ((Polygon)poly).Fill = Brush;
            }
        }

        protected abstract Brush Brush { get; }
        protected abstract IEnumerable<Polygon> Polygons { get; }

        public static float GetDensity(DependencyObject obj)
        {
            return (float)obj.GetValue(DensityProperty);
        }
        public static void SetDensity(DependencyObject obj, float value)
        {
            obj.SetValue(DensityProperty, value);
        }
        public static readonly DependencyProperty DensityProperty =
            DependencyProperty.RegisterAttached("Density", typeof(float), typeof(BodyControl), new PropertyMetadata(Const.Density));

        public ObservableCollection<FlagControl> Flags
        {
            get { return (ObservableCollection<FlagControl>)GetValue(FlagsProperty); }
            set { SetValue(FlagsProperty, value); }
        }
        public static readonly DependencyProperty FlagsProperty =
            DependencyProperty.Register("Flags", typeof(ObservableCollection<FlagControl>), typeof(BodyControl), new PropertyMetadata(null));

        void Flags_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {
                    _canvas.Children.Add((FlagControl)x);
                }
            }
        }
        
        public IEnumerable<IPointControl> FlagsPoints
        {
            get { return from x in Flags select x; }
        }

        public rotoTranslation RotoTranslation
        {
            set
            {
                _traslation.X = value.Translation.X;
                _traslation.Y = value.Translation.Y;
                _rotation.Angle = value.Angle;
            }
        }
    }
}
