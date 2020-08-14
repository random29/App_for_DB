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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1(bool admin, Form2 form2)
        {
            this.admin = admin;
            form2.Hide();
            InitializeComponent();
        }
        public Form1(bool admin, Form3 form3)
        {
            form3.Hide();
            this.admin = admin;
            InitializeComponent();
        }

        private static string insufficientRights = "Извините, у вас недостаточно прав для выполнения этого действия. Залогиньтесь в админ-запись.";
        private static string success = "Ваш запрос успешно выполнен";
        private static string buttonAdd = " в кнопке ";
        private static string error = "Во время вашего запроса возникли ошибки. Возможно вы использовали запрещённые слова или ввели данные не того типа. Проверьте введённые данные ";
        private static string[] staffProfString = { "Капитан", "Помощник Капитана", "Стюард" };
        private static string noProf = "И вообще - похоже той профессии, что вы ввели просто нет в аэропорту.";
        private static string errorBox = "Извините, во время вашего запроса к базе данных произошла какая-то ошибка:\n";
        private bool admin = false;
        private Point last;

        private bool staffProf(string inLine)
        {
            for (int num = 0; num < staffProfString.Length; num++)
            {
                if (inLine.Equals(staffProfString[num]))
                {
                    return true;
                }           
            }
            return false;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string maxPriceString = textBox1.Text;
                int maxPrice;
                bool isNumeric = int.TryParse(maxPriceString, out maxPrice);
                if (isNumeric)
                {
                    OdbcConnection con = new OdbcConnection("DSN=Connect");
                    con.Open();
                    OdbcDataAdapter ask = new OdbcDataAdapter("select id_flight , plane_id, cost_economic, cost_elite from flights where cost_elite < " + maxPriceString, con);
                    DataSet ds = new DataSet();
                    ask.Fill(ds);
                    con.Close();
                    dataGridView1.DataSource = ds.Tables[0];
                    textBox3.Text = success + buttonAdd + "'" + button1.Text + "'";
                }
                else
                {
                    dataGridView1.DataSource = null;
                    textBox3.Text = error + buttonAdd + "'" + button1.Text + "'";
                }
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
         
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string dateSr = textBox4.Text;
                string date = dateSr.ToLower();
                string maxPriceString = textBox5.Text;
                int maxPrice;
                bool isNumeric = int.TryParse(maxPriceString, out maxPrice);
                if (isNumeric && !date.Contains("select") && !date.Contains("union") && !date.Contains("drop") && !date.Contains("update") && !date.Contains("delete"))
                {
                    OdbcConnection con = new OdbcConnection("DSN=Connect");
                    con.Open();
                    OdbcDataAdapter ask = new OdbcDataAdapter("(select first_country, destination, cost_economic from flights where time_start > '" + dateSr + "') union (select first_country, destination, cost_economic from flights where cost_economic < " + maxPriceString+ ")", con);
                    DataSet ds = new DataSet();
                    ask.Fill(ds);
                    con.Close();
                    dataGridView1.DataSource = ds.Tables[0];
                    textBox3.Text = success + buttonAdd + "'" + button2.Text + "'";
                }
                else
                {
                    dataGridView1.DataSource = null;
                    textBox3.Text = error + buttonAdd + "'" + button2.Text + "'";
                }
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                    OdbcConnection con = new OdbcConnection("DSN=Connect");
                    con.Open();
                    OdbcDataAdapter ask = new OdbcDataAdapter("(select * from luggage left join passengers on luggage.id_person = passengers.id_pass) union (select * from luggage right join passengers on luggage.id_person = passengers.id_pass)", con);
                    DataSet ds = new DataSet();
                    ask.Fill(ds);
                    con.Close();
                    dataGridView1.DataSource = ds.Tables[0];
                    textBox3.Text = success + buttonAdd + "'" + button3.Text + "'";
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string nameRes = textBox6.Text;
                string name = nameRes.ToLower();
                if(!name.Contains("select") && !name.Contains("union") && !name.Contains("drop") && !name.Contains("update") && !name.Contains("delete"))
                {
                    OdbcConnection con = new OdbcConnection("DSN=Connect");
                    con.Open();
                    OdbcDataAdapter ask = new OdbcDataAdapter("(select passport_id, name, surname from passengers where surname like '"+ nameRes + "') union (select passport_id, name, surname from staff where surname like '"+ nameRes + "')", con);
                    DataSet ds = new DataSet();
                    ask.Fill(ds);
                    con.Close();
                    dataGridView1.DataSource = ds.Tables[0];
                    textBox3.Text = success + buttonAdd + "'" + button4.Text + "'";
                }
                else
                {
                    dataGridView1.DataSource = null;
                    textBox3.Text = error + buttonAdd + "'" + button4.Text + "'";
                }
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                OdbcConnection con = new OdbcConnection("DSN=Connect");
                con.Open();
                OdbcDataAdapter ask = new OdbcDataAdapter("select surname from passengers intersect select surname from staff", con);
                DataSet ds = new DataSet();
                ask.Fill(ds);
                con.Close();
                dataGridView1.DataSource = ds.Tables[0];
                textBox3.Text = success + buttonAdd + "'" + button5.Text + "'";
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string dateSr = textBox7.Text;
                string date = dateSr.ToLower();
                string maxPriceString = textBox8.Text;
                int maxPrice;
                bool isNumeric = int.TryParse(maxPriceString, out maxPrice);
                bool checkProf = staffProf(dateSr);
                if (checkProf && isNumeric && !date.Contains("select") && !date.Contains("union") && !date.Contains("drop") && !date.Contains("update") && !date.Contains("delete"))
                {
                    OdbcConnection con = new OdbcConnection("DSN=Connect");
                    con.Open();
                    OdbcDataAdapter ask = new OdbcDataAdapter("select * from (select * from staff where salary > " + maxPriceString + " intersect select * from staff where position = '"+ dateSr +"') as test order by name", con);
                    DataSet ds = new DataSet();
                    ask.Fill(ds);
                    con.Close();
                    dataGridView1.DataSource = ds.Tables[0];
                    textBox3.Text = success + buttonAdd + "'" + button6.Text + "'";
                }
                else
                {
                    dataGridView1.DataSource = null;
                    textBox3.Text = error + buttonAdd + "'" + button6.Text + "'\r\n";
                    if (!checkProf)
                    {
                        dataGridView1.DataSource = null;
                        textBox3.Text = textBox3.Text + "\n" + noProf;
                    }
                }
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string dateSr = textBox9.Text;
                string date = dateSr.ToLower();
                if (!date.Contains("select") && !date.Contains("union") && !date.Contains("drop") && !date.Contains("update") && !date.Contains("delete"))
                {
                    OdbcConnection con = new OdbcConnection("DSN=Connect");
                    con.Open();
                    OdbcDataAdapter ask = new OdbcDataAdapter("select * from (select id_plane from planes1 except select plane_id from flights where time_start > '"+ dateSr +"') as test ", con);
                    DataSet ds = new DataSet();
                    ask.Fill(ds);
                    con.Close();
                    dataGridView1.DataSource = ds.Tables[0];
                    textBox3.Text = success + buttonAdd + "'" + button7.Text + "'";
                }
                else
                {
                    dataGridView1.DataSource = null;
                    textBox3.Text = error + buttonAdd + "'" + button7.Text + "'";
                }
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string dateSr = textBox10.Text;
                string date = dateSr.ToLower();
                bool isStaff = staffProf(dateSr);
                if (isStaff && !date.Contains("select") && !date.Contains("union") && !date.Contains("drop") && !date.Contains("update") && !date.Contains("delete"))
                {
                    OdbcConnection con = new OdbcConnection("DSN=Connect");
                    con.Open();
                    OdbcDataAdapter ask = new OdbcDataAdapter("select position from staff except select '"+ dateSr +"'", con);
                    DataSet ds = new DataSet();
                    ask.Fill(ds);
                    con.Close();
                    dataGridView1.DataSource = ds.Tables[0];
                    textBox3.Text = success + buttonAdd + "'" + button8.Text + "'";
                }
                else
                {
                    dataGridView1.DataSource = null;
                    textBox3.Text = error + buttonAdd + "'" + button8.Text + "'";
                    if (!isStaff)
                    {
                        textBox3.Text = textBox3.Text + "\r\n" + noProf;
                    }
                }
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                string dateSr = textBox11.Text;
                string date = dateSr.ToLower();
                if (!date.Contains("select") && !date.Contains("union") && !date.Contains("drop") && !date.Contains("update") && !date.Contains("delete"))
                {
                    OdbcConnection con = new OdbcConnection("DSN=Connect");
                    con.Open();
                    OdbcDataAdapter ask = new OdbcDataAdapter("((select surname from passengers) intersect (select surname from staff)) except (select '"+ dateSr +"') ", con);
                    DataSet ds = new DataSet();
                    ask.Fill(ds);
                    con.Close();
                    dataGridView1.DataSource = ds.Tables[0];
                    textBox3.Text = success + buttonAdd + "'" + button9.Text + "'";
                }
                else
                {
                    dataGridView1.DataSource = null;
                    textBox3.Text = error + buttonAdd + "'" + button9.Text + "'";
                }
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                OdbcConnection con = new OdbcConnection("DSN=Connect");
                con.Open();
                OdbcDataAdapter ask = new OdbcDataAdapter("select * from staff, transactions", con);
                DataSet ds = new DataSet();
                ask.Fill(ds);
                con.Close();
                dataGridView1.DataSource = ds.Tables[0];
                textBox3.Text = success + buttonAdd + "'" + button10.Text + "'";
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                OdbcConnection con = new OdbcConnection("DSN=Connect");
                con.Open();
                OdbcDataAdapter ask = new OdbcDataAdapter("select * from passengers where ((select person_id from transactions tran join flights fli on tran.flight_id = fli.id_flight where tran.elite = true limit 1) = passengers.id_pass)", con);
                DataSet ds = new DataSet();
                ask.Fill(ds);
                con.Close();
                dataGridView1.DataSource = ds.Tables[0];
                textBox3.Text = success + buttonAdd + "'" + button11.Text + "'";
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                string dateSr = textBox13.Text;
                string date = dateSr.ToLower();
                string dateSrEnd = textBox12.Text;
                string dateEnd = dateSrEnd.ToLower();
                if (!date.Contains("select") && !date.Contains("union") && !date.Contains("drop") && !date.Contains("update") && !date.Contains("delete")
                    && !dateEnd.Contains("select") && !dateEnd.Contains("union") && !dateEnd.Contains("drop") && !dateEnd.Contains("update") && !dateEnd.Contains("delete"))
                {
                    OdbcConnection con = new OdbcConnection("DSN=Connect");
                    con.Open();
                    OdbcDataAdapter ask = new OdbcDataAdapter("select fli.first_country as from , fli.destination, fli.time_start, fli.time_end, pl.model from planes1 pl inner join flights fli on fli.plane_id = pl.id_plane where fli.time_start > '"+ dateSr +"' and fli.time_start < '"+ dateSrEnd +"' order by destination ", con);
                    DataSet ds = new DataSet();
                    ask.Fill(ds);
                    con.Close();
                    dataGridView1.DataSource = ds.Tables[0];
                    textBox3.Text = success + buttonAdd + "'" + button12.Text + "'";
                }
                else
                {
                    dataGridView1.DataSource = null;
                    textBox3.Text = error + buttonAdd + "'" + button12.Text + "'";
                }
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                string maxPriceString = textBox14.Text;
                int maxPrice;
                bool isNumeric = int.TryParse(maxPriceString, out maxPrice);
                if (isNumeric)
                {
                    OdbcConnection con = new OdbcConnection("DSN=Connect");
                    con.Open();
                    OdbcDataAdapter ask = new OdbcDataAdapter("select getpercentageofcapacity("+ maxPriceString +")", con);
                    DataSet ds = new DataSet();
                    ask.Fill(ds);
                    con.Close();
                    dataGridView1.DataSource = ds.Tables[0];
                    textBox3.Text = success + buttonAdd + "'" + button13.Text + "'";
                }
                else
                {
                    dataGridView1.DataSource = null;
                    textBox3.Text = error + buttonAdd + "'" + button13.Text + "'";
                }
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                OdbcConnection con = new OdbcConnection("DSN=Connect");
                con.Open();
                OdbcDataAdapter ask = new OdbcDataAdapter("select flights.id_flight, sum(transactions.sum) from flights, transactions where transactions.flight_id = flights.id_flight group by id_flight", con);
                DataSet ds = new DataSet();
                ask.Fill(ds);
                con.Close();
                dataGridView1.DataSource = ds.Tables[0];
                textBox3.Text = success + buttonAdd + "'" + button14.Text + "'";
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                string dateSr = textBox15.Text;
                string date = dateSr.ToLower();
                if (!date.Contains("select") && !date.Contains("union") && !date.Contains("drop") && !date.Contains("update") && !date.Contains("delete"))
                {
                    OdbcConnection con = new OdbcConnection("DSN=Connect");
                    con.Open();
                    OdbcDataAdapter ask = new OdbcDataAdapter("select * from flights, planes1 where exists(select first_country from flights where first_country = '"+ dateSr +"') AND flights.plane_id = planes1.id_plane", con);
                    DataSet ds = new DataSet();
                    ask.Fill(ds);
                    con.Close();
                    dataGridView1.DataSource = ds.Tables[0];
                    textBox3.Text = success + buttonAdd + "'" + button15.Text + "'";
                }
                else
                {
                    dataGridView1.DataSource = null;
                    textBox3.Text = error + buttonAdd + "'" + button15.Text + "'";
                }
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                OdbcConnection con = new OdbcConnection("DSN=Connect");
                con.Open();
                OdbcDataAdapter ask = new OdbcDataAdapter("select * from planes1", con);
                DataSet ds = new DataSet();
                ask.Fill(ds);
                con.Close();
                dataGridView1.DataSource = ds.Tables[0];
                textBox3.Text = success + buttonAdd + "'" + button17.Text + "'";
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                OdbcConnection con = new OdbcConnection("DSN=Connect");
                con.Open();
                OdbcDataAdapter ask = new OdbcDataAdapter("select * from flights", con);
                DataSet ds = new DataSet();
                ask.Fill(ds);
                con.Close();
                dataGridView1.DataSource = ds.Tables[0];
                textBox3.Text = success + buttonAdd + "'" + button16.Text + "'";
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                OdbcConnection con = new OdbcConnection("DSN=Connect");
                con.Open();
                OdbcDataAdapter ask = new OdbcDataAdapter("select * from staff", con);
                DataSet ds = new DataSet();
                ask.Fill(ds);
                con.Close();
                dataGridView1.DataSource = ds.Tables[0];
                textBox3.Text = success + buttonAdd + "'" + button21.Text + "'";
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (admin)
            {
                try
                {
                    OdbcConnection con = new OdbcConnection("DSN=Connect");
                    con.Open();
                    OdbcDataAdapter ask = new OdbcDataAdapter("select * from passport", con);
                    DataSet ds = new DataSet();
                    ask.Fill(ds);
                    con.Close();
                    dataGridView1.DataSource = ds.Tables[0];
                    textBox3.Text = success + buttonAdd + "'" + button20.Text + "'";
                }
                catch (OdbcException rofl)
                {
                    MessageBox.Show(errorBox + rofl);
                }
            }
            else
            {
                MessageBox.Show(insufficientRights);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (admin)
            {
                try
                {
                    OdbcConnection con = new OdbcConnection("DSN=Connect");
                    con.Open();
                    OdbcDataAdapter ask = new OdbcDataAdapter("select * from transactions", con);
                    DataSet ds = new DataSet();
                    ask.Fill(ds);
                    con.Close();
                    dataGridView1.DataSource = ds.Tables[0];
                    textBox3.Text = success + buttonAdd + "'" + button18.Text + "'";
                }
                catch (OdbcException rofl)
                {
                    MessageBox.Show(errorBox + rofl);
                }
            }
            else
            {
                MessageBox.Show(insufficientRights);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                OdbcConnection con = new OdbcConnection("DSN=Connect");
                con.Open();
                OdbcDataAdapter ask = new OdbcDataAdapter("select * from passengers", con);
                DataSet ds = new DataSet();
                ask.Fill(ds);
                con.Close();
                dataGridView1.DataSource = ds.Tables[0];
                textBox3.Text = success + buttonAdd + "'" + button19.Text + "'";
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                OdbcConnection con = new OdbcConnection("DSN=Connect");
                con.Open();
                OdbcDataAdapter ask = new OdbcDataAdapter("select * from luggage", con);
                DataSet ds = new DataSet();
                ask.Fill(ds);
                con.Close();
                dataGridView1.DataSource = ds.Tables[0];
                textBox3.Text = success + buttonAdd + "'" + button22.Text + "'";
            }
            catch (OdbcException rofl)
            {
                MessageBox.Show(errorBox + rofl);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Close();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (admin)
            {
                Form4 form4 = new Form4(this);
                form4.ShowDialog();
            }
            else
            {
                MessageBox.Show(insufficientRights);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        
        private void button25_Click(object sender, EventArgs e)
        {
            Form2 formTest = new Form2(this);
            formTest.ShowDialog();
        }

        private void Form1_MouseDown_1(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
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
