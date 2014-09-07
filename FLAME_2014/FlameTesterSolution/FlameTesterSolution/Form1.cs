using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flame.Dlr;

namespace FlameTesterSolution
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 1) copy "Data" directory at the same level of 


        private void button1_Click(object sender, EventArgs e)
        {
            var m = new ManagerCreator();
            m.AddVariable(new Variable[1] { new Variable() { Name = "form", Data = this } });
            var f = new Flame.Controls.ScripterControlForm(m);
            f.Show();
        }
    }
}
