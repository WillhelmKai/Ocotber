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
using System.Xml;

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
        public void show()
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

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dataGridView1.ColumnCount-2)
            {
                NewTask newtask = new NewTask(dataGridView1.CurrentRow);
                this.Hide();
                newtask.Show();
            }
            if(e.ColumnIndex == dataGridView1.ColumnCount - 1)
            {
                string xmlpath = @"task.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlpath);
                XmlNode prexn = xmlDoc.SelectSingleNode("UIC");
                XmlNodeList xnList = prexn.ChildNodes;

                foreach (XmlNode xn in xnList)
                {
                    if (xn.Attributes[0].Value == dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())
                    {

                        xn.RemoveAll();
                        xn.ParentNode.RemoveChild(xn);

                    }
                }
                xmlDoc.Save(xmlpath);

                string xmlpath1 = @"course.xml";
                XmlDocument xmlDoc1 = new XmlDocument();
                xmlDoc1.Load(xmlpath1);
                XmlNode prexn1 = xmlDoc1.SelectSingleNode("courses");
                XmlNodeList xnList2 = prexn1.ChildNodes;

                foreach (XmlNode xn in xnList2)
                {
                    if (xn.Attributes[0].Value == dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())
                    {

                        prexn1.RemoveChild(xn);

                    }
                }
                xmlDoc1.Save(xmlpath1);
            }
            show();
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

                DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
                btn1.Name = "Delete";
                btn1.HeaderText = "Delete";
                btn1.DefaultCellStyle.NullValue = "Delete";
                dataGridView1.Columns.Add(btn1);
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
