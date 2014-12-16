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


        public string FullId
        {
            get
            {
                List<CanvasId> names = new List<CanvasId>();
                var n = this;
                while (n != null)
                {
                    names.Add(n);
                    n = n.Parent as CanvasId;
                }
                names.Reverse();
                return names.Select(c => c.Id).Aggregate((s1, s2) => String.Format("{0}.{1}"));

            }
        }


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
