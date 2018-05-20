using System;
using Evaluate;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6;

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
            EVA eva = new EVA();
            this.Hide();
            eva.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
