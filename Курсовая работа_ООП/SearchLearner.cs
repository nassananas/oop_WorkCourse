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
    public partial class SearchLearner : Form
    {
        public static string search_id = "";
        public SearchLearner()
        {
            InitializeComponent();
        }
        public static string get_id()
        {
            return search_id;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string where = "";
                if (textBox1.Text.Length > 0)
                {
                    where += $"name LIKE '%{textBox1.Text}%' ";
                }
                if (checkedListBox1.CheckedItems.Count > 0)
                {
                    where += "AND ";
                    List<string> category = new List<string>();
                    for (int index = 0; index < checkedListBox1.CheckedItems.Count; index++)
                    {
                        category.Add(checkedListBox1.CheckedItems[index] + " ");
                    }
                    where += $"(category LIKE '%{category[0]}%'";
                    for (int i = 1; i < category.Count; i++)
                    {
                        where += $" AND category LIKE '%{category[i]}%'";
                    }
                    where += ") ";
                }
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        if (where.Length > 1) where += "AND ";
                        where += "omission>0 AND omission<5 ";
                        break;
                    case 1:
                        if (where.Length > 1) where += "AND ";
                        where += "omission>5 AND omission<10 ";
                        break;
                    case 2:
                        if (where.Length > 1) where += "AND ";
                        where += "omission>10 ";
                        break;
                }
                if (checkBox1.Checked)
                {
                    if (where.Length > 1) where += "AND ";
                    where += "theory=1 ";
                }
                if (checkBox2.Checked)
                {
                    if (where.Length > 1) where += "AND ";
                    where += "court=1 ";
                }
                if (checkBox3.Checked)
                {
                    if (where.Length > 1) where += "AND ";
                    where += "city=1 ";
                }
                if (checkBox4.Checked)
                {
                    if (where.Length > 1) where += "AND ";
                    where += "exam1=1 ";
                }
                if (checkBox5.Checked)
                {
                    if (where.Length > 1) where += "AND ";
                    where += "exam2=1 ";
                }
                if (checkBox6.Checked)
                {
                    if (where.Length > 1) where += "AND ";
                    where += "exam3=1 ";
                }
                DataTable dt = DbHelper.fill(DbHelper.select("AutoCourses", "*", where));
                search_id = dt.Rows[0]["id"].ToString();
                this.Close();
            }
            catch
            {
                search_id = "";
                this.Close();
            }
        }
    }
}
