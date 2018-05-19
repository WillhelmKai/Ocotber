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
    public partial class Form6 : Form
    {
        public DataGridViewRow dgvr;
        public String name;
        public Form6()
        {
            InitializeComponent();

        }

        public Form6(DataGridViewRow myDGVR)
        {
            InitializeComponent();
            dgvr = myDGVR;
            name = dgvr.Cells[1].Value.ToString();
        }

        public Form6(string updatename)
        {
            InitializeComponent();
            name = updatename;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Form7 f7 = new Form7(dataGridView1.CurrentRow);
            f7.Show();
            this.Hide();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
          
            string strPath = @"rubric.xml";
            XElement student = XElement.Load(strPath);
            IEnumerable<XElement> stu = from st in student.Elements("courses")
                                        where (string)st.Attribute("name") == name
                                        select st;
            var query1 = from n in stu.Elements()
                         select new
                         {
                             name = name,
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

        private void button1_Click(object sender, EventArgs e)
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
                        gradesum = gradesum + Convert.ToInt32(xn1.Attributes[2].Value);

                    }

                }
            }

            if (gradesum == 100)
            {
                Form5 f5 = new Form5();
                f5.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("You should make total percentage as 100");
            }
        }
    }
}
