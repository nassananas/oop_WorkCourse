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
    public partial class AddLearner : Form
    {
        string mode;
        string id;
        public AddLearner()
        {
            InitializeComponent();
        }

        public AddLearner(string mode, string id)
        {
            InitializeComponent();
            this.mode = mode;
            this.Text = "Добавление";
            this.id = id;
            label1.Visible = false;
            textBox1.Visible = false;
            button6.Text = "Сохранить";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (mode == "editing_category")
            {
                if (checkedListBox1.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Заполните поля!");
                    return;
                }
                string category = "";
                for (int index = 0; index < checkedListBox1.CheckedItems.Count; index++)
                {
                    category += checkedListBox1.CheckedItems[index] + " ";
                }
                if (DbHelper.update("AutoCourses", $"category='{category}'", id)) this.Hide();
                else MessageBox.Show("Отсутствует подключение к БД");
            }
            else
            {
                if (textBox1.Text == "" || checkedListBox1.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Заполните поля!");
                    return;
                }
                string category = "";
                for (int index = 0; index < checkedListBox1.CheckedItems.Count; index++)
                {
                    category += checkedListBox1.CheckedItems[index] + " ";
                }
                if (DbHelper.insert("AutoCourses", "name, category, omission, theory, city, court, exam1, exam2, exam3", $"'{textBox1.Text}','{category}', '0', '0', '0', '0', '0', '0', '0' ")) 
                {
                    this.Close();
                }
                else MessageBox.Show("Отсутствует подключение к БД");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }
    }
}
