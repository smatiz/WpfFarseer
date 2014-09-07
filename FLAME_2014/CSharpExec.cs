using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roslyn.Scripting;
using Roslyn.Scripting.CSharp;
using System.Dynamic;
using System.Reflection;

namespace Flame.Dlr
{
    public class CSharpExec : IExecutable
    {

        public class HostObject 
        {
            public dynamic Share;// { get; private set; }
            public HostObject()
            {
                Share = new ExpandoObject();
            }
        }

         Session _session;
        ScriptEngine _rosylnEngine;
        //dynamic _hostObject;
        //ExpandoObject _hostObject;

        HostObject _ho;
        public CSharpExec()
        {
            ClearScope();
        }

        public int OOO = 4;

        public Languages Language { get { return Languages.CSharpScript; } }


       // CSharpCompiler _csharpCompiler;
        public void ClearScope()
        {

            _rosylnEngine = new ScriptEngine();
            _ho = new HostObject();
            _session = _rosylnEngine.CreateSession(_ho);
            _session.AddReference(_ho.GetType().Assembly);
            //_csharpCompiler = new CSharpCompiler();
           

            /*dynamic x = new ExpandoObject();
            x.aaa = "ciao";
            _session.AddReference(x.GetType().Assembly);
            _session.AddReference(this.GetType().Assembly);*/
        }

        Result check(Result cr)
        {
            try
            {
                if (cr.Data == null)
                    return Result.Null;
            }
            catch
            {
                return Result.Bad;
            }
            return cr;
        }

        public Result Execute(string text)
        {
           /* string classstring = "public class csharpExecHost { ";

            foreach (var v in _vars)
            {
                classstring += "public int " + v.Name + ";";
            }

            classstring += "}";


            var r = _csharpCompiler.Execute(classstring);
            var ass = ((System.CodeDom.Compiler.CompilerResults)r.Data).CompiledAssembly;
            Assembly.LoadFrom(ass.Location);

            dynamic d = Activator.CreateInstance(ass.GetType("csharpExecHost"));
            d.uuu = 444;
            _session = _rosylnEngine.CreateSession(d);
            d.uuu = 333;
            _session.AddReference(ass);*/


            return check(new Result() { Data = _session.Execute(text) });
        }

        public ResultAssembly AddAssembly(AssemblyWrapper res)
        {
            try
            {
                _session.AddReference(res.Assembly);
            }
            catch(Exception e)
            {
                return new ResultAssembly() { Exception = e, Language = Language, Loaded = false };
            }
            return new ResultAssembly() { Exception = null, Language = Language, Loaded = true };
        }


        List<Variable> _vars = new List<Variable>();
        public void AddVariable(Variable variable)
        {
            _vars.Add(variable);
           // ((IDictionary<String, Object>)_hostObject).Add(variable.Name.ToLower(), variable.Data);
            //_hostObject.dsf = "ciao";
            //_session.
        }
    }
}
