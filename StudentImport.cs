using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp6
{
    public partial class StudentImport : Form
    {
        public StudentImport()
        {
            InitializeComponent();
        }

        private void Import_Load(object sender, EventArgs e)
        {
        }

        public DataSet ImportExcel(string filePath)
        {
            DataSet ds = null;
            DataTable dt = null;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            Microsoft.Office.Interop.Excel.Sheets sheets = null;
            Microsoft.Office.Interop.Excel.Range range = null;
            object missing = System.Reflection.Missing.Value;

            try
            {
                if (excel == null)
                {
                    return null;
                }

                //打开 Excel 文件
                workbook = excel.Workbooks.Open(filePath, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

                //获取所有的 sheet 表
                sheets = workbook.Worksheets;

                ds = new DataSet();

                for (int i = 1; i <= sheets.Count; i++)
                {
                    //获取第一个表
                    worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(i);

                    int rowCount = worksheet.UsedRange.Rows.Count;
                    int colCount = worksheet.UsedRange.Columns.Count;

                    int rowIndex = 1;   //起始行为 1
                    int colIndex = 1;   //起始列为 1

                    DataColumn dc;
                    dt = new DataTable();
                    dt.TableName = "table" + i.ToString();

                    //读取列名
                    for (int j = 0; j < colCount; j++)
                    {
                        range = worksheet.Cells[rowIndex, colIndex + j];

                        dc = new DataColumn();
                        dc.DataType = Type.GetType("System.String");
                        dc.ColumnName = range.Text.ToString().Trim();

                        //添加列
                        dt.Columns.Add(dc);
                    }

                    //读取行数据
                    for (int k = 1; k < rowCount; k++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int l = 0; l < colCount; l++)
                        {
                            range = worksheet.Cells[rowIndex + k, colIndex + l];

                            //使用 range.Value.ToString(); 或 range.Value2.ToString(); 或 range.Text.ToString(); 都可以获取单元格的值
                            dr[l] = range.Text.ToString();
                        }
                        dt.Rows.Add(dr.ItemArray);
                    }

                    ds.Tables.Add(dt);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                workbook.Close();

                //关闭退出
                excel.Quit();

                //释放 COM 对象
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(excel);

                worksheet = null;
                workbook = null;
                excel = null;

                GC.Collect();
            }

            return ds;
        }


        public XmlAttribute CreateAttribute(XmlNode node, string attributeName, string value)
        {
            try
            {
                XmlDocument doc = node.OwnerDocument;
                XmlAttribute attr = null;
                attr = doc.CreateAttribute(attributeName);
                attr.Value = value;
                node.Attributes.SetNamedItem(attr);
                return attr;
            }
            catch (Exception err)
            {
                string desc = err.Message;
                return null;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog filedialog = new OpenFileDialog();
            string FileName = "";

            if (filedialog.ShowDialog() == DialogResult.OK)
            {
                FileName = filedialog.FileName;
                
                dataGridView1.DataSource = ImportExcel(FileName).Tables[0];

            }
        }

        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Student form4 = new Student();
            this.Hide();
            form4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                MessageBox.Show("Please input the course name!");
            }
            else {

                    if (File.Exists("student.xml"))
                    {
                     int j = 0;
                      XmlDocument myxml = new XmlDocument();
                      myxml.Load("student.xml");
                      XmlNode demo = myxml.DocumentElement;
                    foreach (XmlNode node in demo.ChildNodes)
                    {
                        if (textBox7.Text == node.Attributes[0].Value)
                            j = 1;
                    }
                    if (j == 0)
                    {

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load("student.xml");
                        XmlNode root = xmlDoc.SelectSingleNode("Students");

                        int i = 1;

                        for (i = 0; i < dataGridView1.RowCount; i++)
                        {
                            String a = dataGridView1[0, i].EditedFormattedValue.ToString();
                            String b = dataGridView1[1, i].EditedFormattedValue.ToString();
                            String c = dataGridView1[2, i].EditedFormattedValue.ToString();
                            String d = dataGridView1[3, i].EditedFormattedValue.ToString();
                            XmlNode node1 = xmlDoc.CreateElement("student", null);
                            node1.Attributes.Append(CreateAttribute(node1, "Course", textBox7.Text));
                            root.AppendChild(node1);
                            XmlNode node2 = xmlDoc.CreateElement("task", null);
                            node2.Attributes.Append(CreateAttribute(node2, "SurName", a));
                            node2.Attributes.Append(CreateAttribute(node2, "FirstName", b));
                            node2.Attributes.Append(CreateAttribute(node2, "ID", c));
                            node2.Attributes.Append(CreateAttribute(node2, "Group", d));
                            node1.AppendChild(node2);
                        }

                        xmlDoc.Save("student.xml");
                        MessageBox.Show("This sheet has been added successful! ");
                    }

                    else
                    {
                        MessageBox.Show("This DepartMent has already exist! ");
                    }
                }
                else
                {
                    
                    XmlDocument xmlDoc = new XmlDocument();

                    //  
                    XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
                    xmlDoc.AppendChild(node);
                    //
                    XmlNode root = xmlDoc.CreateElement("Students");
                    xmlDoc.AppendChild(root);
                    int i;
                    
                    for (i = 0; i < dataGridView1.RowCount; i++)
                    {
                        String a = dataGridView1[0, i].EditedFormattedValue.ToString();
                        String b = dataGridView1[1, i].EditedFormattedValue.ToString();
                        String c = dataGridView1[2, i].EditedFormattedValue.ToString();
                        String d = dataGridView1[3, i].EditedFormattedValue.ToString();
                        XmlNode node1 = xmlDoc.CreateElement("student", null);
                        node1.Attributes.Append(CreateAttribute(node1, "Course", textBox7.Text));
                        root.AppendChild(node1);
                        XmlNode node2 = xmlDoc.CreateElement("task", null);
                        node2.Attributes.Append(CreateAttribute(node2, "SurName", a));
                        node2.Attributes.Append(CreateAttribute(node2, "FirstName", b));
                        node2.Attributes.Append(CreateAttribute(node2, "ID", c));
                        node2.Attributes.Append(CreateAttribute(node2, "Group", d));
                        node1.AppendChild(node2);
                    }
                    xmlDoc.Save("student.xml");
                    MessageBox.Show("This sheet has been added successful! ");


                }

            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
