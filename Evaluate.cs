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

namespace Evaluate
{
    public partial class Evaluate : Form
    {
        public Evaluate()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Evaluate_Load(object sender, EventArgs e)
        {
            //hello world again
            //Hello World
            string xmlpath = @"task.xml";
            XDocument xdoc = XDocument.Load(xmlpath);
            var query = from n in xdoc.Descendants("task")
                        select new
                        {
                            CourseName = n.Attribute("CourseName").Value,
                            Task = n.Attribute("Task").Value,
                            Rubric = n.Attribute("Rubric").Value
                        };
            dataGridView1.DataSource = query.ToList();
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "evaluate";
            btn.HeaderText = "evaluate";
            btn.DefaultCellStyle.NullValue = "evaluate";
            dataGridView1.Columns.Add(btn);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            mark f2 = new mark(dataGridView1.CurrentRow);
            f2.Show();
            this.Hide();
        }
    }
}
