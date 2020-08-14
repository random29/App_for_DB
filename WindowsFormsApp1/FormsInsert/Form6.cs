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
    public partial class Form6 : Form
    {
        private WindowsFormsApp1.Form4 elder;
        public Form6(WindowsFormsApp1.Form4 test)
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

        private void button2_Click(object sender, EventArgs e)
        {
            elder.Show();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string id_person = textBox1.Text;
                string id_flight = textBox7.Text;
                string weight = textBox5.Text;
                string color = textBox3.Text;
                bool test1 = checkMeInt(id_person);
                bool test2 = checkMeInt(id_flight);
                bool test3 = checkMeInt(weight);
                bool test4 = checkMeString(color);
                if (test1 && test2 && test3 && test4)
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
                        using (OdbcCommand com = new OdbcCommand("insert into luggage(id_person, id_flight, weight, color) values ("+id_person+", "+id_flight+", "+weight+", '"+color+"')", con))
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
                        empty = false;
                        MessageBox.Show("Все поля должны быть заполнены.");
                    }
                    MessageBox.Show(errorBool);
                }
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
                MessageBox.Show(error);
                button3.Visible = true;
                button4.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(elder);
            form3.ShowDialog();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {

            Form5 form5 = new Form5(elder);
            form5.ShowDialog();
            
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
