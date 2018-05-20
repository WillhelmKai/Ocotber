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
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApp6
{
    public partial class StudentSearch : Form
    {
        public StudentSearch()
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
            Student form4 = new Student();
            this.Hide();
            form4.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            if (File.Exists("gradelist.xml"))
            {
                int j = 0;
                XmlDocument myxml = new XmlDocument();
                myxml.Load("gradelist.xml");
                XmlNode demo = myxml.DocumentElement;
                foreach (XmlNode node in demo.ChildNodes)
                {
                    if (textBox.Text == (node.Attributes[1].Value.ToString() + node.Attributes[2].Value.ToString()))
                    {
                        XElement root1 = XElement.Load("gradelist.xml");
                        IEnumerable<XElement> address =
                            from el in root1.Elements("student")
                            where ((string)el.Attribute("FirstName")+(string)el.Attribute("SurName")) == textBox.Text
                            select el;

                        var query = from n in address.Elements()
                                    select new
                                    {
                                        id = n.Parent.Attribute("id").Value,
                                        firstname = n.Parent.Attribute("FirstName").Value,
                                        lastname = n.Parent.Attribute("SurName").Value,
                                        course = n.Attribute("course").Value,
                                        grade = n.Attribute("grade").Value
                                    };
                        skinDataGridView1.DataSource = query.ToList();

                        j = 1;

                    }
                    if (textBox.Text == (node.Attributes[1].Value.ToString() + " ".ToString() + node.Attributes[2].Value.ToString()))
                    {
                        XElement root1 = XElement.Load("gradelist.xml");
                        IEnumerable<XElement> address =
                            from el in root1.Elements("student")
                            where ((string)el.Attribute("FirstName") + " ".ToString() + (string)el.Attribute("SurName")) == textBox.Text
                            select el;

                        var query = from n in address.Elements()
                                    select new
                                    {
                                        id = n.Parent.Attribute("id").Value,
                                        firstname = n.Parent.Attribute("FirstName").Value,
                                        lastname = n.Parent.Attribute("SurName").Value,
                                        course = n.Attribute("course").Value,
                                        grade = n.Attribute("grade").Value
                                    };
                        skinDataGridView1.DataSource = query.ToList();

                        j = 1;
                    }

                }

                if (j == 0) {
                    MessageBox.Show("No such student!!");
                }

            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists("gradelist.xml"))
            {
                int j = 0;
                XmlDocument myxml = new XmlDocument();
                myxml.Load("gradelist.xml");
                XmlNode demo = myxml.DocumentElement;
                foreach (XmlNode node in demo.ChildNodes)
                {
                    if (textBox1.Text == (node.Attributes[0].Value.ToString()))
                    {
                        XElement root1 = XElement.Load("gradelist.xml");
                        IEnumerable<XElement> address =
                            from el in root1.Elements("student")
                            where ((string)el.Attribute("id")) == textBox1.Text
                            select el;

                        var query = from n in address.Elements()
                                    select new
                                    {
                                        id = n.Parent.Attribute("id").Value,
                                        firstname = n.Parent.Attribute("FirstName").Value,
                                        lastname = n.Parent.Attribute("SurName").Value,
                                        course = n.Attribute("course").Value,
                                        grade = n.Attribute("grade").Value
                                    };
                        skinDataGridView1.DataSource = query.ToList();

                        j = 1;

                    }
                }

                if (j == 0)
                {
                    MessageBox.Show("No such student!!");
                }

            }

        }
    }
}
