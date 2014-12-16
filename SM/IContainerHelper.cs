using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class IContainerHelper
    {
        static IEntity GetTransformedEntity(IEntity entity, transform2d transform)
        {
            var transformable = entity as ITransformable;
            if (transformable != null)
            {
                transformable.Transform = transformable.Transform * transform;
            }
            return entity;
        }

        public static IEnumerable<IEntity> GetAllIEntities(IContainer container, transform2d transform)
        {
            var transformer = container as ITransformer;

            var currentTransform2dSaved = transform;
            if (transformer != null)
            {
                transform = transformer.Transform * transform;
            }

            var childrenLinq = container.Entities.Cast<object>();
            var entities = childrenLinq.Where(x => x is IEntity).Select(x => GetTransformedEntity((IEntity)x, transform));
            foreach (var c in childrenLinq.Where(x => x is IContainer).Select(x => (IContainer)x))
            {
                entities = entities.Concat(GetAllIEntities(c, transform));
            }
            return entities;
        }
    }
}
