﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IJoint : IDescriptor
    {
        bool CollideConnected { get; }
    }
}