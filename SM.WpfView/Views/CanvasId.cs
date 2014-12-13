using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SM.WpfView
{
    public class CanvasId : Canvas
    {
#if DEBUG
        static List<string> IDS = new List<string>();
#endif
        string _id;

        public string Id
        {
            get { return _id; }
            set
            {
#if DEBUG
                //if(IDS.Contains(value))
                //{
                //    throw new Exception();
                //}
                //IDS.Add(value);
#endif
                _id = value;
            }
        }
    }
}
