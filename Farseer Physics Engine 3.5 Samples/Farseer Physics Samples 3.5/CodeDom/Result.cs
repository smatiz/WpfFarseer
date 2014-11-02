using System;
using System.Collections.Generic;
using System.Text;

namespace Flame.Dlr
{
    public class Null
    {
        public override string ToString()
        {
            /// make this customizable ?=
            return "";
            //return "Null";
        }
    }
    public class Result
    {
        static int CurrentId = -1;

        public Result()
        {
            CurrentId++;
            Id = CurrentId;
        }

        public int Id { get; private set; }
#if NET35
        public object Data { get; set; }
#else
        public dynamic Data { get; set; }
#endif
        public static Result Null { get { return new Result() { Data = new Null() }; } }
    }
}
