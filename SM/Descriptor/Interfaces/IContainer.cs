﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface ILayer : IContainer
    {
        transform2d Transform { get; }
    }
}
