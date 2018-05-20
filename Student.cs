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
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Home form2 = new Home();
            this.Hide();
            form2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StudentImport form4 = new StudentImport();
            this.Hide();
            form4.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StudentList form3 = new StudentList();
            this.Hide();
            form3.ShowDialog();
        }

        private void Student_Load(object sender, EventArgs e)
        {
            //hello
        }
    }
}
