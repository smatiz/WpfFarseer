using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JointCodeGenerator
{

    public class TheRazorModel
    {
        public string UserNamespace { get { return "ciao"; } }
    }

    public class JointRazorModel
    {
        public string Name { get { return "ciao"; } }
    }

    class Program
    {

        static string create_I_Joint()
        {




            string code = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public partial interface I@Model.Name Joint : IJoint
    {
        float MaxLength { get; }
        float MaxLengthFactor { get; }
        string TargetFlagIdA { get; }
        string TargetFlagIdB { get; }
    }
}
";

            return RazorEngine.Razor.Parse<JointRazorModel>(code, new JointRazorModel());
        }





        static void Main(string[] args)
        {
            Console.WriteLine(create_I_Joint());
            Console.Read();
        }
    }
}
