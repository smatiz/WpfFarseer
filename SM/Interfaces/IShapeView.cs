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

    //public interface IShapeMaterial
    //{
    //    void Build(IEnumerable<IVector2> points, float density);
    //}

    //public class ShapeManager
    //{
    //    public IShapeView ShapeView { private get; set; }
    //    public IShapeMaterial ShapeMaterial { private get; set; }



    //    public void Build()
    //    {
    //        ShapeMaterial.Build(ShapeView.Points_X, ShapeView.Density_X);
    //    }
    //}
}
