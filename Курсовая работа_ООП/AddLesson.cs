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
    public partial class AddLesson : Form
    {
        public AddLesson()
        {
            InitializeComponent();
            DataTable dt = DbHelper.fill();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "id";
            }
            comboBox1.KeyPress += (sender, e) => e.Handled = true;
            comboBox2.KeyPress += (sender, e) => e.Handled = true;
            textBox1.Text = "0";
            textBox2.Text = " ";
            comboBox2.SelectedIndex = 0;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length<0)
            {
                textBox1.Text = "0";
            }
            if (textBox2.Text.Length<0)
            {
                textBox2.Text = "-";
            }
            if (DbHelper.insert("Attendance", "pupil_id, name, lesson, date_lesson, mistake, description", $"'{comboBox1.SelectedValue}','{(comboBox1.SelectedItem as DataRowView).Row["name"].ToString()}', '{comboBox2.SelectedItem}', '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}', '{textBox1.Text}', '{textBox2.Text}'"))
            {
                this.Close();
            }
            else MessageBox.Show("Отсутствует подключение к БД");
        }
    }
}
