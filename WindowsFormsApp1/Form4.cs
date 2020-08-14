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
    public partial class Form4 : Form
    {
        private Form1 form1;
        private Point last;

        public Form4(Form1 form11)
        {
            form11.Hide();
            form1 = form11;
            InitializeComponent();
        }


        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormsInsert.Form5 form5 = new FormsInsert.Form5(this);
            form5.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormsInsert.Form1 form1 = new FormsInsert.Form1(this);
            form1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormsInsert.Form2 form2 = new FormsInsert.Form2(this);
            form2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormsInsert.Form3 form3 = new FormsInsert.Form3(this);
            form3.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormsInsert.Form4 form4 = new FormsInsert.Form4(this);
            form4.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormsInsert.Form6 form6 = new FormsInsert.Form6(this);
            form6.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormsInsert.Form7 form7 = new FormsInsert.Form7(this);
            form7.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            form1.Show();
            Close();
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
