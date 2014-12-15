﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IBody : IEntity, ITransformable
    {
        List<IFlag> Flags { get; }

        BodyType BodyType { get; }
        List<IShape> Shapes { get; }
    }
}
