using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;
using WindowsFormsApp6;
using System.IO;

namespace course
{
    public partial class CourseIndex : Form
    {
        public CourseIndex()
        {
            InitializeComponent();
        }
        
        private void New_Click(object sender, EventArgs e)
        {
            NewCourse newcourse = new NewCourse();
            this.Hide();
            newcourse.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NewTask newtask = new NewTask(dataGridView1.CurrentRow);
            this.Hide();
            newtask.Show();
        }

        private void CourseIndex_Load_1(object sender, EventArgs e)
        {
            if (File.Exists("course.xml"))
            {
                string xmlpath = @"course.xml";
                XDocument xdoc = XDocument.Load(xmlpath);
                var query = from n in xdoc.Descendants("course")
                            select new
                            {
                                Course = n.Attribute("CourseName").Value,
                                Code = n.Attribute("CourseNumber").Value,
                                Student = n.Attribute("Student").Value,
                            };
                dataGridView1.DataSource = query.ToList();
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "Edit";
                btn.HeaderText = "Edit";
                btn.DefaultCellStyle.NullValue = "Edit";
                dataGridView1.Columns.Add(btn);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Home form2 = new Home();
            this.Hide();
            form2.ShowDialog();
        }
    }
}
