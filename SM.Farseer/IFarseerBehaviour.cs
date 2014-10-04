﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public interface IFarseerBehaviour
    {
        // loop di Farseer
        IEnumerator<BasicCoroutine> Loop(float dt);
    }
}