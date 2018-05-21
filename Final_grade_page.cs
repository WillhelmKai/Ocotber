using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using WindowsFormsApp6;

namespace Evaluate
{
    public partial class Final_grade_page : Form
    {
        public Final_grade_page()
        {
            InitializeComponent();
        }

        private void Final_grade_page_Load(object sender, EventArgs e)
        {
            XmlDocument myxml = new XmlDocument();
            myxml.Load("course.xml");
            XmlNode demo = myxml.DocumentElement;
            foreach (XmlNode node in demo)
            {
                comboBox1.Items.Add(node.Attributes[0].Value);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                Gradelist grade = new Gradelist(comboBox1.SelectedItem.ToString());
                this.Hide();
                grade.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox1.SelectedItem.ToString());
            if (comboBox1.SelectedItem != null)
            {
                ProportionOfSection proportion = new ProportionOfSection(comboBox1.SelectedItem.ToString());
                this.Hide();
                proportion.ShowDialog();
            }
            else
                MessageBox.Show("Please select a course first!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Home form2 = new Home();
            this.Hide();
            form2.ShowDialog();
        }
    }
}
