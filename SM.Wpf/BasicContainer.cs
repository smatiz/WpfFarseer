using SM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using SM.Wpf;

namespace SM.Xaml
{
    public abstract class BasicContainer : Panel, IContainer
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

        static IEnumerable<IEntity> GetAllIEntities(BasicContainer container, transform2d transform)
        {
            var transformer = container as ITransformer;

            var currentTransform2dSaved = transform;
            if (transformer != null)
            {
                transform = transformer.Transform * transform;
            }

            var childrenLinq = container.Children.Cast<object>();
            var entities = childrenLinq.Where(x => x is IEntity).Select(x => GetTransformedEntity((IEntity)x, transform));
            foreach (var c in childrenLinq.Where(x => x is BasicContainer).Select(x => (BasicContainer)x))
            {
                entities = entities.Concat(GetAllIEntities(c, transform));
            }
            return entities;
        }

        public IEnumerable<IEntity> Entities
        {
            get
            {
                return GetAllIEntities(this, transform2d.Null);
            }
        }

        public string Id { get; set; }
    }
}
