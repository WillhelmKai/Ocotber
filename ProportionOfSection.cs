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

namespace Evaluate
{

    public partial class ProportionOfSection : Form
    {
        String CourseName;
        public ProportionOfSection(String course)
        {
            InitializeComponent();
            CourseName = course;
            textBox1.Text = CourseName;
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
            int col = dataGridView1.Rows[1].Cells.Count;
            int sum = 0, a = 0;

            //check the persentage            
            for (int i =0; i < row; i++) {
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                }
                else {
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
                foreach (XmlNode xn in nodeList) {
                    XmlElement xe = (XmlElement)xn;
                    
                    if (xe.GetAttribute("CourseName") == CourseName) {
                        XmlNodeList nls = xe.ChildNodes;
                        foreach (XmlNode xn1 in nls) {
                            xn1.Attributes[3].Value = dataGridView1.Rows[count].Cells[1].Value.ToString();
                            count++;
                        }
                        
                    }
                }
                xmlDoc.Save("task.xml");
                MessageBox.Show("Saved");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ProportionOfSection_Load(object sender, EventArgs e)
        {

            string xmlpath = @"task.xml";
            XElement root = XElement.Load(xmlpath);
            IEnumerable<XElement> address =
               from el in root.Elements("courses")
               where (string)el.Element("task").Attribute("CourseName")==CourseName
               select el;
           var query = from n in address.Elements()
                       select new
                       {
                           Task = n.Attribute("Task").Value
                       };
           dataGridView1.DataSource = query.ToList();

            DataGridViewTextBoxColumn btn = new DataGridViewTextBoxColumn();
            
            btn.HeaderText = "Precentage";
            
            btn.DefaultCellStyle.NullValue = "0";
            dataGridView1.Columns.Add(btn);
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
