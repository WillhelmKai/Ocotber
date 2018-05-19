using Evaluate;
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

namespace WindowsFormsApp6
{
    public partial class Gradelist : Form
    {
        public DataGridViewRow dgvr;
        public Gradelist()
        {
            InitializeComponent();
        }

        public Gradelist(DataGridViewRow myDGVR)
        {
            InitializeComponent();
            dgvr = myDGVR;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Gradelist_Load(object sender, EventArgs e)
        {
            string xmlpath = @"gradelist.xml";
            XDocument xdoc = XDocument.Load(xmlpath);
            var query = from n in xdoc.Descendants("grade")
                        select new
                        {
                            id = n.Attribute("id").Value,
                            course = n.Attribute("course").Value,
                            grade = n.Attribute("grade").Value
                        };
            dataGridView1.DataSource = query.ToList();

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Final_grade_page form2 = new Final_grade_page();
            this.Hide();
            form2.ShowDialog();
        }
    }
}
