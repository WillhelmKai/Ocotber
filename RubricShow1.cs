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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Form6 f6 = new Form6(dataGridView1.CurrentRow);
            f6.Show();
            this.Hide();
        }

        private void Form5_Load(object sender, EventArgs e)
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
            btn.Name = "Edit";
            btn.HeaderText = "Edit";
            btn.DefaultCellStyle.NullValue = "Edit";
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
