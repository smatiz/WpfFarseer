using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SM.Wpf
{
    public class BasicShapeControl : DependencyObject, IContextual
    {
        public BasicShapeControl()
        {
            Context = new DefaultContext();
        }


        public float Density
        {
            get { return (float)GetValue(DensityProperty); }
            set { SetValue(DensityProperty, value); }
        }
        public static readonly DependencyProperty DensityProperty =
            DependencyProperty.Register("Density", typeof(float), typeof(BasicShapeControl), new PropertyMetadata(1f));

        protected IContext _context;
        public virtual IContext Context
        {
            set
            {
                _context = value;
            }
        }

    }
}