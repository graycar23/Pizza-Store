using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaStore
{
	public partial class Form1 : Form
	{

		public Form1()
		{
			InitializeComponent();
		}

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Form3.login == true)
            {
                Form14 alreadyLogin = new Form14();
                alreadyLogin.ShowDialog();
            }
            else
            {
                Form3 loginRegister = new Form3();
                loginRegister.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(Form3.login == true)
            {
                Form6 orderHistory = new Form6();
                orderHistory.ShowDialog();
            }
            else
            {
                Form17 histLogin = new Form17();
                histLogin.ShowDialog();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label9.Text != "0")
            {
                Form2 shoppingCart = new Form2();
                shoppingCart.ShowDialog();

                if(cheeseCart == 0 && pepperoniCart == 0 && sausageCart == 0)
                {
                    label9.Text = "0";
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form12 EmployeeLogin = new Form12();
            EmployeeLogin.ShowDialog();
        }

        public static int cheeseCart = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            cheeseCart += 1;
            int cartNum = Int32.Parse(label9.Text);

            cartNum += 1;

            label9.Text = cartNum.ToString();
        }

        public static int pepperoniCart = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            pepperoniCart += 1;
            int cartNum = Int32.Parse(label9.Text);

            cartNum += 1;

            label9.Text = cartNum.ToString();
        }

        public static int sausageCart = 0;

        private void button3_Click(object sender, EventArgs e)
        {
            sausageCart += 1;
            int cartNum = Int32.Parse(label9.Text);

            cartNum += 1;

            label9.Text = cartNum.ToString();
        }

    }
}
