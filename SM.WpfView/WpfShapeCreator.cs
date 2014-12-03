using SM.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.WpfView
{
    public class WpfShapeCreator : IShapeViewCreator
    {
        private BasicShapeView To(IShape shape, IContext context, float scale)
        {
            if (shape is ICircle)
            {
                return new CircleShapeView(context, (ICircle)shape, scale);
            }
            else if (shape is IEllipse)
            {
                return new EllipseShapeView(context, (IEllipse)shape, scale);
            }
            else if (shape is IPolygon)
            {
                return new PolygonShapeView(context, (IPolygon)shape, scale);
            }
            else if (shape is ISkinnedShape)
            {
                return new SkinnedShapeView(context, (ISkinnedShape)shape, scale);
            }
            throw new Exception("private IShape To(Shape shape)");
        }

        public IEnumerable<BasicShapeView> Create(IEnumerable<IShape> shape, IContext context, float scale)
        {
            return shape.Select<IShape, BasicShapeView>(s => To(s, context, scale)); ;
        }
    }
}
