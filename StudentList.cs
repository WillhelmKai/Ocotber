using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp6
{
    public partial class StudentList : Form
    {
        public StudentList()
        {
            InitializeComponent();
        }

        private void skinTextBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            Student form4 = new Student();
            this.Hide();
            form4.ShowDialog();
        }

        private void skinTextBox1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student form4 = new Student();
            this.Hide();
            form4.ShowDialog();
        }

        private void StudentList_Load(object sender, EventArgs e)
        {
            if (File.Exists("student.xml"))
            {
                string xmlpath = @"student.xml";
                XDocument xdoc = XDocument.Load(xmlpath);
                var query = from n in xdoc.Descendants("task")
                            select new
                            {
                               // Course = n.Attribute("Course").Value,
                                SurName = n.Attribute("SurName").Value,
                                FirstName = n.Attribute("FirstName").Value,
                                ID = n.Attribute("ID").Value,
                                Group = n.Attribute("Group").Value
                            };
                skinDataGridView1.DataSource = query.ToList();

            }
        }
    }
}
