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
        public bool flag=true;
        public bool taskflag=true;
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
                            CourseName= n.Attribute("CourseName").Value,
                            Task = n.Attribute("Task").Value,
                            Rubric = n.Attribute("Rubric").Value                       
                        };
            dataGridView1.DataSource = query.ToList();

            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            btn1.Name = "Delete";
            btn1.HeaderText = "Delete";
            btn1.DefaultCellStyle.NullValue = "Delete";
            dataGridView1.Columns.Add(btn1);

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
            string xmlpath = @"task.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlpath);
            XmlNode prexn = xmlDoc.SelectSingleNode("UIC");
            XmlNodeList xnList = prexn.ChildNodes;

            foreach (XmlNode xn in xnList)
            {
                if (xn.Attributes[0].Value == dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString())
                {
                    XmlNodeList xnList1 = xn.ChildNodes;
                    foreach (XmlNode lastxn in xnList1)
                    {
                        if(lastxn.Attributes[1].Value== dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString())
                        {
                            xn.RemoveChild(lastxn);
                        }
                    }

                }
            }
            xmlDoc.Save(xmlpath);
            XElement root = XElement.Load(filename);
            IEnumerable<XElement> address =
                from el in root.Elements("courses")
                where (string)el.Attribute("CourseName") == course
                select el;


            var query = from n in address.Elements()
                        select new
                        {
                            CourseName = n.Attribute("CourseName").Value,
                            Task = n.Attribute("Task").Value,
                            Rubric = n.Attribute("Rubric").Value
                        };
            dataGridView1.DataSource = query.ToList();
        }

        private void NewTask_Load(object sender, EventArgs e)
        {
            XmlDocument myxml = new XmlDocument();
            myxml.Load("rubric.xml");
            XmlNode demo = myxml.DocumentElement;
            foreach (XmlNode node in demo.ChildNodes)
            {
                comboBox2.Items.Add(node.Attributes[1].Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag = true;
            taskflag = true;

            if (textBox1.Text == "" || comboBox2.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Please input all the information!!");

            }
            else
            {
                if (File.Exists(filename))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filename);
                    XmlNodeList nodeList = doc.SelectSingleNode("UIC").ChildNodes;
                    foreach (XmlNode xn in nodeList)
                    {
                        XmlElement xe = (XmlElement)xn;

                        if (xe.GetAttribute("CourseName") == course)
                        {
                            XmlNodeList nodeList1 = xn.ChildNodes;
                            foreach (XmlNode xn1 in nodeList1)
                            {
                                XmlElement xe1 = (XmlElement)xn1;
                                if (xe1.GetAttribute("Task") == textBox1.Text)
                                {
                                    taskflag = false;
                                }
                            }

                            flag = false;

                        }
                    }

                    if(flag)
                    {
                        XmlNode node = doc.SelectSingleNode("//UIC");

                        XmlNode node1 = doc.CreateElement("courses");
                        node1.Attributes.Append(CreateAttribute(node1, "CourseName", course));
                        node.AppendChild(node1);
                        XmlNode node2 = doc.CreateElement("task");
                        node2.Attributes.Append(CreateAttribute(node2, "CourseName", course));
                        node2.Attributes.Append(CreateAttribute(node2, "Task", textBox1.Text));
                        node2.Attributes.Append(CreateAttribute(node2, "Rubric", comboBox2.Text));
                        node2.Attributes.Append(CreateAttribute(node2, "Percentage", "0"));
                        node2.Attributes.Append(CreateAttribute(node2, "Group", comboBox1.Text));
                        node2.Attributes.Append(CreateAttribute(node2, "finish", "N"));

                        node1.AppendChild(node2);

                        doc.Save(filename);
                        MessageBox.Show("Added Successful!!");
                    }
                    else
                    {
                        if(taskflag)
                        {
                            XmlNode node = doc.SelectSingleNode("//courses[@CourseName='" + course + "']");

                            XmlNode node1 = doc.CreateElement("task");
                            node1.Attributes.Append(CreateAttribute(node1, "CourseName", course));
                            node1.Attributes.Append(CreateAttribute(node1, "Task", textBox1.Text));
                            node1.Attributes.Append(CreateAttribute(node1, "Rubric", comboBox2.Text));
                            node1.Attributes.Append(CreateAttribute(node1, "Percentage", "0"));
                            node1.Attributes.Append(CreateAttribute(node1, "Group", comboBox1.Text));
                            node1.Attributes.Append(CreateAttribute(node1, "finish", "N"));

                            node.AppendChild(node1);

                            doc.Save(filename);
                            MessageBox.Show("Added Successful!!");
                        }
                        else
                        {
                            XmlNode node = doc.SelectSingleNode("//courses[@CourseName='" + course + "']");
                            XmlNodeList nodeList2 = node.ChildNodes;
                            foreach(XmlNode taskxn in nodeList2)
                            {
                                XmlElement xe = (XmlElement)taskxn;
                                if (xe.GetAttribute("Task") == textBox1.Text)
                                {
                                    xe.Attributes[0].Value = course;
                                    xe.Attributes[1].Value = textBox1.Text;
                                    xe.Attributes[2].Value = comboBox2.Text;
                                    xe.Attributes[3].Value = "0";
                                    xe.Attributes[4].Value = comboBox1.Text;
                                    xe.Attributes[5].Value = "Y";

                                }
                            }
                            doc.Save(filename);
                            MessageBox.Show("Added Successful!!");
                        }
                        
                    }

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
                    node.Attributes.Append(CreateAttribute(node, "Percentage", "0"));
                    node.Attributes.Append(CreateAttribute(node, "Group", comboBox1.Text));
                    node.Attributes.Append(CreateAttribute(node, "finish", "N"));

                    root.AppendChild(node);

                    doc.Save(filename);
                    MessageBox.Show("Added Successful!!");
                }

                XElement root1 = XElement.Load(filename);
                IEnumerable<XElement> address =
                    from el in root1.Elements("courses")
                    where (string)el.Attribute("CourseName") == course
                    select el;


                var query = from n in address.Elements()
                            select new
                            {
                                CourseName = n.Attribute("CourseName").Value,
                                Task = n.Attribute("Task").Value,
                                Rubric = n.Attribute("Rubric").Value
                            };
                dataGridView1.DataSource = query.ToList();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CourseIndex form2 = new CourseIndex();
            this.Hide();
            form2.ShowDialog();
        }
    }
}
