﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface ISynchronizer 
    {
        //IdInfo Id { get; }
        void UpdateMaterial();
        void UpdateView();
        //object Object { get; }
    }
}
