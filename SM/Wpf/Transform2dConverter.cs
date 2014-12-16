using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class Transform2dString
    {
        public class Exception : System.Exception
        {
            public Exception(string message)
                : base(message)
            {

            }

            public Exception()
                : base("???????????")
            {

            }
        }


        private string X { get; set; }
        private string Y { get; set; }
        private string A { get; set; }
        private string S { get; set; }
        private bool Degree { get; set; }


        private bool setIfLetter(string v)
        {
            if (v.Length < 1)
            {
                throw new Exception();
            }
            if (v.Length < 2)
            {
                return false;
            }
            if (v[0] == 'r')
            {
                Degree = false;
                A = v.Substring(1);
            }
            else if (v[0] == 'R')
            {
                A = v.Substring(1);
            }
            else if (v[0] == 's')
            {
                S = v.Substring(1);
            }
            else if (v[0] == 'x')
            {
                X = v.Substring(1);
            }
            else if (v[0] == 'y')
            {
                Y = v.Substring(1);
            }
            else
            {
                return false;
            }
            return true;
        }

        private Transform2dString(string s)
        {
            X = "0";
            Y = "0";
            A = "0";
            S = "1";
            Degree = true;

            var values = s.Split(',');

            switch (values.Length)
            {
                case 1:
                    if (!setIfLetter(values[0]))
                    {
                        A = values[0];
                    }
                    break;
                case 2:
                    bool x = setIfLetter(values[0]);
                    bool y = setIfLetter(values[1]);
                    if (!(x ^ y))
                    {
                        if (!x)
                        {
                            X = values[0];
                            Y = values[1];
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }

                    break;
                case 3:
                    bool a = setIfLetter(values[0]);
                    bool b = setIfLetter(values[1]);
                    bool c = setIfLetter(values[2]);
                    if (!(a ^ b) && !(b ^ c))
                    {
                        if (!a)
                        {
                            X = values[0];
                            Y = values[1];
                            A = values[2];
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                    break;
                case 4:
                    a = setIfLetter(values[0]);
                    b = setIfLetter(values[1]);
                    c = setIfLetter(values[2]);
                    var d = setIfLetter(values[3]);
                    if (!(a ^ b) && !(b ^ c) && !(c ^ d))
                    {
                        if (!a)
                        {
                            X = values[0];
                            Y = values[1];
                            A = values[2];
                            S = values[3];
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                    break;

                //case 0:
                default:
                    break;
            }

        }

        private transform2d GetTransform2d()
        {
            rotoTranslation r;
            if (Degree)
            {
                r = rotoTranslation.FromDegree(float.Parse(X), float.Parse(Y), float.Parse(A));
            }
            else
            {
                r = new rotoTranslation(float.Parse(X), float.Parse(Y), float.Parse(A));
            }

            return new transform2d(r, float.Parse(S));
        }

        public static transform2d GetTransform2d(string s)
        {
            return new Transform2dString(s).GetTransform2d();
        }

        public static rotoTranslation GetRotoTranslation(string s)
        {
            return Transform2dString.GetTransform2d(s as string).RotoTranslation;
        }

        public static float2 GetFloat2(string s)
        {
            return Transform2dString.GetRotoTranslation(s as string).Translation;
        }
    }

    public abstract class BasicTransformationConverter : TypeConverter
    {
        Type _t;

        public BasicTransformationConverter(Type t)
        {
            _t = t;
        }

        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == _t;
        }
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
                throw new ArgumentNullException("destinationType");

            if (value.GetType().Equals(_t))
            {
                return ((rotoTranslation)value).ToString();
            }

            return "0,0,0";
        }

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    return Transform2dString.GetRotoTranslation(value as string);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Cannot convert '{0}' ({1}) because {2}", value, value.GetType(), ex.Message), ex);
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        protected abstract object Get(string s);
    }

    public class Float2Converter : BasicTransformationConverter
    {
        public Float2Converter()
            : base(typeof(float2))
        { }

        protected override object Get(string value)
        {
            return Transform2dString.GetFloat2(value as string);
        }
    }

    public class RotoTranslationConverter : BasicTransformationConverter
    {
        public RotoTranslationConverter()
            : base(typeof(rotoTranslation))
        { }

        protected override object Get(string value)
        {
            return Transform2dString.GetRotoTranslation(value as string);
        }
    }

    public class Transform2dConverter : BasicTransformationConverter
    {
        public Transform2dConverter()
            : base(typeof(transform2d))
        { }

        protected override object Get(string value)
        {
            return Transform2dString.GetTransform2d(value as string);
        }
    }
}
