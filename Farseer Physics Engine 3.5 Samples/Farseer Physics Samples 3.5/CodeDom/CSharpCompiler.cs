using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using System.CodeDom.Compiler;

namespace Flame.Dlr
{
    /// <summary>
    /// You can compile c# code to add functionalities to others scripting languages
    /// it compiles a class into an assembly and add this assembly to other scope
    /// </summary>
    /// 
    public class CSharpCompiler : CodeDomCompiler
    {

        public CSharpCompiler(/*IEnumerable<IExecutable> execs*/)
            //: base(execs)
        {
        }

        public override CodeDomProvider CodeDomProvider { get { return CodeDomProvider.CreateProvider("CSharp", new Dictionary<String, String> { { "CompilerVersion", "v4.0" } }); } }
    }

}
