using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PizzaStore
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (Form3.login == true)
            {
                int id = Form3.currentID;
                string connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";

                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "SELECT * FROM salyersCard WHERE custID=@id";
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@id", id);
                    MySqlDataReader myReader = cmd.ExecuteReader();

                    if (myReader.Read())
                    {
                        textBox1.Text = myReader["firstName"].ToString() + " " + myReader["lastName"].ToString();
                        comboBox1.SelectedItem = myReader["type"].ToString();
                        textBox2.Text = myReader["cardNumber"].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(myReader["expDate"].ToString());
                        checkBox1.Checked = false;
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.Text != "Select Card Type" && comboBox1.SelectedItem.ToString() != "Select Card Type" && textBox2.Text != ""){
                if (checkBox1.Checked == true)
                {
                    int id = Form3.currentID;
                    string[] name = textBox1.Text.Split(' ');
                    string firstName = name[0];
                    string lastName = name[1];
                    string type = Convert.ToString(comboBox1.SelectedItem);
                    string cardNum = textBox2.Text;
                    DateTime expDate = dateTimePicker1.Value;
                    string connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";
                    MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
                    try
                    {
                        Console.WriteLine("Connecting to MySQL...");
                        conn.Open();

                        string sql1 = "SELECT * FROM salyersCard WHERE cardNumber = @cardNum";
                        MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand(sql1, conn);

                        cmd1.Parameters.AddWithValue("@cardNum", cardNum);
                        Int64 exists = Convert.ToInt64(cmd1.ExecuteScalar());

                        if (exists != 0)
                        {
                            textBox1.Text = "";
                            comboBox1.SelectedItem = "Select Card Type";
                            textBox2.Text = "";
                            dateTimePicker1.Value = DateTime.Now;

                            Form16 cardFailed = new Form16();
                            cardFailed.ShowDialog();
                        }
                        else
                        {
                            string sql2 = "INSERT INTO salyersCard (firstName, lastName, type, cardNumber, expDate, custID) VALUES (@fname, @lname, @type, @cardNum, @expDate, @id)";
                            MySql.Data.MySqlClient.MySqlCommand cmd2 = new MySql.Data.MySqlClient.MySqlCommand(sql2, conn);
                            cmd2.Parameters.AddWithValue("@fname", firstName);
                            cmd2.Parameters.AddWithValue("@lname", lastName);
                            cmd2.Parameters.AddWithValue("@type", type);
                            cmd2.Parameters.AddWithValue("@cardNum", cardNum);
                            cmd2.Parameters.AddWithValue("@expDate", expDate);
                            cmd2.Parameters.AddWithValue("@id", id);
                            cmd2.ExecuteNonQuery();

                            string sql3 = "UPDATE salyersCustomer SET cardNumber = @cardNum WHERE custID = @id";
                            MySql.Data.MySqlClient.MySqlCommand cmd3 = new MySql.Data.MySqlClient.MySqlCommand(sql3, conn);
                            cmd3.Parameters.AddWithValue("@cardNum", cardNum);
                            cmd3.Parameters.AddWithValue("@id", id);
                            cmd3.ExecuteNonQuery();

                            Form15 cardSaved = new Form15();
                            cardSaved.ShowDialog();

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
                else
                {
                    Form5 orderRecord = new Form5();
                    orderRecord.ShowDialog();

                    this.Close();
                }
            }
        }
    }
}
