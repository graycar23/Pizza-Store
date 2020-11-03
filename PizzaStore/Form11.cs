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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string pass = textBox2.Text;
            string connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {

                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM salyersCustomer WHERE userName=@user AND passWord=@pass";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", pass);
                int exists = Convert.ToInt32(cmd.ExecuteScalar());

                if (exists != 0)
                {
                    Form3.login = true;
                    Form3.currentUsername = user;
                    Form3.currentID = exists;

                    Form4 payment = new Form4();
                    payment.ShowDialog();

                    this.Close();
                }
                else
                {
                    Form13 loginFailed = new Form13();
                    loginFailed.ShowDialog();

                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string firstName = textBox3.Text;
            string lastName = textBox4.Text;
            string username = textBox5.Text;
            string password = textBox6.Text;
            string connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql1 = "SELECT * FROM salyersCustomer WHERE username = @uname";
                MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand(sql1, conn);

                cmd1.Parameters.AddWithValue("@uname", username);
                int exists = Convert.ToInt32(cmd1.ExecuteScalar());

                if (exists != 0)
                {
                    Form9 registerFailed = new Form9();
                    registerFailed.ShowDialog();

                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                }
                else
                {
                    string sql2 = "INSERT INTO salyersCustomer (username, password, firstName, lastName) VALUES (@uname, @pass, @fname, @lname)";
                    MySql.Data.MySqlClient.MySqlCommand cmd2 = new MySql.Data.MySqlClient.MySqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@uname", username);
                    cmd2.Parameters.AddWithValue("@pass", password);
                    cmd2.Parameters.AddWithValue("@fname", firstName);
                    cmd2.Parameters.AddWithValue("@lname", lastName);
                    cmd2.ExecuteNonQuery();

                    string sql3 = "SELECT * FROM salyersCustomer ORDER BY custID DESC LIMIT 1";
                    MySql.Data.MySqlClient.MySqlCommand cmd3 = new MySql.Data.MySqlClient.MySqlCommand(sql3, conn);

                    MySqlDataReader myReader = cmd3.ExecuteReader();

                    if (myReader.Read())
                    {
                        Form3.currentID = Convert.ToInt32(myReader["custID"]);
                    }
                    else
                    {
                        Form3.currentID = 1;
                    }
                    myReader.Close();

                    Form3.login = true;
                    Form3.currentUsername = username;

                    Form4 payment = new Form4();
                    payment.ShowDialog();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }
    }
}
