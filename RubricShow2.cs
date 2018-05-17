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
            name = dgvr.Cells[0].Value.ToString();
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
            /* XmlDocument xdoc = new XmlDocument();
             xdoc.Load("rubric.xml");
             string path = "rubric.xml//courses [@id='" + name + " ']";
             XmlNodeList nodelist = xdoc.SelectNode(path);*/
            //string path = "//courses [@id='" + name + " ']//task";
            string path = "rubric.xml";
            DataSet myds = new DataSet();
            myds.ReadXml(path);
            dataGridView1.DataSource = myds.Tables[1];
            // string path = "rubric.xml";
            /*  XDocument xdoc = XDocument.Load(path);
              var query = from n in xdoc.Descendants("task")
                          select new
                          {

                              //RubricName = name,
                              id =n.Attribute("row").Value,
                              Criteria = n.Attribute("Item").Value,
                              Percentage = n.Attribute("Percentage").Value,
                              Excellent = n.Attribute("A").Value,
                              Good = n.Attribute("B").Value,
                              Satisfy = n.Attribute("C").Value,
                              Pass = n.Attribute("D").Value,
                              Fail = n.Attribute("F").Value


                          };
              dataGridView1.DataSource = query.ToList();*/

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
            this.Hide();
        }
    }
}
