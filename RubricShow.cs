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
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            string xmlpath = @"rubric.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlpath);
            XmlNode root = xmlDoc.SelectSingleNode("UIC");
            XmlNodeList xnList = root.ChildNodes;
            foreach(XmlNode xn in xnList)
            {
                if (int.Parse(xn.Attributes[0].Value)== this.dataGridView1.CurrentRow.Index+1)
                {
                   
                    root.RemoveChild(xn);
                    
                }
            }
            xmlDoc.Save(xmlpath);
            XmlNodeList xnList2 = root.ChildNodes;
            foreach (XmlNode xn in xnList2)
            {
                if (int.Parse(xn.Attributes[0].Value) > this.dataGridView1.CurrentRow.Index + 1)
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
    }
}
