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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form3.login == false){
                Form11 loginRegister = new Form11();
                loginRegister.ShowDialog();

                this.Close();
            }else{
                Form4 payment = new Form4();
                payment.ShowDialog();

                this.Close();
            }
        }

        public static double price = 0;
        private void Form2_Load(object sender, EventArgs e)
        {
            label9.Text = Form1.cheeseCart.ToString();
            label10.Text = Form1.pepperoniCart.ToString();
            label11.Text = Form1.sausageCart.ToString();

            label12.Text = (Form1.cheeseCart * 13.00).ToString() + ".00";
            label13.Text = (Form1.pepperoniCart * 13.00).ToString() + ".00";
            label14.Text = (Form1.sausageCart * 13.00).ToString() + ".00";

            price = ((Convert.ToDouble(label12.Text)) + (Convert.ToDouble(label13.Text)) + (Convert.ToDouble(label14.Text)));
            label15.Text = price.ToString() + ".00";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.cheeseCart = 0;
            Form1.pepperoniCart = 0;
            Form1.sausageCart = 0;
            Form2.price = 0;

            this.Close();
        }
    }
}
