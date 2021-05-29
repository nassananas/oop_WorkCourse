using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Курсовая_работа_ООП
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (!DbHelper.fillTable(ref dataGridView1))
                MessageBox.Show("Отсутствует подключение к БД");
            else headerText("all");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DbHelper.isConnect())
            {
                Form ifrm = new Attendance();
                ifrm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Отсутствует подключение к БД");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(!DbHelper.deleteDB()) MessageBox.Show("Отсутствует подключение к БД");
            else dataGridView1.DataSource = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(!DbHelper.createDB()) MessageBox.Show("Ошибка создания БД, возможно БД уже создана");
            else { 
                DbHelper.fillTable(ref dataGridView1);
                headerText("all");
                MessageBox.Show("База данных успешно создана");
                button12.Enabled = false;
            }
        }

        public void headerText(string type)
        {
            switch (type)
            {
                case "all":
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "ФИО курсанта";
                    dataGridView1.Columns[2].HeaderText = "Категория";
                    dataGridView1.Columns[3].HeaderText = "Кол-во пропусков";
                    dataGridView1.Columns[4].HeaderText = "Внутренний Теория";
                    dataGridView1.Columns[5].HeaderText = "Внутренний Площадка";
                    dataGridView1.Columns[6].HeaderText = "Внутренний Город";
                    dataGridView1.Columns[7].HeaderText = "Экзамен Теория";
                    dataGridView1.Columns[8].HeaderText = "Экзамен Площадка";
                    dataGridView1.Columns[9].HeaderText = "Экзамен Город";
                    break;
                case "test":
                    dataGridView1.Columns[0].HeaderText = "ФИО курсанта";
                    dataGridView1.Columns[1].HeaderText = "Категория";
                    dataGridView1.Columns[2].HeaderText = "Внутренний Теория";
                    dataGridView1.Columns[3].HeaderText = "Внутренний Площадка";
                    dataGridView1.Columns[4].HeaderText = "Внутренний Город";
                    break;
                case "exam":
                    dataGridView1.Columns[0].HeaderText = "ФИО курсанта";
                    dataGridView1.Columns[1].HeaderText = "Категория";
                    dataGridView1.Columns[2].HeaderText = "Экзамен Теория";
                    dataGridView1.Columns[3].HeaderText = "Экзамен Площадка";
                    dataGridView1.Columns[4].HeaderText = "Экзамен Город";
                    break;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DbHelper.isConnect())
            {
                Form ifrm = new AddLearner();
                ifrm.ShowDialog();
                DbHelper.fillTable(ref dataGridView1);
                headerText("all");
            }
        
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!DbHelper.connect()) MessageBox.Show("Не найдена БД, создайте новую");
            else
            {
                DbHelper.fillTable(ref dataGridView1);
                headerText("all");
                button12.Enabled = false;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Удалить запись {dataGridView1[1, dataGridView1.SelectedCells[0].RowIndex].Value.ToString()} ?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { 
                string id = dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                if(DbHelper.deleteRecord("Attendance", $"pupil_id={id}"))
                if (DbHelper.deleteRecord("AutoCourses", $"id={id}")) MessageBox.Show("Запись успешно удалена");
                else MessageBox.Show("Не удалось удалить запись");
                DbHelper.fillTable(ref dataGridView1);
                headerText("all");
                
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для изменения кликните два раза по ячейке, \nкоторую вы хотите изменить");
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.Moccasin;
        }
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0) e.Cancel = true;
            else if (e.ColumnIndex == 2)
            {
                string id = dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                Form ifrm = new AddLearner("editing_category", id);
                ifrm.ShowDialog();
                DbHelper.fillTable(ref dataGridView1);
                headerText("all");
                e.Cancel = true;
            }

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string values = "";
            int flag;
            values = $"name='{dataGridView1[1, e.RowIndex].Value.ToString()}', omission='{Convert.ToInt32(dataGridView1[3, e.RowIndex].Value.ToString())}'";
            if(e.ColumnIndex > 3)
            {
                if (Convert.ToBoolean(dataGridView1[e.ColumnIndex, e.RowIndex].Value)) flag = 1;
                else flag = 0;
                switch (e.ColumnIndex)
                {
                    case 4:
                        values += $", theory = {flag}";
                        break;
                    case 5:
                        values += $", city = {flag}";
                        break;
                    case 6:
                        values += $", court = {flag}";
                        break;
                    case 7:
                        values += $", theory = {flag}";
                        break;
                    case 8:
                        values += $", exam = {flag}";
                        break;
                    case 9:
                        values += $", exam1 = {flag}";
                        break;
                    case 10:
                        values += $", exam2 = {flag}";
                        break;
                }

            }
            /*if (Convert.ToBoolean(dataGridView1[4, e.RowIndex].Value))
            {
                values += $", theory = 1";
            }
            else
            {
                values += $", theory = 0";
            }
            */

                DbHelper.update("AutoCourses", values, dataGridView1[0, e.RowIndex].Value.ToString());
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!DbHelper.fillTable(ref dataGridView1, DbHelper.select("AutoCourses", "name, category, theory, court, city")))
                MessageBox.Show("Отсутствует подключение к БД");
            else headerText("test");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!DbHelper.fillTable(ref dataGridView1, DbHelper.select("AutoCourses", "name, category, exam1, exam2, exam3")))
                MessageBox.Show("Отсутствует подключение к БД");
            else headerText("exam");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для сортировки просто кликните по названию столбца, \nпо которому вы хотите отсортировать");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form ifrm = new Filter(ref dataGridView1);
            ifrm.ShowDialog();
            if(DbHelper.isConnect()) headerText("all");
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int number;
            bool b;
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                ((DataGridView)sender).CancelEdit();
            switch (e.ColumnIndex)
            {
                case 1:
                    if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                        ((DataGridView)sender).CancelEdit();
                    break;
                case 2:

                    break;
                case 3:
                    if (e.FormattedValue.ToString() != "" && !Int32.TryParse(e.FormattedValue.ToString(), out number))
                    {
                        ((DataGridView)sender).CancelEdit();
                        MessageBox.Show("Неверный формат данных");
                    }
                    break;

            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                dataGridView1.BeginEdit(true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DbHelper.isConnect())
            {
                Form ifrm = new SearchLearner();
                ifrm.ShowDialog();
                int rowIndex = -1;
                try
                {
                    dataGridView1.ClearSelection();
                    DataGridViewRow row = dataGridView1.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["id"].Value.ToString().Equals(SearchLearner.search_id))
                    .First();
                    rowIndex = row.Index;
                    dataGridView1.Rows[rowIndex].Selected = true;
                }
                catch
                {
                    MessageBox.Show("Запись не найдена");
                }
            }

        }
    }
}
