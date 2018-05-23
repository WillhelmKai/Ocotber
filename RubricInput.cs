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
using System.Text.RegularExpressions;

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
        public bool text = true;
        public bool grade = true;
        public bool countmodify = true;
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

            if (string.IsNullOrWhiteSpace(textBox1.Text) && text)
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text) && text)
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text) && text)
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text) && text)
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            if (string.IsNullOrWhiteSpace(textBox5.Text) && text)
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            if (string.IsNullOrWhiteSpace(textBox6.Text) && text)
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            if (string.IsNullOrWhiteSpace(textBox7.Text) && text)
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            string SuID = textBox1.Text.ToString();
            Regex reg = new Regex("^[0-9]+$");
            Match ma = reg.Match(SuID);
            if (!ma.Success)
            {
                MessageBox.Show("You should enter integer in percentage");
                text = false;
            }
            if(text)
            {
                if (Convert.ToInt32(textBox1.Text) > 100)
                {
                    MessageBox.Show("You shouldn't enter integer in percentage excceed 100");
                    grade = false;
                }

            }
            if(!countmodify)
            {
                count = 0;
            }
            // if the file exists, we'll add the student into the xml

            //MessageBox.Show("File exist ");
            //insert new student info
            if(text==true && grade==true)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("rubric.xml");
                XmlNode node = doc.SelectSingleNode("//UIC");
                XmlNodeList xnList = node.ChildNodes;
                foreach (XmlNode xn in xnList)
                {
                    if (xn.Attributes[1].Value.Equals(name))
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
                    XmlNode node1 = doc.SelectSingleNode("//courses[@id='" + id + "']");

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

                string strPath = @"rubric.xml";
                XElement student = XElement.Load(strPath);
                IEnumerable<XElement> stu = from st in student.Elements("courses")
                                            where (string)st.Attribute("name") == name
                                            select st;
                var query1 = from n in stu.Elements()
                             select new
                             {
                                 row = n.Attribute("row").Value,
                                 Item = n.Attribute("Item").Value,
                                 Percentage = n.Attribute("Percentage").Value,
                                 A = n.Attribute("A").Value,
                                 B = n.Attribute("B").Value,
                                 C = n.Attribute("C").Value,
                                 D = n.Attribute("D").Value,
                                 F = n.Attribute("F").Value
                             };
                dataGridView1.DataSource = query1.ToList();

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

        private void button3_Click(object sender, EventArgs e)
        {
            int gradesum = 0;
            XmlDataDocument xmlDoc = new XmlDataDocument();
            xmlDoc.Load("rubric.xml");
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("UIC").ChildNodes;
            int count = 0;
            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;

                if (xe.GetAttribute("name") == name)
                {
                    XmlNodeList nls = xe.ChildNodes;
                    foreach (XmlNode xn1 in nls)
                    {
                        gradesum= gradesum + Convert.ToInt32( xn1.Attributes[2].Value) ;
                       
                    }

                }
            }

            if(gradesum==100)
            {
                Form1 f1 = new Form1();
                f1.Show();
                this.Hide();
            }
            else
            {
                countmodify = false;
                string xmlpath = @"rubric.xml";
                XmlDocument xmlDoc1 = new XmlDocument();
                xmlDoc1.Load(xmlpath);
                XmlNode root = xmlDoc1.SelectSingleNode("UIC");
                XmlNodeList xnList = root.ChildNodes;
                foreach (XmlNode xn in xnList)
                {
                    if (xn.Attributes[1].Value.ToString() == name)
                    {

                        root.RemoveChild(xn);

                    }
                }
                xmlDoc1.Save(xmlpath);
                MessageBox.Show("Total percentage should be 100, please enter rubric again");
            }
           

            XElement student = XElement.Load("rubric.xml");
            IEnumerable<XElement> stu = from st in student.Elements("courses")
                                        where (string)st.Attribute("name") == name
                                        select st;
            var query1 = from n in stu.Elements()
                         select new
                         {
                             row = n.Attribute("row").Value,
                             Item = n.Attribute("Item").Value,
                             Percentage = n.Attribute("Percentage").Value,
                             A = n.Attribute("A").Value,
                             B = n.Attribute("B").Value,
                             C = n.Attribute("C").Value,
                             D = n.Attribute("D").Value,
                             F = n.Attribute("F").Value
                         };
            dataGridView1.DataSource = query1.ToList();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
