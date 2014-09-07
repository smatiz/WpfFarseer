using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Roslyn.Scripting;
using Roslyn.Scripting.CSharp;
using System.Reflection;
using System.Dynamic;

namespace WindowsFormsApplication1
{
    public  class VariableHost : DynamicObject
    {
        public int UUU = 342211;
    }

    public partial class Form1 : Form
    {
        public int III = 34;
        public dynamic DDD = new ExpandoObject();
        Session _session;
        ScriptEngine _rosylnEngine;
        dynamic _x;
        public Form1()
        {
            InitializeComponent();

            dynamic o = new VariableHost();
            _x = new ExpandoObject();
            _x.UUU = 111;
            _rosylnEngine = new ScriptEngine();
            _session = _rosylnEngine.CreateSession(o);
            //_rosylnEngine.
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    _session.AddReference(a);
                }
                catch { }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           /* _session.Execute("using System;");
            _session.Execute("double sin(double x) {return Math.Sin(x);} " +
              "double tan(double x) {return Math.Tan(x);}");
            //rosylnEngine.Execute("double cos(double x) {return Math.Cos(x);}", session);
            _session.Execute("double pi = 3.1415926535;");
            
            _session.Execute("sin(3.1415926535);");
            _session.Execute("var l = new System.Collections.Generic.List<int>() {1,2,3};");
            MessageBox.Show(_session.Execute("l").ToString());
            */
           // _session.AddReference(this.GetType().Assembly);
            //MessageBox.Show(_session.Execute("III").ToString());


            _x.ooo = 2342;
            //DDD.sss = 132;
           // _session.AddReference((typeof(VariableHost)).Assembly);
            MessageBox.Show(_session.Execute("UUU").ToString());


        }
    }
}
