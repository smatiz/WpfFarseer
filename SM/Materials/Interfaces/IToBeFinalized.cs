﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IToBeFinalized
    {
        void Finalize(Materials material);
    }
}