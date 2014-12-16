using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS
{
    public static class Helper
    {
        public static object ReadXaml(this string xaml)
        {
            return System.Windows.Markup.XamlReader.Load(new System.IO.MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(xaml)));
        }

        public static string WriteXaml(this object xaml)
        {
            var ms = new System.IO.MemoryStream();
            System.Windows.Markup.XamlWriter.Save(xaml, ms);
            return System.Text.ASCIIEncoding.ASCII.GetString(ms.GetBuffer());
        }
    }
}
