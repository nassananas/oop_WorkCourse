using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая_работа_ООП
{
    public partial class Attendance : Form
    {
        
        public Attendance()
        {
            InitializeComponent();
            DbHelper.fillTable(ref dataGridView1, "SELECT * FROM Attendance");
            headerText("all");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string search_id = "";
                string where = "";
                if (textBox1.Text.Length > 0)
                {
                    where += $"name LIKE '%{textBox1.Text}%'  AND ";
                }
                if (comboBox2.SelectedItem != null)
                {
                    where += $"lesson = '{comboBox2.SelectedItem.ToString()}' AND ";
                }

                where += $"date_lesson = '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}'";

                DataTable dt = DbHelper.fill(DbHelper.select("Attendance", "*", where));
                search_id = dt.Rows[0]["lesson_id"].ToString();

                int rowIndex = -1;
                dataGridView1.ClearSelection();
                DataGridViewRow row = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells["lesson_id"].Value.ToString().Equals(search_id))
                .First();
                rowIndex = row.Index;
                dataGridView1.Rows[rowIndex].Selected = true;
            }
            catch
            {
                MessageBox.Show("Запись не найдена");
            }

            
        }

        public void headerText(string type)
        {
            switch (type)
            {
                case "all":
                    dataGridView1.Columns[0].HeaderText = "id_занятия";
                    dataGridView1.Columns[1].HeaderText = "id_курсанта";
                    dataGridView1.Columns[2].HeaderText = "ФИО курсанта";
                    dataGridView1.Columns[3].HeaderText = "Занятие";
                    dataGridView1.Columns[4].HeaderText = "Дата";
                    dataGridView1.Columns[5].HeaderText = "Количество ошибок";
                    dataGridView1.Columns[6].HeaderText = "Комментарии (ошибки)";
                    break;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DbHelper.isConnect())
            {
                Form ifrm = new AddLesson();
                ifrm.ShowDialog();
                dataGridView1.DataSource = DbHelper.fill("SELECT * FROM Attendance");
                headerText("all");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Удалить запись {dataGridView1[2, dataGridView1.SelectedCells[0].RowIndex].Value.ToString()} ?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string id = dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                if (DbHelper.deleteRecord("Attendance", $"lesson_id={id}")) MessageBox.Show("Запись успешно удалена");
                else MessageBox.Show("Не удалось удалить запись");
                dataGridView1.DataSource = DbHelper.fill("SELECT * FROM Attendance");
                headerText("all");
            }
        }
    }
}
