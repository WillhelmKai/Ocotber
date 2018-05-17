using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Xml.Linq;

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        public int count = 0;
        public String id;
        public String name;
        public String row;
        public bool flag = true;
        public String finalname;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string rubricname)
        {
            InitializeComponent();
            name = rubricname;
        }


        private void label1_Click(object sender, EventArgs e)
        {

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

        private void Form2_Load(object sender, EventArgs e)
        {
            /*string xmlpath = @"Courses.xml";
            XDocument xdoc = XDocument.Load(xmlpath);
            var query = from n in xdoc.Descendants("course")
                        select new
                        {
                            id = n.Attribute("id").Value,
                            Criteria = n.Attribute("Criteria").Value,
                            Excellent = n.Attribute("Excellent").Value,
                            Good = n.Attribute("Good").Value,
                            Satisfactory = n.Attribute("Satisfactory").Value,
                            Pass = n.Attribute("Pass").Value,
                            Fail = n.Attribute("Fail").Value
                        };
            dataGridView1.DataSource = query.ToList();*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            // if the file exists, we'll add the student into the xml
            if (File.Exists("rubric.xml"))
            {
                //MessageBox.Show("File exist ");
                //insert new student info
                XmlDocument doc = new XmlDocument();
                doc.Load("rubric.xml");
                XmlNode node = doc.SelectSingleNode("//UIC");
                XmlNodeList xnList = node.ChildNodes;
                foreach (XmlNode xn in xnList)
                {
                    if(xn.Attributes[1].Value.Equals(name))
                    {
                        flag = false;
                        id = xn.Attributes[0].Value;
                    }
                    count++;
                }
                if (flag)
                {
                    XmlNode node1 = doc.CreateElement("courses");
                    id = Convert.ToString(count + 1);
                    node1.Attributes.Append(CreateAttribute(node1, "id", id));
                    node1.Attributes.Append(CreateAttribute(node1, "name", name));
                    node.AppendChild(node1);
                    XmlNode node2 = doc.CreateElement("task");
                     row = String.Format("{0:F}", (float.Parse(id) + 0.10));
                    node2.Attributes.Append(CreateAttribute(node2, "row", row));
                    node2.Attributes.Append(CreateAttribute(node2, "Item", textBox7.Text));
                    node2.Attributes.Append(CreateAttribute(node, "Percentage", textBox1.Text));
                    node2.Attributes.Append(CreateAttribute(node2, "A", textBox2.Text));
                    node2.Attributes.Append(CreateAttribute(node2, "B", textBox3.Text));
                    node2.Attributes.Append(CreateAttribute(node2, "C", textBox4.Text));
                    node2.Attributes.Append(CreateAttribute(node2, "D", textBox5.Text));
                    node2.Attributes.Append(CreateAttribute(node2, "F", textBox6.Text));
                    node1.AppendChild(node2);
                }
                else
                {
                    XmlNode node1 = doc.SelectSingleNode("//courses[@id='"+ id +"']");

                    XmlNode node2 = doc.CreateElement("task");
                    row = String.Format("{0:F}", (float.Parse(node1.LastChild.Attributes[0].Value) + 0.10));
                    node2.Attributes.Append(CreateAttribute(node2, "row", row));
                    node2.Attributes.Append(CreateAttribute(node2, "Item", textBox7.Text));
                    node2.Attributes.Append(CreateAttribute(node, "Percentage", textBox1.Text));
                    node2.Attributes.Append(CreateAttribute(node2, "A", textBox2.Text));
                    node2.Attributes.Append(CreateAttribute(node2, "B", textBox3.Text));
                    node2.Attributes.Append(CreateAttribute(node2, "C", textBox4.Text));
                    node2.Attributes.Append(CreateAttribute(node2, "D", textBox5.Text));
                    node2.Attributes.Append(CreateAttribute(node2, "F", textBox6.Text));
                    node1.AppendChild(node2);
                }

                doc.Save("rubric.xml");  //Save the xml
                string path = "rubric.xml";
                DataSet myds = new DataSet();
                myds.ReadXml(path);
                dataGridView1.DataSource = myds.Tables[1];
                /*  string xmlpath = finalname;
              XDocument xdoc = XDocument.Load(xmlpath);
              var query = from n in xdoc.Descendants("course")
                          select new
                          {
                              id = n.Attribute("id").Value,
                              Criteria = n.Attribute("Criteria").Value,
                              Excellent = n.Attribute("Excellent").Value,
                              Good = n.Attribute("Good").Value,
                              Satisfactory = n.Attribute("Satisfactory").Value,
                              Pass = n.Attribute("Pass").Value,
                              Fail = n.Attribute("Fail").Value
                          };
              dataGridView1.DataSource = query.ToList();*/


            }
            else //The file does not exist, we'll create it
            {
               /* XmlDocument doc = new XmlDocument();
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                //create the root element
                XmlElement root = doc.CreateElement("UIC");
                doc.AppendChild(root);
                //create the "Students" element
                XmlElement element = doc.CreateElement("courses");
                element.Attributes.Append(CreateAttribute(element, "id", "1"));
                element.Attributes.Append(CreateAttribute(element, "name", name));
                root.AppendChild(element);
                //create the "Student" element with attributes
                XmlNode node = doc.CreateElement("task");
                node.Attributes.Append(CreateAttribute(node, "row", "1.1"));
                node.Attributes.Append(CreateAttribute(node, "Criteria", textBox7.Text));
                node.Attributes.Append(CreateAttribute(node, "Percentage", textBox1.Text));
                node.Attributes.Append(CreateAttribute(node, "Excellent", textBox2.Text));
                node.Attributes.Append(CreateAttribute(node, "Good", textBox3.Text));
                node.Attributes.Append(CreateAttribute(node, "Satisfactory", textBox4.Text));
                node.Attributes.Append(CreateAttribute(node, "Pass", textBox5.Text));
                node.Attributes.Append(CreateAttribute(node, "Fail", textBox6.Text));
                element.AppendChild(node);

                doc.Save("rubric.xml");*/

            }
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*Form3 f3 = new Form3(dataGridView1.CurrentRow);
            f3.Show();
            this.Hide();*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
