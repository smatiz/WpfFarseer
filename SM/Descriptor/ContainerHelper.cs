using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class ContainerHelper
    {
        //static IEntity GetTransformedEntity(IEntity entity, transform2d transform)
        //{
        //    var transformable = entity as ITransformable;
        //    if (transformable != null)
        //    {
        //        transformable.Transform = transformable.Transform * transform;
        //    }
        //    return entity;
        //}
        //static IEntity ApplyDescription(IEntity entity, string id)
        //{
        //    //entity.Id = String.Format()
        //    return entity;
        //}

        ////static List<string> Ids = new List<string>();


        //// Questo mi deve restituire una lista di entity 
        //// - correttamente traslate
        //// - con Id univoci
        //// - con lo stesso contesto ??? potrei dire di no perche non riguarda SM ma SM.WpfView ?=
        //private static IEnumerable<IEntity> GetAllIEntities(IContainer container, transform2d currentTransform, string currentId)
        //{
        //    var transformer = container as ITransformer;
        //    if (transformer != null)
        //    {
        //        currentTransform = transformer.Transform * currentTransform;
        //    }
        //    var newId = container as IIdentifiable;
        //    if (newId != null)
        //    {
        //        currentId = String.Format("{0}.{1}", newId.Id, currentId);
        //    }

        //    var childrenLinq = container.Entities.Cast<object>();
        //    var entities = childrenLinq.Where(x => x is IEntity).Select(x => GetTransformedEntity((IEntity)x, currentTransform));
        //    foreach (var c in childrenLinq.Where(x => x is IContainer).Select(x => (IContainer)x))
        //    {
        //        entities = entities.Concat(GetAllIEntities(c, currentTransform, currentId));
        //    }
        //    return entities;
        //}

        //public static IEnumerable<IEntity> GetAllIEntities(IContainer container)
        //{
        //    var r = GetAllIEntities(container, transform2d.Null, "");

        //    // potrei fare questo controllo  nella funzione sopra
        //    if(r.Select(x => x.Id).Distinct().Count() == r.Count())
        //    {
        //        throw new Exception("reapeted id");
        //    }
        //    return r;
        //}
    }
}
