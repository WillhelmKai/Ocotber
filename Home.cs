using course;
using Evaluate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3;


namespace WindowsFormsApp6
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void skinLabel1_Click(object sender, EventArgs e)
        {

        }

        private void logout_Click(object sender, EventArgs e)
        {
           Login form1 = new Login();
            this.Hide();
            form1.ShowDialog();
        }

        private void rubric_Click(object sender, EventArgs e)
        {
            Form3 form5 = new Form3();
            this.Hide();
            form5.ShowDialog();
        }

        private void student_Click(object sender, EventArgs e)
        {
            Student form3 = new Student();
            this.Hide();
            form3.ShowDialog();
        }

        private void evaluate_Click(object sender, EventArgs e)
        {

        }

        private void course_Click(object sender, EventArgs e)
        {

        }

        private void skinButton4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Student form3 = new Student();
            this.Hide();
            form3.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login form1 = new Login();
            this.Hide();
            form1.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CourseIndex form1 = new CourseIndex();
            this.Hide();
            form1.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Final_grade_page form1 = new Final_grade_page();
            this.Hide();
            form1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EVA eva = new EVA();
            this.Hide();
            eva.ShowDialog();
        }
    }
}
