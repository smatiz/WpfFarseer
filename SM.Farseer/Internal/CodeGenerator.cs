using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
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
   public static class CodeGenerator
    {
       static int i = 1;
       public static string N(string s = "")
       {
           return s + (i++).ToString();
       }

       public static string Header
       {
           private get;
           set;
       }

        public static void AddCode(string line)
        {
            Trace.WriteLine(line, Header);
        }

        public static void AddCode(string line, params object[] args )
        {
            AddCode(String.Format(line, args));
        }


        public static void AddCode(this Vertices vs, string shapeName)
        {
            CodeGenerator.AddCode("var {0} = new FarseerPhysics.Common.Vertices();", shapeName);
            foreach (var v in vs)
            {
                CodeGenerator.AddCode("{0}.Add({1});", shapeName, v.g());
            }
        }

        public static void AddCode(this PolygonShape p, string polyName, string verticesName)
        {
            p.Vertices.AddCode(verticesName);
            CodeGenerator.AddCode("var {0} = new FarseerPhysics.Collision.Shapes.PolygonShape({1}, {2});",polyName,verticesName,  p.Density);
        }

        public static string g(this bool b)
        {
            return b ? "true" : "false";
        }

        public static string g(this Vector2 p)
        {
            return String.Format("new Vector2({0},{1})", p.X, p.Y);
        }

        public static string n(this Body b)
        {
            return String.Format("body_{0}", b.UserData as string);
        }

        public static string n(this BreakableBody b)
        {
            return String.Format("body_{0}", b.MainBody.UserData as string);
        }

        public static string n(this Joint j)
        {
            return String.Format("joint_{0}", j.UserData as string);
        }
    }
}
