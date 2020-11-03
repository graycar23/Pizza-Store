using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using MySql.Data.MySqlClient;

namespace PizzaStore
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public class orderHistory
        {
            public int order = 0;
            public string orderPizzaInfo = "";
            public double orderPrice = 0;
            public DateTime orderDate = DateTime.Now;

        }

        public ArrayList getOrderHist(int custID)
        {
            ArrayList eventList = new ArrayList();  //a list to save the events
                                                    //prepare an SQL query to retrieve all the events on the same, specified date
            DataTable myTable = new DataTable();
            string connStr = " server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM salyersOrder WHERE custID = @custID ORDER BY date ASC";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@custID", custID);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
                myAdapter.Fill(myTable);
                Console.WriteLine("Table is ready.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            //convert the retrieved data to events and save them to the list
            int num = 1;
            foreach (DataRow row in myTable.Rows)
            {
                orderHistory newEvent = new orderHistory();
                newEvent.order = num;
                newEvent.orderPizzaInfo = row["pizzaInfo"].ToString();
                newEvent.orderPrice = Convert.ToDouble(row["price"].ToString());
                newEvent.orderDate = Convert.ToDateTime(row["date"].ToString());
                eventList.Add(newEvent);

                num++;
            }
            return eventList;  //return the event list
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            ArrayList orderHist = getOrderHist(Form3.currentID);

            foreach(orderHistory order in orderHist){
                ListViewItem item = new ListViewItem();
                item.Text = order.order.ToString();
                item.SubItems.Add(order.orderPizzaInfo);
                item.SubItems.Add("$" + order.orderPrice.ToString() + ".00");
                item.SubItems.Add(order.orderDate.ToString());
                listView1.Items.Add(item); 
            }

            listView1.View = View.Details;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
