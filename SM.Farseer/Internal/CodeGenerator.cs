using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
   internal static class CodeGenerator
    {
        public static void AddCode(string line)
        {
            Trace.WriteLine(line, "Farseer Code Generator");
        }

        public static string g(this bool b)
        {
            return b ? "true" : "false";
        }

        public static string g(this Vector2 p)
        {
            return String.Format("new Vector2({0},{1})", p.X, p.Y);
        }

        public static string g(this Body b)
        {
            return String.Format("body_{0}", b.UserData as string);
        }

        public static string g(this BreakableBody b)
        {
            return String.Format("body_{0}", b.MainBody.UserData as string);
        }

        public static string g(this Joint j)
        {
            return String.Format("joint_{0}", j.UserData as string);
        }
    }
}
