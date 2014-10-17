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
        void Build(IEnumerable<IVector2> points, float density);
    }

    public class ShapeManager
    {
        public IShapeView ShapeView { get; private set; }
        public IShapeMaterial ShapeMaterial { get; private set; }

        public ShapeManager(IShapeView shapeView, IShapeMaterial shapeMaterial)
        {
            ShapeView = shapeView;
            ShapeMaterial = shapeMaterial;
            Build();
        }

        private void Build()
        {
            ShapeMaterial.Build(ShapeView.Points_X, ShapeView.Density_X);
        }
        public static void Build(IShapeView shapeView, IShapeMaterial shapeMaterial)
        {
            shapeMaterial.Build(shapeView.Points_X, shapeView.Density_X);
        }
    }
}
