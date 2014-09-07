using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Roslyn.Scripting;
using Roslyn.Scripting.CSharp;
using System.Reflection;

namespace WF_Rosylin_Test_Express
{
    public partial class Form1 : Form
    {
        Session _session;
        ScriptEngine _rosylnEngine;
        public Form1()
        {
            InitializeComponent();

            _session = Session.Create();
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                _session.AddReference(a);
            _rosylnEngine = new ScriptEngine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            _rosylnEngine.Execute("using System;", _session);
            _rosylnEngine.Execute("double sin(double x) {return Math.Sin(x);} " +
              "double tan(double x) {return Math.Tan(x);}", _session);
            //rosylnEngine.Execute("double cos(double x) {return Math.Cos(x);}", session);
            _rosylnEngine.Execute("double pi = 3.1415926535;", _session);

            var x = _rosylnEngine.Execute("sin(3.1415926535);", _session);
        }

        private void button2_Click(object sender, EventArgs e)
        {

           
            //rosylnEngine.Execute("public class ciao{int _o; public ciao(int o){ _o = o;} public int x(int o){ return _o + o;}}", _session);
            var x = _rosylnEngine.Execute(richTextBox1.Text, _session);

            if(x != null)
                MessageBox.Show(x.ToString());
        }
    }
}
