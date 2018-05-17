using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace course
{
    
    public partial class NewCourse : Form
    {
        public String filename = "course.xml";
       // string input_string;
        public NewCourse()
        {
            InitializeComponent();
        }

        public XmlAttribute CreateAttribute(XmlNode node, string attributeName, string value)
        {
            try
            {
                XmlDocument doc = node.OwnerDocument;
                XmlAttribute attr = null;
                attr = doc.CreateAttribute(attributeName);
                attr.Value = value;
                node.Attributes.SetNamedItem(attr);
                return attr;
            }
            catch (Exception err)
            {
                string desc = err.Message;
                return null;
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            CourseIndex form1 = new CourseIndex();
            this.Hide();
            form1.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void New_Click(object sender, EventArgs e)
        {
            if ( File.Exists(filename) )
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                XmlNode node = doc.SelectSingleNode("//courses");
                XmlNode node1 = doc.CreateElement("course");
                node1.Attributes.Append(CreateAttribute(node1, "CourseName", textBox1.Text));
                node1.Attributes.Append(CreateAttribute(node1, "CourseNumber", textBox2.Text));
                node1.Attributes.Append(CreateAttribute(node1, "Student", comboBox1.Text));
                node.AppendChild(node1);

                doc.Save(filename);

            } else {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0","utf-8",null);
                doc.AppendChild(dec);
                XmlElement root = doc.CreateElement("courses");
                doc.AppendChild(root);

                XmlNode node = doc.CreateElement("course");
                node.Attributes.Append(CreateAttribute(node, "CourseName", textBox1.Text));
                node.Attributes.Append(CreateAttribute(node, "CourseNumber", textBox2.Text));
                node.Attributes.Append(CreateAttribute(node, "Student", comboBox1.Text));
                root.AppendChild(node);

                doc.Save(filename);

            }
        }
    }
}
