using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evaluate
{
    public partial class ProportionOfSec : Form
    {
        public ProportionOfSec()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Evaluate form2 = new Evaluate();
            this.Hide();
            form2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
