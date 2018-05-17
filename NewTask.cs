using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Forms;

namespace course
{
    public partial class NewTask : Form
    {
        public DataGridViewRow dgvr;
        public String course;
        public String filename = "task.xml";
        public NewTask(DataGridViewRow myRow)
        {
            InitializeComponent();
            dgvr = myRow;
            course = dgvr.Cells[0].Value.ToString();
            label1.Text = course;

            //XDocument xdoc = XDocument.Load(filename);

            XElement root = XElement.Load(filename);
            IEnumerable<XElement> address =
                from el in root.Elements("courses")
                where (string)el.Attribute("CourseName") == course
                select el;


            var query = from n in address.Elements()
                        select new
                        {
                            Task = n.Attribute("Task").Value,
                            Rubric = n.Attribute("Rubric").Value                       
                        };
            dataGridView1.DataSource = query.ToList();
            
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void NewTask_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(filename))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                XmlNode node = doc.SelectSingleNode("//courses");
                XmlNode node1 = doc.CreateElement("task");
                node1.Attributes.Append(CreateAttribute(node1, "CourseName", course));
                node1.Attributes.Append(CreateAttribute(node1, "Task", textBox1.Text));
                node1.Attributes.Append(CreateAttribute(node1, "Rubric", comboBox2.Text));
                node.AppendChild(node1);

                doc.Save(filename);

            }
            else
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                XmlElement root = doc.CreateElement("courses");
                doc.AppendChild(root);

                XmlNode node = doc.CreateElement("task");
                node.Attributes.Append(CreateAttribute(node, "CourseName", course));
                node.Attributes.Append(CreateAttribute(node, "Task", textBox1.Text));
                node.Attributes.Append(CreateAttribute(node, "Rubric", comboBox2.Text));
                root.AppendChild(node);

                doc.Save(filename);

            }

            //display the data into the datagrideview
            XDocument xdoc = XDocument.Load(filename);
            var query = from n in xdoc.Descendants("task")
                        select new
                        {
                            Task = n.Attribute("Task").Value,
                            Rubric = n.Attribute("Rubric").Value             
                        };
            dataGridView1.DataSource = query.ToList();


        }
    }
}
