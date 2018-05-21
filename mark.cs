using System;
using Evaluate;
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
using WindowsFormsApp6;
using System.IO;

namespace Evaluate
{
    public partial class mark : Form
    {
        public string name;
        public string course;
        public string taskname;
        public int count = 0;
        public int sumcount;
        public bool flag = true;
        public bool evaflag;
        public bool editflag = true;
        public mark()
        {
            InitializeComponent();
        }
        public DataGridViewRow dgvr;
        public mark(DataGridViewRow myDGVR)
        {
            InitializeComponent();
            dgvr = myDGVR;
            name = dgvr.Cells[2].Value.ToString();
            taskname = dgvr.Cells[1].Value.ToString();
            course = dgvr.Cells[0].Value.ToString();

            //  getData();
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

        public double transform(String grade)
        {
            if (grade.Equals("A"))
            {
                return 100;
            }
            else if (grade.Equals("B"))
            {
                return 80;
            }
            else if (grade.Equals("C"))
            {
                return 60;
            }
            else if (grade.Equals("D"))
            {
                return 40;
            }
            else if (grade.Equals("F"))
            {
                return 20;
            }
            else if (grade.Equals("1day"))
            {
                return 0.3;
            }
            else if (grade.Equals("2days"))
            {
                return 0.5;
            }
            else if (grade.Equals("morethan3days"))
            {
                return 1.0;
            }
            else if (grade.Equals(null))
            {
                return 0;
            }
            else
            {
                return 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EVA eva = new EVA();
            this.Hide();
            eva.ShowDialog();
        }

        private void mark_Load(object sender, EventArgs e)
        {
            string xmlpath = @"rubric.xml";
            XElement root = XElement.Load(xmlpath);
            IEnumerable<XElement> address =
               from el in root.Elements("courses")
               where (string)el.Attribute("name") == name
               select el;
            var query = from n in address.Elements()
                        select new
                        {
                            Item = n.Attribute("Item").Value,
                            A = n.Attribute("A").Value,
                            B = n.Attribute("B").Value,
                            C = n.Attribute("C").Value,
                            D = n.Attribute("D").Value,
                            F = n.Attribute("F").Value
                        };
            dataGridView1.DataSource = query.ToList();


            string strPath = @"student.xml";
            XElement student = XElement.Load(strPath);
            IEnumerable<XElement> stu = from st in student.Elements("student")
                                        where (string)st.Attribute("Course") == course
                                        select st;
            var query1 = from n in stu.Elements()
                         select new
                         {
                             Studentname = n.Attribute("ID").Value
                         };
            dataGridView2.DataSource = query1.ToList();

            //extract information in rubric
            XmlDocument doc = new XmlDocument();
            doc.Load("rubric.xml");
            string[] array = new string[100];
            string stupath = "//courses[@name='" + name + "']//task";
            XmlNodeList nodeList = doc.SelectNodes(stupath);
            foreach (XmlNode item in nodeList)
            {
                array[count] = item.Attributes[1].Value;
                count++;
            }
            sumcount = count + 1;
            for (; count > 0; count--)
            {
                DataGridViewComboBoxColumn dgcbc = new DataGridViewComboBoxColumn();
                DataGridViewComboBoxCell it = new DataGridViewComboBoxCell();
                it.Items.Add("A");
                it.Items.Add("B");
                it.Items.Add("C");
                it.Items.Add("D");
                it.Items.Add("F");
                dgcbc.CellTemplate = it;
                //设置列的属性
                dgcbc.DataPropertyName = array[count - 1];
                dgcbc.Name = array[count - 1];
                dgcbc.HeaderText = array[count - 1];
                dataGridView2.Columns.Add(dgcbc);
            }

            DataGridViewComboBoxColumn delay = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxCell idelay = new DataGridViewComboBoxCell();
            idelay.Items.Add("1day");
            idelay.Items.Add("2days");
            idelay.Items.Add("morethan3days");
            delay.CellTemplate = idelay;
            //设置列的属性
            delay.DataPropertyName = "delay";
            delay.Name = "delay";
            delay.HeaderText = "delay";
            dataGridView2.Columns.Add(delay);

            DataGridViewTextBoxColumn sum = new DataGridViewTextBoxColumn();
            sum.Name = "sum";
            sum.DataPropertyName = "sum";
            sum.HeaderText = "sum";
            dataGridView2.Columns.Add(sum);
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                dataGridView2.Rows[i].Cells[dataGridView2.ColumnCount - 1].Value = 0;
            }


        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("你选定的项为:" + dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString());
            int column = dataGridView2.ColumnCount;
            double average = Convert.ToDouble(column - 3);
            dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value = 0;
            for (int i = 1; i < column - 2; i++)
            {
                if (dataGridView2.Rows[e.RowIndex].Cells[i].Value != null)
                {
                    dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value = transform(dataGridView2.Rows[e.RowIndex].Cells[i].Value.ToString()) / average + Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value);
                }
            }
            if (dataGridView2.Rows[e.RowIndex].Cells[column - 2].Value != null)
            {
                dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value) - Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value) * transform(dataGridView2.Rows[e.RowIndex].Cells[column - 2].Value.ToString());
            }

            /* if(dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString().Equals("A"))
             {

                 dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value) + (100.0 / average);

             }
             else if(dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString().Equals("B"))
             {
                 dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value) + (80.0 / average);
             }
             else if(dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString().Equals("C"))
             {
                 dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value) + (60.0 / average);
             }
             else if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString().Equals("D"))
             {
                 dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value) + (40.0 / average);
             }
             else
             {
                 dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[column - 1].Value) + (20.0 / average);
             }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists("evaluate.xml"))
            {
                int i;
                XmlDocument doc = new XmlDocument();
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                //create the root element
                XmlElement root = doc.CreateElement("UIC");
                doc.AppendChild(root);
                //create the "Students" element
                /*XmlElement element = doc.CreateElement("Students");
                root.AppendChild(element);*/
                for (i = 0; i < dataGridView2.RowCount; i++)
                {
                    if (dataGridView2.Rows[i].Cells[dataGridView2.ColumnCount - 1].Value.ToString() == "0")
                    {
                        flag = false;
                    }
                }



                if (flag)
                {
                    for (i = 0; i < dataGridView2.RowCount; i++)
                    {
                        XmlNode courses = doc.CreateElement("student");
                        courses.Attributes.Append(CreateAttribute(courses, "id", dataGridView2.Rows[i].Cells[0].Value.ToString()));
                        root.AppendChild(courses);
                        XmlNode task = doc.CreateElement("task");
                        task.Attributes.Append(CreateAttribute(task, "CourseName", course));
                        task.Attributes.Append(CreateAttribute(task, "Task", taskname));
                        task.Attributes.Append(CreateAttribute(task, "Rubric", name));
                        task.Attributes.Append(CreateAttribute(task, "Grade", dataGridView2.Rows[i].Cells[dataGridView2.ColumnCount - 1].Value.ToString()));
                        courses.AppendChild(task);
                    }
                    doc.Save("evaluate.xml");
                    MessageBox.Show("XML File created ! ");
                }
                else
                {
                    MessageBox.Show("All students should be graded");
                }
            }
            else
            {
                int countstu = 0;
                XmlDocument doc1 = new XmlDocument();
                doc1.Load("evaluate.xml");
                XmlNode node = doc1.SelectSingleNode("//UIC");
                XmlNodeList xnList = node.ChildNodes;
                for (; countstu < dataGridView2.RowCount; countstu++)
                {
                    evaflag = true;
                    foreach (XmlNode xn in xnList)
                    {
                        if (xn.Attributes[0].Value.Equals(dataGridView2.Rows[countstu].Cells[0].Value.ToString()))
                        {
                            editflag = true;
                            XmlNodeList nls = xn.ChildNodes;
                            foreach (XmlNode newxn in nls)
                            {
                                XmlElement neweva = (XmlElement)newxn;
                                if (newxn.Attributes[0].Value.Equals(course) && newxn.Attributes[1].Value.Equals(taskname))
                                {
                                    neweva.SetAttribute("Grade", dataGridView2.Rows[countstu].Cells[dataGridView2.ColumnCount - 1].Value.ToString());
                                    editflag = false;
                                    evaflag = false;
                                    break;
                                }
                            }


                            if (editflag)
                            {
                                XmlNode task = doc1.CreateElement("task");
                                task.Attributes.Append(CreateAttribute(task, "CourseName", course));
                                task.Attributes.Append(CreateAttribute(task, "Task", taskname));
                                task.Attributes.Append(CreateAttribute(task, "Rubric", name));
                                task.Attributes.Append(CreateAttribute(task, "Grade", dataGridView2.Rows[countstu].Cells[dataGridView2.ColumnCount - 1].Value.ToString()));
                                xn.AppendChild(task);
                                evaflag = false;
                            }



                        }

                    }
                    if (evaflag)
                    {
                        XmlNode courses = doc1.CreateElement("student");
                        courses.Attributes.Append(CreateAttribute(courses, "id", dataGridView2.Rows[countstu].Cells[0].Value.ToString()));
                        node.AppendChild(courses);
                        XmlNode task = doc1.CreateElement("task");
                        task.Attributes.Append(CreateAttribute(task, "CourseName", course));
                        task.Attributes.Append(CreateAttribute(task, "Task", taskname));
                        task.Attributes.Append(CreateAttribute(task, "Rubric", name));
                        task.Attributes.Append(CreateAttribute(task, "Grade", dataGridView2.Rows[countstu].Cells[dataGridView2.ColumnCount - 1].Value.ToString()));
                        courses.AppendChild(task);
                    }
                }

                doc1.Save("evaluate.xml");
            }
        }
    }
}
