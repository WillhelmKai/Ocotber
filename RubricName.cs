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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public bool flag;
        public bool text;

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
            if (!File.Exists("rubric.xml")) {

                XmlDocument doc = new XmlDocument();
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                //create the root element
                XmlElement root = doc.CreateElement("UIC");
                doc.AppendChild(root);
                //create the "Students" element
                /*XmlElement element = doc.CreateElement("Students");
                root.AppendChild(element);*/

                //create the "Student" element with attributes
                XmlNode courses = doc.CreateElement("courses");
                courses.Attributes.Append(CreateAttribute(courses, "id", "0"));
                courses.Attributes.Append(CreateAttribute(courses, "name", "0"));
                root.AppendChild(courses);
                XmlNode task = doc.CreateElement("task");
                task.Attributes.Append(CreateAttribute(task, "row", "0"));
                task.Attributes.Append(CreateAttribute(task, "Item", "0"));
                task.Attributes.Append(CreateAttribute(task, "Percentage", "0"));
                task.Attributes.Append(CreateAttribute(task, "A", "0"));
                task.Attributes.Append(CreateAttribute(task, "B", "0"));
                task.Attributes.Append(CreateAttribute(task, "C", "0"));
                task.Attributes.Append(CreateAttribute(task, "D", "0"));
                task.Attributes.Append(CreateAttribute(task, "F", "0"));
                courses.AppendChild(task);
                doc.Save("rubric.xml");






                MessageBox.Show("This sheet has been added successfully! ");
                Form2 frm2 = new Form2(textBox1.Text);
                this.Hide();  //use the Hide method to hide the form1 when form2 is shown
                frm2.ShowDialog();

            } else
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("You should enter content in the textbox");
                    text = false;
                }
                else
                {
                    text = true;
                }
                if (text)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("rubric.xml");
                    XmlNode node = doc.SelectSingleNode("//UIC");
                    XmlNodeList xnList = node.ChildNodes;
                    foreach (XmlNode xn in xnList)
                    {
                        if (xn.Attributes[1].Value.Equals(textBox1.Text))
                        {
                            flag = true;
                        }

                    }


                    if (flag)
                    {
                        Form5 frm5 = new Form5();
                        this.Hide();  //use the Hide method to hide the form1 when form2 is shown
                        frm5.ShowDialog();
                    }
                    else
                    {
                        Form2 frm2 = new Form2(textBox1.Text);
                        this.Hide();  //use the Hide method to hide the form1 when form2 is shown
                        frm2.ShowDialog();

                    }
                }

            }  

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
