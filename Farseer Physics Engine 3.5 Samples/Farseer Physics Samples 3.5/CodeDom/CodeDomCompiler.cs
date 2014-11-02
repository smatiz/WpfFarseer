using System;
using System.Collections.Generic;

using System.Text;
using System.CodeDom.Compiler;

namespace Flame.Dlr
{
    public abstract class CodeDomCompiler 
    {
        public class CompilationResult
        {
            public ResultAssembly ResultAssembly { get; set; }
        }

        List<AssemblyManager> _assembliesToLoad = new List<AssemblyManager>();


        public abstract CodeDomProvider CodeDomProvider { get; }

        public Result Execute(string script)
        {
            using (CodeDomProvider codeDomProvider = CodeDomProvider)
            {
                try
                {

                    var parameters = new System.CodeDom.Compiler.CompilerParameters()
                    {
                        //WarningLevel = 3,
                        //TreatWarningsAsErrors = false,
                        //CompilerOptions = "/optimize",
                        GenerateInMemory = false,
                        GenerateExecutable = false,
                        IncludeDebugInformation = false
                    };
                    foreach (AssemblyManager assembly in _assembliesToLoad)
                    {
                        try
                        {
                            parameters.ReferencedAssemblies.Add(assembly.Location);
                        }
                        catch
                        {
                        }
                    }

                    try
                    {
                        var res = codeDomProvider.CompileAssemblyFromSource(parameters, script);
                        if (res.Errors.HasErrors)
                            return new Result() { Data = res.Errors[0] };


                        codeDomProvider.Dispose();

                        List<CompilationResult> errors = new List<CompilationResult>();
                        ResultAssembly ra = null;
                        if (errors.Count != 0) return new Result() { Data = errors };
                        return new Result() { Data = res.CompiledAssembly };
                    }
                    catch (Exception e)
                    {
                        return new Result() { Data = e };
                    }
                }
                catch (System.Exception e)
                {
                    return new Result() { Data = e };
                }
            }
        }

        public void AddVariable(Variable variable) { }

        public ResultAssembly AddAssembly(AssemblyManager ainfo)
        {
            _assembliesToLoad.Add(ainfo);
            return new ResultAssembly() { Loaded = false, Exception = null, Language = GetType() };
        }

    }
}
