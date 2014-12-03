using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
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
        
        public Field(PropertyInfo propertyInfo)
        {
            Type = propertyInfo.PropertyType.Name;
            VarName = propertyInfo.Name;
            HasGet = propertyInfo.GetMethod != null && propertyInfo.GetMethod.IsPublic;
            HasSet = propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsPublic;

            switch (Type)
            {
                case "float":
                case "Single":
                    DefaultValue = "0f";
                    break;
                case "bool":
                    DefaultValue = "false";
                    break;
                case "int":
                case "double":
                    DefaultValue = "0";
                    break;
                default:
                    DefaultValue = "null";
                    break;
            }
        }
    }

    public static class PropertyReflector
    {
        public static IEnumerable<Field> GetJointFields(this Type type)
        {
            return type.GetProperties().Select(x => new Field(x)).Where(x => x.HasGet && x.HasSet);
        }
    }

}