using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WindowsFormsApp3
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
    
        }

        public bool flag = true;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(flag)
            {
                string xmlpath = @"rubric.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlpath);
                XmlNode root = xmlDoc.SelectSingleNode("UIC");
                XmlNodeList xnList = root.ChildNodes;

                foreach (XmlNode xn in xnList)
                {
                    if (xn.Attributes[0].Value == dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())
                    {

                        root.RemoveChild(xn);

                    }
                }
                xmlDoc.Save(xmlpath);
                XmlNodeList xnList2 = root.ChildNodes;
                foreach (XmlNode xn in xnList2)
                {
                    if (int.Parse(xn.Attributes[0].Value) > int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute("id", Convert.ToString(int.Parse(xn.Attributes[0].Value) - 1));


                    }
                }
                xmlDoc.Save(xmlpath);
                //update

                XDocument xdoc = XDocument.Load(xmlpath);

                var query = from n in xdoc.Descendants("courses")
                            select new
                            {
                                id = n.Attribute("id").Value,
                                name = n.Attribute("name").Value
                            };
                dataGridView1.DataSource = query.ToList();
            }
            else
            {
                string xmlpath = @"rubric.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlpath);
                XmlNode root = xmlDoc.SelectSingleNode("UIC");
                XmlNodeList xnList = root.ChildNodes;

                foreach (XmlNode xn in xnList)
                {
                    if (xn.Attributes[0].Value == dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString())
                    {

                        root.RemoveChild(xn);

                    }
                }
                xmlDoc.Save(xmlpath);
                XmlNodeList xnList2 = root.ChildNodes;
                foreach (XmlNode xn in xnList2)
                {
                    if (int.Parse(xn.Attributes[0].Value) > int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute("id", Convert.ToString(int.Parse(xn.Attributes[0].Value) - 1));


                    }
                }
                xmlDoc.Save(xmlpath);
                //update

                XDocument xdoc = XDocument.Load(xmlpath);

                var query = from n in xdoc.Descendants("courses")
                            select new
                            {
                                id = n.Attribute("id").Value,
                                name = n.Attribute("name").Value
                            };
                dataGridView1.DataSource = query.ToList();
            }
           

           

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string xmlpath = @"rubric.xml";
            XDocument xdoc = XDocument.Load(xmlpath);
            var query = from n in xdoc.Descendants("courses")
                        select new
                        {
                            id = n.Attribute("id").Value,
                            name = n.Attribute("name").Value
                        };
            dataGridView1.DataSource = query.ToList();
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "Delete";
            btn.HeaderText = "Delete";
            btn.DefaultCellStyle.NullValue = "Delete";
            dataGridView1.Columns.Add(btn);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string strPath = @"rubric.xml";
            XElement student = XElement.Load(strPath);
            IEnumerable<XElement> stu = from st in student.Elements("courses")
                                        where (string)st.Attribute("name") == textBox1.Text
                                        select st;
            var query1 = from n in stu.Elements()
                         select new
                         {
                             id = n.Parent.Attribute("id").Value,
                             name = n.Parent.Attribute("name").Value
                         };
            dataGridView1.DataSource = query1.ToList();
            flag = false;
        }
    }
}
