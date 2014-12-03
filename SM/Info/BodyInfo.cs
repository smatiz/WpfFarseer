﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class BodyInfo : BasicInfo
    {
        public IEnumerable<IShape> Shapes { get; private set; }
        public IEnumerable<IFlag> Flags { get; private set; }

        public transform2d Transform { get; private set; }
        public BodyType BodyType { get; private set; }

        public BodyInfo(IBody body)
            : base(body)
        {
            Flags = body.Flags;
            Transform = body.Transform;
            BodyType = body.BodyType;

            Shapes = body.Shapes;
        }
    }
}
