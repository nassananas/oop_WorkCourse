using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace Курсовая_работа_ООП
{
    
    static class DbHelper
    {
        private static string config = "datasource=127.0.0.1;port=3306;username=root;password=;SslMode=None;";
        private static string dbName = "oop_kp";
        public static bool connect()
        {
            try
            {
                string request = "USE " + dbName;
                MySqlConnection connect = new MySqlConnection(config);
                connect.Open();
                MySqlCommand command = new MySqlCommand(request, connect);
                command.ExecuteNonQuery();
                connect.Close();
                config += "Database=" + dbName;
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool isConnect()
        {
              try
              {
                  string request = "SELECT * FROM AutoCourses";
                  MySqlConnection connect = new MySqlConnection(config);
                  connect.Open();
                  MySqlCommand command = new MySqlCommand(request, connect);
                  command.ExecuteNonQuery();
                  connect.Close();
                  return true;
              }
              catch
              {
                  return false;
              }

        }

        public static bool createDB()
        {
            try
            {
                string request = "CREATE DATABASE " + dbName;
                MySqlConnection connect = new MySqlConnection(config);
                connect.Open();
                MySqlCommand command = new MySqlCommand(request, connect);
                command.ExecuteNonQuery();
                config += "Database=" + dbName;
                connect.Close();
                connect = new MySqlConnection(config);
                connect.Open();
                request = "CREATE TABLE AutoCourses(id INT NOT NULL primary key AUTO_INCREMENT, name varchar(255) NOT NULL, category varchar(255), omission INT, theory BOOL, court BOOL, city BOOL, exam1 BOOL, exam2 BOOL, exam3 BOOL);";
                command = new MySqlCommand(request, connect);
                command.ExecuteNonQuery();
                request = "CREATE TABLE Attendance(lesson_id INT NOT NULL primary key AUTO_INCREMENT, pupil_id INT, name varchar(255), lesson varchar(255), date_lesson DATE, mistake INT, description varchar(255), FOREIGN KEY (pupil_id)  REFERENCES AutoCourses (id))";
                command = new MySqlCommand(request, connect);
                command.ExecuteNonQuery();
                connect.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool deleteDB()
        {
            try
            {
                string request = "DROP DATABASE " + dbName;
                MySqlConnection connect = new MySqlConnection(config);
                connect.Open();
                MySqlCommand cmd = new MySqlCommand(request, connect);
                cmd.ExecuteNonQuery();
                connect.Close();
                config = config.Replace("Database=oop_kp", "");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string select(string table, string col = "*", string where = null)
        {
            string request = "SELECT " + col + " FROM " + table;
            if (where != null)
            {
                request += " WHERE " + where;
            }
            return request;
        }

        public static bool insert(string table, string col, string values)
        {
            try
            {
                string request = "INSERT INTO " + table + "(" + col + ") VALUES(" + values + ")";
                MySqlConnection connect = new MySqlConnection(config);
                connect.Open();
                MySqlCommand cmd = new MySqlCommand(request, connect);
                cmd.ExecuteNonQuery();
                connect.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool update(string table, string col, string id)
        {
            try
            {
                string request = "UPDATE " +table+ " SET "+ col + " WHERE id=" + id;
                MySqlConnection connect = new MySqlConnection(config);
                connect.Open();
                MySqlCommand cmd = new MySqlCommand(request, connect);
                cmd.ExecuteNonQuery();
                connect.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool fillTable(ref DataGridView dgv, string request = "SELECT * from AutoCourses")
        {
            try
            {
                MySqlConnection connect = new MySqlConnection(config);
                connect.Open();
                MySqlDataAdapter ms_data = new MySqlDataAdapter(request, config);
                DataTable table = new DataTable();
                ms_data.Fill(table);
                dgv.DataSource = null;
                dgv.DataSource = table;
                connect.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DataTable fill(string request = "SELECT * from AutoCourses")
        {
           // try
           // {
                MySqlConnection connect = new MySqlConnection(config);
                connect.Open();
                MySqlDataAdapter ms_data = new MySqlDataAdapter(request, config);
                DataTable table = new DataTable();
                ms_data.Fill(table);
                connect.Close();
                return table;
            /*}
            catch
            {
                return table;
            }*/
        }


        public static bool deleteRecord(string table, string where)
        {
            try
            {
                string request = "DELETE from " + table + " WHERE "+ where;
                MySqlConnection connect = new MySqlConnection(config);
                connect.Open();
                MySqlCommand command = new MySqlCommand(request, connect);
                command.ExecuteNonQuery();
                connect.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
