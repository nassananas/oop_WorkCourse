using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Курсовая_работа_ООП
{
    public partial class Filter : Form
    {
        DataGridView dgv;
        public Filter()
        {
            InitializeComponent();
        }

        public Filter(ref DataGridView dgv)
        {
            InitializeComponent();
            this.dgv = dgv;
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> category = new List<string>();
            for (int index = 0; index < checkedListBox1.CheckedItems.Count; index++)
            {
                category.Add(checkedListBox1.CheckedItems[index] + " ");
            }
            string where = "";
            if (category.Count > 0)
            {
                where += $"(category LIKE '%{category[0]}%'";
                for (int i = 1; i<category.Count; i++)
                {
                    where += $" OR category LIKE '%{category[i]}%'";
                }
                where += ") AND ";
            }
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    where += "omission>0 AND omission<5 ";
                    break;
                case 1:
                    where += "omission>5 AND omission<10 ";
                    break;
                case 2:
                    where += "omission>10 ";
                    break;
                default:
                    if (where.Length > 0)
                    {
                        where = where.Substring(0, where.Length - 5);
                    }
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
                where += "exam=1 "; 
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

            string request = DbHelper.select("AutoCourses", "*" , where);
            if (where.Length>0) DbHelper.fillTable(ref dgv, request);
            this.Hide();
        }
    }
}
