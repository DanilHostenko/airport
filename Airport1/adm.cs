using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Airport1
{
    public partial class adm : Form
    {
        public adm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = dataGridView1.SelectedCells[0].RowIndex;
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                id.Text = row.Cells["Column1"].Value.ToString();
                date.Text = row.Cells["Column2"].Value.ToString();
                time.Text = row.Cells["Column3"].Value.ToString();
                route.Text = row.Cells["Column4"].Value.ToString();
                number.Text = row.Cells["Column5"].Value.ToString();
            }
        }

        private void number_TextChanged(object sender, EventArgs e)
        {

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(id.Text,date.Text,time.Text,route.Text,number.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int ind = dataGridView1.SelectedCells[0].RowIndex;
            dataGridView1.Rows.RemoveAt(ind);
        }

        private void adm_Load(object sender, EventArgs e)
        {

        }
        OpenFileDialog ofd = new OpenFileDialog();
       
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Stream mystr = null;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if ((mystr = ofd.OpenFile()) != null)
                {
                    StreamReader myread = new StreamReader(mystr);
                    string[] str;
                    int num = 0;
                    try
                    {
                        string[] str1 = myread.ReadToEnd().Split('\n');
                        num = str1.Count();
                        dataGridView1.RowCount = num;
                        for (int i = 0; i < num; i++)
                        {
                            str = str1[i].Split(' ');
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                try
                                {
                                    dataGridView1.Rows[i].Cells[j].Value = str[j];
                                }
                                catch { }
                            }

                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        myread.Close();
                    }
                }

            }
        }
        SaveFileDialog sfd = new SaveFileDialog();
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = sfd.OpenFile())!= null)
                {
                    StreamWriter myWritet = new StreamWriter(myStream);
                    try
                    {
                        for (int i=0;i<dataGridView1.RowCount -1;i++)
                        {
                            for (int j =0;j<dataGridView1.ColumnCount;j++)
                            {
                                myWritet.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + ' ' );
                            }
                            myWritet.WriteLine();
                                    }
                                }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        myWritet.Close();
                    }
                    myStream.Close();
                }
                        }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
            Form1 fo = new Form1();
            fo.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            
        }
    }
}
