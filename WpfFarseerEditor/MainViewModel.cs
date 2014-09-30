using SM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfFarseerEditor.wpf 
{
    public class MainViewModel : NotifyObjectViewer
    {
        public string Code
        {
            get;
            set;
        }

        public MainViewModel()
        {
            Code = "";
        }

        public Canvas Canvas
        {
            get;
            set;
        }

        public Action OnTextChanged
        {
            get
            {
                return () => 
                { 
                    try
                    {
                       Canvas = System.Windows.Markup.XamlReader.Load(new MemoryStream(Encoding.UTF8.GetBytes(Code ?? ""))) as Canvas;
                        NotifyPropertyChanged(() => Canvas); 
                    }
                    catch { }
                };
            }
        }
    }
}
