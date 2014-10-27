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
    public interface IFlaggable : IIdentifiable
    {
        ObservableCollection<FlagControl> Flags { get; }
    }

    public abstract class BasicBodyControl : BasicControl, IFlaggable
    {
        private RotateTransform _rotation; 
        private TranslateTransform _traslation;

        public BasicBodyControl()
        {
            Flags = new ObservableCollection<FlagControl>();
            Flags.CollectionChanged += Flags_CollectionChanged;

            _rotation = new RotateTransform();
            _traslation = new TranslateTransform();

            bool loaded = false;
            _canvas.Initialized += (s, e) =>
            {
                if (loaded) return;
                loaded = true;
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

        //public static float GetDensity(DependencyObject obj)
        //{
        //    return (float)obj.GetValue(DensityProperty);
        //}
        //public static void SetDensity(DependencyObject obj, float value)
        //{
        //    obj.SetValue(DensityProperty, value);
        //}
        //public static readonly DependencyProperty DensityProperty =
        //    DependencyProperty.RegisterAttached("Density", typeof(float), typeof(BodyControl), new PropertyMetadata(Const.Density));

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
                    ((FlagControl)x).AddToUIElementCollection(_canvas.Children);
                    //_canvas.Children.Add((FlagControl)x);
                }
            }
        }



        public float X
        {
            get { return (float)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(float), typeof(BasicBodyControl), new PropertyMetadata(0f));



        public float Y
        {
            get { return (float)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(float), typeof(BasicBodyControl), new PropertyMetadata(0f));

        public float2 Position { get { return new float2(X, Y); } }
        


        //public IEnumerable<IPointControl> FlagsPoints
        //{
        //    get { return from x in Flags select x; }
        //}

        public rotoTranslation RotoTranslation
        {
            get 
            {
                return rotoTranslation.FromDegree(new float2((float)_traslation.X, (float)_traslation.Y), (float)_rotation.Angle);
            }
            set
            {
                _traslation.X = value.Translation.X;
                _traslation.Y = value.Translation.Y;
                _rotation.Angle = value.DegreeAngle;
            }
        }
    }
}
