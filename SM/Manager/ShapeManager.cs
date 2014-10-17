using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
     public interface IShapeView
    {
        IEnumerable<IVector2> Points_X { get; }
        float Density_X { get; }
    }

    public interface IShapeMaterial
    {
        IEnumerable<IVector2> Points_X { set; }
        float Density_X { set; }
    }

    public class ShapeManager
    {
        public IShapeView ShapeView { get; private set; }
        public IShapeMaterial ShapeMaterial { get; private set; }

        public ShapeManager(IShapeView shapeView, IShapeMaterial shapeMaterial)
        {
            ShapeView = shapeView;
            ShapeMaterial = shapeMaterial;
        }

        public void Build()
        {
            ShapeMaterial.Points_X = ShapeView.Points_X;
            ShapeMaterial.Density_X = ShapeView.Density_X;
        }
    
    }
}
