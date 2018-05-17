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
using System.IO;
using System.Xml.Linq;
namespace WindowsFormsApp3
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public bool flag;

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("rubric.xml");
            XmlNode node = doc.SelectSingleNode("//UIC");
            XmlNodeList xnList = node.ChildNodes;
            foreach (XmlNode xn in xnList)
            {
                if (xn.Attributes[1].Value.Equals(textBox1.Text))
                {
                    flag = true;
                }
                
            }


            if (flag)
            {
                Form5 frm5 = new Form5();
                this.Hide();  //use the Hide method to hide the form1 when form2 is shown
                frm5.ShowDialog();
            }
            else
            {
                Form2 frm2 = new Form2(textBox1.Text);
                this.Hide();  //use the Hide method to hide the form1 when form2 is shown
                frm2.ShowDialog();
               
            }
               

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
