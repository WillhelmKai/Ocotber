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
    public partial class EVA : Form
    {
        public EVA()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            mark f2 = new mark(dataGridView1.CurrentRow);
            f2.Show();
            this.Hide();
        }

        private void EVA_Load(object sender, EventArgs e)
        {
            string xmlpath = @"task.xml";
            XDocument xdoc = XDocument.Load(xmlpath);
            var query = from n in xdoc.Descendants("task")
                        select new
                        {
                            CourseName = n.Attribute("CourseName").Value,
                            Task = n.Attribute("Task").Value,
                            Rubric = n.Attribute("Rubric").Value,
                            Group = n.Attribute("Group").Value,
                            finish = n.Attribute("finish").Value
                            
                        };
            dataGridView1.DataSource = query.ToList();
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "evaluate";
            btn.HeaderText = "evaluate";
            btn.DefaultCellStyle.NullValue = "evaluate";
            dataGridView1.Columns.Add(btn);
        }
    }
}
