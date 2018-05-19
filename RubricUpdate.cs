using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace WindowsFormsApp3
{
    public partial class Form7 : Form
    {
        public bool text = true;
        public bool grade = true;
        public string name;
        public DataGridViewRow dgvr;
        public Form7()
        {
            InitializeComponent();
        }

        public Form7(DataGridViewRow myDGVR)
        {
            InitializeComponent();
            dgvr = myDGVR;
            name = dgvr.Cells[0].Value.ToString();
            getData();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            text = true;
            grade = true;
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("You should enter content in the textbox");
                text = false;
            }
            string SuID = textBox8.Text.ToString();
            Regex reg = new Regex("^[0-9]+$");
            Match ma = reg.Match(SuID);
            if (!ma.Success)
            {
                MessageBox.Show("You should enter integer in percentage");
                text = false;
            }
            if (text)
            {
                if (Convert.ToInt32(textBox8.Text.ToString()) > 100)
                {
                    MessageBox.Show("You shouldn't enter integer in percentage excceed 100");
                    grade = false;
                }

            }
            if (text == true && grade == true)
            {
                updateXML(float.Parse(dgvr.Cells[1].Value.ToString()), textBox1.Text, textBox8.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);
                this.Close();
                Form6 f6 = new Form6(name);
                f6.Show();
            }

        }

        public void getData()                           //给控件赋值
        {

            textBox1.Text = dgvr.Cells[2].Value.ToString();
            textBox2.Text = dgvr.Cells[4].Value.ToString();
            textBox3.Text = dgvr.Cells[5].Value.ToString();
            textBox4.Text = dgvr.Cells[6].Value.ToString();
            textBox5.Text = dgvr.Cells[7].Value.ToString();
            textBox6.Text = dgvr.Cells[8].Value.ToString();
            textBox8.Text = dgvr.Cells[3].Value.ToString();
            //textBox7.Text = dgvr.Cells[0].Value.ToString();
        }

        public void updateXML(float id, string Criteria, string Percentage, string Excellent, string Good, string Satisfactory, string Pass, string Fail)
        {
            int i = 0;
            XmlDocument doc = new XmlDocument();

            doc.Load("rubric.xml");
            string path1 = "//task";
            XmlNodeList node1 = doc.SelectNodes(path1);
            while (node1.Item(i) != null)
            {

                if (float.Parse(node1.Item(i).Attributes[0].Value) == id)
                {

                    node1.Item(i).Attributes[1].Value = Criteria;
                    node1.Item(i).Attributes[2].Value = Percentage;
                    node1.Item(i).Attributes[3].Value = Excellent;
                    node1.Item(i).Attributes[4].Value = Good;
                    node1.Item(i).Attributes[5].Value = Satisfactory;
                    node1.Item(i).Attributes[6].Value = Pass;
                    node1.Item(i).Attributes[7].Value = Fail;
                }
                i++;
            }

            doc.Save("rubric.xml");



        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();

        }
    }
}
