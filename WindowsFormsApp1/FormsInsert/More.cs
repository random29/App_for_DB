using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.FormsInsert
{
    public partial class More : Form
    {
        private Form1 newForm;
        private Form2 newForm2;
        private Form3 newForm3;
        private Form4 newForm4;
        private Form5 newForm5;
        private Form6 newForm6;
        private Form7 newForm7;
        private WindowsFormsApp1.Form4 elder;
        private int num;
        public More(Form1 form, WindowsFormsApp1.Form4 test)
        {
            elder = test;
            newForm = form;
            num = 1;
            InitializeComponent();
        }

        public More(Form2 form, WindowsFormsApp1.Form4 test)
        {
            elder = test;
            newForm2 = form;
            num = 2;
            InitializeComponent();
        }
        public More(Form3 form, WindowsFormsApp1.Form4 test)
        {
            elder = test;
            newForm3 = form;
            num = 3;
            InitializeComponent();
        }
        public More(Form4 form, WindowsFormsApp1.Form4 test)
        {
            elder = test;
            newForm4 = form;
            num = 4;
            InitializeComponent();
        }
        public More(Form5 form, WindowsFormsApp1.Form4 test)
        {
            elder = test;
            newForm5 = form;
            num = 5;
            InitializeComponent();
        }
        public More(Form6 form, WindowsFormsApp1.Form4 test)
        {
            elder = test;
            newForm6 = form;
            num = 6;
            InitializeComponent();
        }
        public More(Form7 form, WindowsFormsApp1.Form4 test)
        {
            elder = test;
            newForm7 = form;
            num = 7;
            InitializeComponent();
        }
        private void More_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (num)
            {  
                case 1:
                    newForm.Close();
                    elder.Show();
                    Close();
                    break;
                case 2:
                    newForm2.Close();
                    elder.Show();
                    Close();
                    break;
                case 3:
                    newForm3.Close();
                    elder.Show();
                    Close();
                    break;
                case 4:
                    newForm4.Close();
                    elder.Show();
                    Close();
                    break;
                case 5:
                    newForm5.Close();
                    elder.Show();
                    Close();
                    break;
                case 6:
                    newForm6.Close();
                    elder.Show();
                    Close();
                    break;
                case 7:
                    newForm7.Close();
                    elder.Show();
                    Close();
                    break;
            }
        }
    }
}
