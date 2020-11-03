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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int orderNum = Int32.Parse(label5.Text);
            string orderStat = label3.Text;
            string connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "UPDATE salyersOrder SET status = @orderStat WHERE orderNumber = @orderNum";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@orderStat", orderStat);
                cmd.Parameters.AddWithValue("@orderNum", orderNum);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");

            this.Close();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            label5.Text = Form7.orderNumSelected.ToString();
            label3.Text = Form7.orderStatSelected;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "Preparing";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label3.Text = "Baking";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Text = "Boxing";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label3.Text = "Ready";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label3.Text = "Picked Up";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
