using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp6;
namespace Evaluate
{
    public partial class ProportionOfSection : Form
    {
        String CourseName;
        public bool flag = false;
        public bool courseflag = true;
        public string firstname;
        public string surname;
        public ProportionOfSection(String course)
        {
            InitializeComponent();
            CourseName = course;
            textBox1.Text = CourseName;
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
        private void Back_Click(object sender, EventArgs e)
        {
            Final_grade_page form2 = new Final_grade_page();
            this.Hide();
            form2.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.Rows.Count;
            // int col = dataGridView1.Rows[1].Cells.Count;
            int sum = 0, a = 0;            //check the persentage            
            for (int i = 0; i < row; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                }
                else
                {
                    a++;
                }
            }
            if (a != 0 || sum != 100)
            {
                MessageBox.Show("Invalid Input");
            }
            else
            {
                //MessageBox.Show("Valid input");
                XmlDataDocument xmlDoc = new XmlDataDocument();
                xmlDoc.Load("task.xml");
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("UIC").ChildNodes;
                int count = 0;
                foreach (XmlNode xn in nodeList)
                {
                    XmlElement xe = (XmlElement)xn; if (xe.GetAttribute("CourseName") == CourseName)
                    {
                        XmlNodeList nls = xe.ChildNodes;
                        foreach (XmlNode xn1 in nls)
                        {
                            xn1.Attributes[3].Value = dataGridView1.Rows[count].Cells[1].Value.ToString();
                            count++;
                        }
                    }
                }
                xmlDoc.Save("task.xml");
                MessageBox.Show("Saved"); if (File.Exists("gradelist.xml"))
                {
                    XmlDataDocument xmlDoc0 = new XmlDataDocument();
                    xmlDoc0.Load("gradelist.xml"); XmlDataDocument xmlDoc1 = new XmlDataDocument();
                    xmlDoc1.Load("evaluate.xml");
                    XmlNodeList newnodeList = xmlDoc1.SelectSingleNode("UIC").ChildNodes; XmlDataDocument xmlDoc2 = new XmlDataDocument();
                    xmlDoc2.Load("student.xml");
                    XmlNodeList newnodeList1 = xmlDoc2.SelectSingleNode("Students").ChildNodes; double sumgrade;
                    foreach (XmlNode xn in newnodeList)
                    {
                        XmlElement xe = (XmlElement)xn;
                        XmlNodeList nls = xe.ChildNodes;
                        int number = 0;
                        sumgrade = 0;
                        foreach (XmlNode xn1 in nls)
                        {
                            XmlElement newxe = (XmlElement)xn1;
                            if (newxe.GetAttribute("CourseName") == CourseName)
                            {
                                flag = true;
                                sumgrade = sumgrade + Convert.ToDouble(newxe.Attributes[3].Value) * (Convert.ToDouble(dataGridView1.Rows[number].Cells[1].Value)) / 100;
                                number++;
                            }
                        }
                        foreach (XmlNode newxn in newnodeList1)
                        {
                            XmlElement newxe1 = (XmlElement)newxn;
                            XmlNodeList newnls = newxe1.ChildNodes; foreach (XmlNode newxn1 in newnls)
                            {
                                XmlElement newtest = (XmlElement)newxn1;
                                if (newtest.GetAttribute("ID") == xe.GetAttribute("id"))
                                {

                                    firstname = newtest.GetAttribute("FirstName");
                                    surname = newtest.GetAttribute("SurName");
                                }
                            }
                        }
                        if (flag)
                        {
                            XmlNode node = xmlDoc0.SelectSingleNode("/UIC");
                            foreach (XmlNode coursenode in node.ChildNodes)
                            {
                                XmlElement newcourse = (XmlElement)coursenode;
                                if (newcourse.GetAttribute("course") == CourseName && newcourse.GetAttribute("id") == xe.GetAttribute("id"))
                                {
                                    foreach (XmlNode grade in coursenode.ChildNodes)
                                    {
                                        XmlElement newgrade = (XmlElement)grade;
                                        newgrade.SetAttribute("grade", sumgrade.ToString());
                                        courseflag = false;
                                    }


                                }
                            }
                        }
                        if (courseflag)
                        {
                            XmlNode node = xmlDoc0.SelectSingleNode("/UIC");
                            XmlNode studentNode = xmlDoc0.CreateElement("student");
                            studentNode.Attributes.Append(CreateAttribute(studentNode, "id", xe.GetAttribute("id")));
                            studentNode.Attributes.Append(CreateAttribute(studentNode, "FirstName", firstname));
                            studentNode.Attributes.Append(CreateAttribute(studentNode, "SurName", surname));
                            studentNode.Attributes.Append(CreateAttribute(studentNode, "course", CourseName));
                            node.AppendChild(studentNode);
                            XmlNode grade = xmlDoc0.CreateElement("grade");
                            grade.Attributes.Append(CreateAttribute(grade, "grade", sumgrade.ToString()));
                            studentNode.AppendChild(grade);
                        }




                        /*  if (flag)
                          {
                              XmlNode node = xmlDoc0.SelectSingleNode("//student[@id='" + xe.GetAttribute("id") + "'] ");
                              foreach (XmlNode coursenode in node.ChildNodes)
                              {
                                  XmlElement newcourse = (XmlElement)coursenode;
                                  if(newcourse.GetAttribute("course")== CourseName)
                                  {
                                      newcourse.SetAttribute("grade", sumgrade.ToString());
                                      courseflag = false;
                                      break;
                                  }
                              }
                              if(courseflag)
                              {
                                  XmlNode grade = xmlDoc0.CreateElement("grade");
                                  grade.Attributes.Append(CreateAttribute(grade, "course", CourseName));
                                  grade.Attributes.Append(CreateAttribute(grade, "grade", sumgrade.ToString()));
                                  node.AppendChild(grade);
                              }
                          }*/
                    }
                    xmlDoc0.Save("gradelist.xml");
                }
                else
                {
                    //create grade list
                    XmlDocument doc = new XmlDocument();
                    XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                    doc.AppendChild(dec);
                    //create the root element
                    XmlElement root = doc.CreateElement("UIC");
                    doc.AppendChild(root); XmlDataDocument xmlDoc1 = new XmlDataDocument();
                    xmlDoc1.Load("evaluate.xml");
                    XmlDataDocument xmlDoc2 = new XmlDataDocument();
                    xmlDoc2.Load("student.xml");
                    XmlNodeList newnodeList1 = xmlDoc2.SelectSingleNode("Students").ChildNodes;
                    XmlNodeList newnodeList = xmlDoc1.SelectSingleNode("UIC").ChildNodes;
                    double sumgrade;
                    foreach (XmlNode xn in newnodeList)
                    {
                        XmlElement xe = (XmlElement)xn;
                        XmlNodeList nls = xe.ChildNodes;
                        int number = 0;
                        sumgrade = 0;
                        foreach (XmlNode xn1 in nls)
                        {
                            XmlElement newxe = (XmlElement)xn1;
                            if (newxe.GetAttribute("CourseName") == CourseName)
                            {
                                flag = true;
                                sumgrade = sumgrade + Convert.ToDouble(newxe.Attributes[3].Value) * (Convert.ToDouble(dataGridView1.Rows[number].Cells[1].Value)) / 100;
                                number++;
                            }
                        }
                        foreach (XmlNode newxn in newnodeList1)
                        {
                            XmlElement newxe1 = (XmlElement)newxn;
                            XmlNodeList newnls = newxe1.ChildNodes; foreach (XmlNode newxn1 in newnls)
                            {
                                XmlElement newtest = (XmlElement)newxn1;
                                if (newtest.GetAttribute("ID") == xe.GetAttribute("id"))
                                {
                                    firstname = newtest.GetAttribute("FirstName");
                                    surname = newtest.GetAttribute("SurName");
                                }
                            }
                        }
                        if (flag)
                        {
                            XmlNode student = doc.CreateElement("student");
                            student.Attributes.Append(CreateAttribute(student, "id", xe.GetAttribute("id")));
                            student.Attributes.Append(CreateAttribute(student, "FirstName", firstname));
                            student.Attributes.Append(CreateAttribute(student, "SurName", surname));
                            student.Attributes.Append(CreateAttribute(student, "course", CourseName));
                            root.AppendChild(student); XmlNode grade = doc.CreateElement("grade");

                            grade.Attributes.Append(CreateAttribute(grade, "grade", sumgrade.ToString()));
                            student.AppendChild(grade);
                        }
                    }
                    doc.Save("gradelist.xml");
                    MessageBox.Show("XML File created ! ");
                }
            }
            Gradelist form2 = new Gradelist(CourseName);
            this.Hide();
            form2.ShowDialog();
        }
        private void label1_Click(object sender, EventArgs e)
        { }
        private void ProportionOfSection_Load(object sender, EventArgs e)
        {
            string xmlpath = @"task.xml";
            XElement root = XElement.Load(xmlpath);
            IEnumerable<XElement> address =
               from el in root.Elements("courses")
               where (string)el.Element("task").Attribute("CourseName") == CourseName
               select el;
            var query = from n in address.Elements()
                        select new
                        {
                            Task = n.Attribute("Task").Value
                        };
            dataGridView1.DataSource = query.ToList(); DataGridViewTextBoxColumn btn = new DataGridViewTextBoxColumn();

            btn.HeaderText = "Precentage";

            btn.DefaultCellStyle.NullValue = "0";
            dataGridView1.Columns.Add(btn);

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { }
    }
}