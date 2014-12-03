using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class JointRazorModel
    {
        public string Name { get; private set; }
        public IEnumerable<Field> Fields { get; private set; }

        public JointRazorModel(Type type)
        {
            string[] exclusions = new string[] { "WorldAnchorA", "WorldAnchorB", "LocalAnchorA", "LocalAnchorB" };


            Name = type.Name;
            Fields = type.GetProperties().Where(p => p.DeclaringType == type).Select(x => new Field(x)).Where(f => f.HasSet && !exclusions.Contains(f.VarName));
        }


        public static string create_I_Joint()
        {


            string code = @"
// This File was automaticly created (do not change the file manually)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text
using System.Threading.Tasks;

namespace SM
{
    public partial interface I@(Model.Name) : IJoint
    {
        @foreach(var x in Model.Fields){
             @:@x.Type @x.VarName { get; }
        }
    }
}
";


            code = @"
// This File was automaticly created (do not change the file manually)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using SM;
using System.Windows.Media;

namespace SM.Xaml
{
    public partial class @(Model.Name)Control : BasicJointControl, I@(Model.Name)
    {
 @foreach(var x in Model.Fields){
    @:    public @x.Type @x.VarName
    @:    {
    @:        get { return (@x.Type)GetValue(@(x.VarName)Property); }
    @:        set { SetValue(@(x.VarName)Property, value); }
    @:    }
    @:    public static readonly DependencyProperty @(x.VarName)Property =
    @:        DependencyProperty.Register(""@x.VarName"", typeof(@x.Type), typeof(@(Model.Name)Control), new PropertyMetadata(@x.DefaultValue));
}
    }
}

";

code = @"
// This File was automaticly created (do not change the file manually)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text
using System.Threading.Tasks;

namespace SM
{
    public partial interface I@(Model.Name) : IJoint
    {
        @foreach(var x in Model.Fields){
             @:@x.Type @x.VarName { get; }
        }
    }
}
";

            return RazorEngine.Razor.Parse<JointRazorModel>(code, new JointRazorModel(typeof(FarseerPhysics.Dynamics.Joints.WeldJoint)));
        }
    }
}
