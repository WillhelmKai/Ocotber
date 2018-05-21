using Evaluate;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApp6
{
    public partial class Gradelist : Form
    {
        public DataGridViewRow dgvr;
        String CourseName;

        public Gradelist(String course)
        {
            InitializeComponent();
            CourseName = course;
          
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

            XElement root1 = XElement.Load("gradelist.xml");
            IEnumerable<XElement> address =
                from el in root1.Elements()
                where ((string)el.Attribute("course")) == CourseName
                select el;


            XElement xmlTree2 = new XElement("UIC",
                from el in root1.Elements("student").Elements("grade")
                where (((string)el.Attribute("course")) == CourseName)
                select el);

            var query = from n in xmlTree2.Elements()
                        select new
                        {
                            //id = n.Attribute("id").Value,
                           // firstname = n.Parent.Attribute("FirstName").Value,
                          //  lastname = n.Attribute("SurName").Value,
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

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl files (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为Excel文件";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Stream myStream;
                myStream = dlg.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
                string columnTitle = "";
                try
                {
                    //写入列标题  
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        if (i > 0)
                        {
                            columnTitle += "\t";
                        }
                        columnTitle += dataGridView1.Columns[i].HeaderText;
                    }
                    sw.WriteLine(columnTitle);

                    //写入列内容  
                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        string columnValue = "";
                        for (int k = 0; k < dataGridView1.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                columnValue += "\t";
                            }
                            if (dataGridView1.Rows[j].Cells[k].Value == null)
                                columnValue += "";
                            else
                                columnValue += dataGridView1.Rows[j].Cells[k].Value.ToString().Trim();
                        }
                        sw.WriteLine(columnValue);
                    }
                    sw.Close();
                    myStream.Close();
                    MessageBox.Show("Export successful!!");
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ///设置导出字体
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            string FontPath = path + "\\simsun.ttc";
            int FontSize = 12;
            if (File.Exists(FontPath))
            {
                FontPath += ",1";
            }
            else
            {
                MessageBox.Show("Wrong!");
                return;
            }


            Boolean cc = false;
            string strFileName;
            SaveFileDialog savFile = new SaveFileDialog();
            savFile.Filter = "PDF文件|.pdf";
            savFile.ShowDialog();
            if (savFile.FileName != "")
            {
                strFileName = savFile.FileName;
            }
            else
            {
                return;
            }

            Document document = new Document(PageSize.A4.Rotate(), 25, 25, 25, 25);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(strFileName, FileMode.Create));

            BaseFont baseFont = BaseFont.CreateFont(
                FontPath,
                BaseFont.IDENTITY_H,
                BaseFont.NOT_EMBEDDED);

            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, FontSize);

            document.Open();

            int ColCount = 0;

            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                if (dataGridView1.Columns[j].Visible == true)
                {
                    ColCount++;
                }
            }
            PdfPTable table = new PdfPTable(ColCount);

           // table.DefaultCell.BackgroundColor = BaseColor.BLUE;
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                if (dataGridView1.Columns[j].Visible == true)
                {
                    table.AddCell(new Phrase(dataGridView1.Columns[j].HeaderText, font));
                }
            }

            table.HeaderRows = 1;

           // table.DefaultCell.BackgroundColor = BaseColor.WHITE;

            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {

                for (int k = 0; k < dataGridView1.Columns.Count; k++)
                {
                    if (dataGridView1.Rows[j].Cells[k].Visible == true)
                    {
                        try
                        {
                            string value = "";
                            if (dataGridView1.Rows[j].Cells[k].Value != null)
                            {
                                value = dataGridView1.Rows[j].Cells[k].Value.ToString();

                            }
                            table.AddCell(new Phrase(value, font));
                        }
                        catch (Exception)
                        {

                            //MessageBox.Show(e.Message);
                            cc = true;
                        }

                    }
                }

            }
            document.Add(table);
            document.Close();
            writer.Close();
            MessageBox.Show("Export successful!!");
        }
    
    }
}
