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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = dataGridView1.SelectedCells[0].RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                id.Text = row.Cells["Column1"].Value.ToString();
                date.Text = row.Cells["Column2"].Value.ToString();
                time.Text = row.Cells["Column3"].Value.ToString();
                route.Text = row.Cells["Column4"].Value.ToString();
                number.Text = row.Cells["Column5"].Value.ToString();
            }
        }
        OpenFileDialog ofd = new OpenFileDialog();
        private void button1_Click_1(object sender, EventArgs e)
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
                    catch (Exception ex)
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
        private void button2_Click(object sender, EventArgs e)
        {

            sfd.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            sfd.InitialDirectory = @"c:\Desktop\";
            if (sfd.ShowDialog() == DialogResult.Cancel)
                return;
          
            string filename = sfd.FileName;
          
            string result =
                $"_________________________________________ \n"+
                $"|Id рейса:{id.Text};\n"+
                $"|Дата:{date.Text};\n"+              
                $"|Время:{time.Text};\n"+               
                $"|Маршрут:{route.Text};\n"+          
                $"|Фамилия:{Surname.Text}\n"+
                $"|Имя:{Name2.Text}\n"+
                $"|Счастливого пути!!!\n" +
                $"|________________________________________ ";
            System.IO.File.WriteAllText(filename, result);
            MessageBox.Show("Билет куплен!");

            
        }

        private void Surname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void number_TextChanged(object sender, EventArgs e)
        {

        }

        private void Name2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 autho = new Form1();
            autho.Show();
        }
    }
}
