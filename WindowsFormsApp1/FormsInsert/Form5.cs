using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.FormsInsert
{
    public partial class Form5 : Form
    {
        private WindowsFormsApp1.Form4 elder;
        public Form5(WindowsFormsApp1.Form4 test)
        {
            elder = test;
            elder.Hide();
            InitializeComponent();
        }
        private bool empty = false;
        private static string error = "Бд не приняла ваш запрос на добавление нового объекта. Проверьте - может сначала вам нужно добавить записи в другие таблицы?";
        private static string errorBox = "Извините, во время вашего запроса к базе данных произошла какая-то ошибка:\n";
        private static string errorBool = "Данные, что вы ввели не прошли проверку на корректность.\r\n Проверьте на отстутствие запрещённых слов и соответствие типам.";
        private Point last;

        private bool checkMeString(string dateSr)
        {
            string date = dateSr.ToLower();
            if (!date.Contains("select") && !date.Contains("union") && !date.Contains("drop") && !date.Contains("update") && !date.Contains("delete"))
            {
                if (date != "")
                {
                    return true;
                }
                else
                {
                    empty = true;
                    return false;
                }
            }
            else return false;
        }
        private bool checkMeInt(string data)
        {
            if (data == "")
            {
                empty = true;
                return false;
            }
            else
            {
                int maxPrice;
                bool isNumeric = int.TryParse(data, out maxPrice);
                return isNumeric;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(elder);
            form4.ShowDialog();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            elder.Show();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {   
                string plane_id = textBox20.Text;
                string cost_eco = textBox18.Text;
                string cost_elite = textBox15.Text;
                string time_start = textBox13.Text;
                string time_end = textBox22.Text;
                string first_country = textBox1.Text;
                string destination = textBox3.Text;
                string captain_id = textBox5.Text;
                string alt_cap_id = textBox7.Text;
                string steward_id = textBox9.Text;
                string steward_id_2 = textBox11.Text;
                bool test1 = checkMeInt(plane_id);
                bool test2 = checkMeInt(cost_eco);
                bool test3 = checkMeInt(cost_elite);
                bool test4 = checkMeString(time_start);
                bool test5 = checkMeString(time_end);
                bool test6 = checkMeString(first_country);
                bool test7 = checkMeString(destination);
                bool test8 = checkMeInt(captain_id);
                bool test9 = checkMeInt(alt_cap_id);
                bool test10 = checkMeInt(steward_id);
                bool test11 = checkMeInt(steward_id_2);
                if (test1 && test2 && test3 && test4 && test5 && test6 && test7 && test8 && test9 && test10 && test11)
                {
                    if (empty == true)
                    {
                        empty = false;
                        MessageBox.Show("Все поля должны быть заполнены.");
                    }
                    else
                    {
                        OdbcConnection con = new OdbcConnection("DSN=Connect");
                        con.Open();
                        using (OdbcCommand com = new OdbcCommand("insert into flights(plane_id, cost_economic, cost_elite, time_start, time_end, first_country , destination, captain_id, alter_captain_id, steward_1_id, steward_2_id) values("+plane_id+", "+cost_eco+", "+cost_elite+", '"+time_start+"', '"+time_end+"', '"+first_country+"', '"+destination+"', "+captain_id+", "+alt_cap_id+", "+steward_id+", "+steward_id_2+")", con))
                        {
                            com.ExecuteNonQuery();
                        }
                        con.Close();
                        More more = new More(this, elder);
                        more.Show();
                    }
                }
                else
                {
                    if (empty == true)
                    {
                        MessageBox.Show("Все поля должны быть заполнены.");
                        empty = false;
                    }
                    MessageBox.Show(errorBool);
                    if (!test6)
                    {
                        MessageBox.Show("Таких профессий в аэропорту нет.");
                    }
                }
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
                MessageBox.Show(error);
                button3.Visible = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                last = MousePosition;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point cur = MousePosition;
                int dx = cur.X - last.X;
                int dy = cur.Y - last.Y;
                Point loc = new Point(Location.X + dx, Location.Y + dy);
                Location = loc;
                last = cur;
            }
        }
    }
}
