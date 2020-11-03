using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PizzaStore
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string orderNum = label9.Text;

            string connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM salyersOrder WHERE orderNumber = @orderNum";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@orderNum", orderNum);

                MySqlDataReader myReader = cmd.ExecuteReader();

                if (myReader.Read())
                {
                    label11.Text = myReader["status"].ToString();
                    label11.Refresh();
                }
                myReader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            label12.Text = Form1.cheeseCart.ToString() + "x";
            label13.Text = Form1.pepperoniCart.ToString() + "x";
            label14.Text = Form1.sausageCart.ToString() + "x";

            label7.Text = Form2.price.ToString();


            string pizzaInfo = "Cheese: " + Form1.cheeseCart.ToString() + ", Pepperoni: " + Form1.pepperoniCart.ToString() + ", Sausage: " + Form1.sausageCart.ToString();
            double price = Convert.ToDouble(Form2.price.ToString());

            Form1.cheeseCart = 0;
            Form1.pepperoniCart = 0;
            Form1.sausageCart = 0;
            Form2.price = 0;

            string status = "Processing";
            DateTime date = DateTime.Now;
            int custID = Form3.currentID;
            string connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql1 = "SELECT * FROM salyersOrder ORDER BY orderNumber DESC LIMIT 1";
                MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand(sql1, conn);

                MySqlDataReader myReader = cmd1.ExecuteReader();

                if (myReader.Read())
                {
                    label9.Text = (Convert.ToInt32(myReader["orderNumber"]) + 1).ToString();

                }
                else
                {
                    label9.Text = 1.ToString();
                }
                myReader.Close();

                string sql2 = "INSERT INTO salyersOrder (pizzaInfo, price, status, date, custID) VALUES (@pinfo, @price, @status, @date, @custID)";
                MySql.Data.MySqlClient.MySqlCommand cmd2 = new MySql.Data.MySqlClient.MySqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@pinfo", pizzaInfo);
                cmd2.Parameters.AddWithValue("@price", price);
                cmd2.Parameters.AddWithValue("@status", status);
                cmd2.Parameters.AddWithValue("@date", date);
                cmd2.Parameters.AddWithValue("@custID", custID);
                cmd2.ExecuteNonQuery();

            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (label11.Text != "Ready")
            {
                e.Cancel = true;
            }
        }
    }
}
