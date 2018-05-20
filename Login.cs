using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Login : Form
    {
        private string password;

        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = Application.StartupPath + @"\MP10.ssk";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            password = textBox1.Text;
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (password == "admin") {
                Home form2 = new Home();
                this.Hide();
                form2.ShowDialog();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (password == "admin")
            {
                Home form2 = new Home();
                this.Hide();
                form2.ShowDialog();
            }
            else {
                MessageBox.Show("Invalid Password");
            }
        }
    }
}
