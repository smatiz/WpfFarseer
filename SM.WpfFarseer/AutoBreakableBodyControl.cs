using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace WpfFarseer
{


    [ContentPropertyAttribute("Shape")]
    public class AutoBreakableBodyControl : BodyControl
    {
        public virtual FarseerPhysics.Common.Decomposition.TriangulationAlgorithm TriangulationAlgorithm
        {
            get { return (FarseerPhysics.Common.Decomposition.TriangulationAlgorithm)GetValue(TriangulationAlgorithmProperty); }
            set { SetValue(TriangulationAlgorithmProperty, value); }
        }
        public static readonly DependencyProperty TriangulationAlgorithmProperty =
            DependencyProperty.Register("TriangulationAlgorithm", typeof(FarseerPhysics.Common.Decomposition.TriangulationAlgorithm), 
            typeof(AutoBreakableBodyControl), new PropertyMetadata(FarseerPhysics.Common.Decomposition.TriangulationAlgorithm.Bayazit));

        public ShapeControl Shape
        {
            get { return (ShapeControl)GetValue(ShapeProperty); }
            set { SetValue(ShapeProperty, value); }
        }
        public static readonly DependencyProperty ShapeProperty =
            DependencyProperty.Register("Shape", typeof(ShapeControl), typeof(AutoBreakableBodyControl),
            new PropertyMetadata(null, new PropertyChangedCallback(ShapePropertyChanged)));
        private static void ShapePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            //var _obj = ((AutoBreakableBodyControl)obj);
            //_obj.Children.Clear();
            //_obj.Children.Add(_obj.Shape);
        }

        public BreakableBodyControl BreakableBodyControl
        {
            get
            {
                var polyF = Shape.Points.ToFarseerVertices();
                var vss = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition( polyF, TriangulationAlgorithm);
                var bbc = new BreakableBodyControl();

                foreach(var p in vss)
                {
                    var bbcp = new BreakableBodyPartControl();
                    bbcp.Shape.Points = p.ToWpf();
                    bbc.Parts.Add(bbcp);
                }
                return bbc;
            }
        }
    }
}
