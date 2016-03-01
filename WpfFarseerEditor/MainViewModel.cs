using SM;
using SM.Farseer;
using SM.WpfFarseer;
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


        public SM.BasicCommand Test
        {
            get
            {
                return new BasicCommand(() => _farseerXaml.Add(), () => true);
            }
        }


        public StepViewModel StepViewModel
        {
            get
            {
                return _farseerXaml.StepViewModel;
            }
        }

        FarseerXaml _farseerXaml;
        public MainViewModel()
        {
            Canvas = new Canvas();
            _farseerXaml = new FarseerXaml("aaa", 10f, 100, new float2(0, 10).ToFarseer(), Canvas);


            Code = "";


            Code = @"
        <Page
            xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006"" 
            xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
            xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
            xmlns:sm=""clr-namespace:SM.Xaml;assembly=SM.Xaml""
            xmlns:smf=""clr-namespace:SM.WpfFarseer;assembly=SM.WpfFarseer""
            xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">
          <Canvas>  
            <Button>ciao</Button>
                <smf:FarseerPlayerControl Gravity=""0,10"" x:Name=""_farseerPlayer""  Zoom=""20""  InDebug=""True"">
                <sm:Farseer  Id=""s9"">
                    <sm:Body BodyType=""Dynamic"">
                        <sm:Polygon  Points=""5,0,5,5,10,4"" Fill=""GreenYellow""  Stroke=""Black"" StrokeThickness=""1""/>
                    </sm:Body>
                    <sm:Body BodyType=""Static"">
                        <sm:Polygon  Points=""5,15,5,20,10,24"" Fill=""Red""  Stroke=""Black"" StrokeThickness=""1""/>
                    </sm:Body>
                </sm:Farseer>
             </smf:FarseerPlayerControl>
          </Canvas>
        </Page>";
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
                        //var x = new System.Windows.Markup.ParserContext();
                        //x.XmlnsDictionary.Add("sm", "sm:farseer");


                        //var page = (Page)System.Windows.Markup.XamlReader.Load(new MemoryStream(Encoding.UTF8.GetBytes(Code ?? ""))) ;
                        //Canvas = page.Content as Canvas;
                        //NotifyPropertyChanged(() => Canvas); 
                    }
                    catch(Exception e)
                    {
                        string msg = e.Message;
                    }
                };
            }
        }
    }
}
