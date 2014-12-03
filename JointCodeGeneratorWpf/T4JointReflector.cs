
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public class JointCreator
    {
        public Field[] ParamFields { get; private set; }
        public string MethodName { get; private set; }

        public JointCreator(MethodInfo methodInfo)
        {
             MethodName = methodInfo.Name;
             //ParamFields = methodInfo.GetParameters().Select(x => new Field(x)).ToArray();
        }
    }

    public class Field
    {
        public string Type { get; private set; }
        public string VarName { get; private set; }
        public string PrivateVarName { get { return "_" + VarName[0].ToString().ToLower() + VarName.Substring(1); } }
        public string ToSmFuncCall { get; private set; }
        public string FromSmFuncCall { get; private set; }

        public bool HasGet { get; private set; }
        public bool HasSet { get; private set; }
        public string DefaultValue { get; private set; }
        public Field(string field)
        {
            string[] s = field.Split(' ');
            Type = s[0];
            VarName = s[1];
        }

        private void needConvertion()
        {
            FromSmFuncCall = ".FromSM()";
            ToSmFuncCall = ".ToSM()";
        }

        public Field(PropertyInfo propertyInfo)
        {
            Type = propertyInfo.PropertyType.Name;
            VarName = propertyInfo.Name;
            HasGet = propertyInfo.GetMethod != null && propertyInfo.GetMethod.IsPublic;
            HasSet = propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsPublic;
            ToSmFuncCall = "";
            FromSmFuncCall = "";

            switch (Type)
            {
                case "float":
                    DefaultValue = "0f";
                    break;
                case "bool":
                    DefaultValue = "false";
                    break;
                case "int":
                case "double":
                    DefaultValue = "0";
                    break;
                case "Vector2":
                    Type = "float2";
                    DefaultValue = "float2.Zero";
                    ToSmFuncCall = ".ToSM()";
                    break;
                case "Body":
                case "Joint":
                    Type = "string";
                    DefaultValue = "null";
                    ToSmFuncCall = ".ToSM()";
                    break;
                case "JointType":
                    DefaultValue = "JointType";
                    ToSmFuncCall = ".ToSM()";
                    break;
                case "LimitState":
                    DefaultValue = "LimitState";
                    ToSmFuncCall = ".ToSM()";
                    break;
                default:
                    DefaultValue = "null";
                    break;
            }
        }


    }
    public class Joint
    {
        public Field[] Fields { get; private set; }
        public JointCreator JointCreator { get; private set; }
        public string JointName { get; private set; }
        public Joint(string className, List<string> fields)
        {
            JointName = className;
            Fields = fields.Select(x => new Field(x)).ToArray();

        }
        public Joint(Type joint)
        {
            JointName = joint.Name;
            Fields = joint.GetProperties().Select(x => new Field(x)).ToArray();

        }
    }
    public class JointReflector
    {
        Type _jointFactoryType;
        Type _baseJointType;


        private JointReflector(Type jointFactoryType, Type baseJointType)
        {
            _baseJointType = baseJointType;
            _jointFactoryType = jointFactoryType;
        }

        public static IEnumerable<Joint> GetJoints(Type jointFactoryType, Type baseJointType)
        {
            return new JointReflector(jointFactoryType, baseJointType).GetJoints();
        }

        private IEnumerable<Joint> GetJoints()
        {
            return _baseJointType.Assembly.GetTypes()
                .Where(x => x.IsSubclassOf(_baseJointType))
                .Select(j =>  new Joint(j));
        }

        private static MethodInfo getBestMethodInfo(IEnumerable<MethodInfo> methodInfo)
        {
            var methodInfoParam = methodInfo.Select(x => new { Method = x, ParamsCount = x.GetParameters().Count() });
            var maxNoParam = methodInfoParam.Select(x => x.ParamsCount).Max();
            var methodInfoParamMax = methodInfoParam.Where(x => x.ParamsCount == maxNoParam);
            return methodInfoParamMax.Select(x => x.Method).First();
        }

        private IEnumerable<MethodInfo> getMethods()
        {
            return _jointFactoryType.GetMethods()
                .Where(x => x.IsStatic && x.ReturnType.IsSubclassOf(_baseJointType))
                .GroupBy(x => x.ReturnType)
                .Select(x=> getBestMethodInfo(x));

           // jointfactoryMethodsGroupedByReturnType.Select(x => x.)

        }

        private IEnumerable<KeyValuePair<Joint, JointCreator>> methods(Joint joint)
        {

            var methods = getMethods();
            methods.Select(x => new JointCreator(x) );


            return null;

            // jointfactoryMethodsGroupedByReturnType.Select(x => x.)

        }
    }


}