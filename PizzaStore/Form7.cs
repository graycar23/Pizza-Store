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
using System.Security.Cryptography.X509Certificates;

namespace PizzaStore
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 StatusChange = new Form8();
            StatusChange.ShowDialog();
        }

        public class orderStatusChange
        {
            public int orderNum = 0;
            public int custID = 0;
            public string orderPizzaInfo = "";
            public double orderPrice = 0;
            public string orderStatus = "";
            public DateTime orderDate = DateTime.Now;
            public string status = "";
        }

        public ArrayList getOrderStatus()
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
                string sql = "SELECT * FROM salyersOrder WHERE status <> @status ORDER BY date ASC";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@status", "Picked Up");
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
            foreach (DataRow row in myTable.Rows)
            {
                orderStatusChange newEvent = new orderStatusChange();
                newEvent.orderNum = Convert.ToInt32(row["orderNumber"]);
                newEvent.custID = Convert.ToInt32(row["custID"]);
                newEvent.orderPizzaInfo = row["pizzaInfo"].ToString();
                newEvent.orderPrice = Convert.ToDouble(row["price"].ToString());
                newEvent.orderDate = Convert.ToDateTime(row["date"].ToString());
                newEvent.status = row["status"].ToString();
                eventList.Add(newEvent);
            }
            return eventList;  //return the event list
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            ArrayList orderStat = getOrderStatus();

            foreach (orderStatusChange orders in orderStat)
            {
                ListViewItem item = new ListViewItem();
                item.Text = orders.orderNum.ToString();
                item.SubItems.Add(orders.custID.ToString());
                item.SubItems.Add(orders.orderPizzaInfo);
                item.SubItems.Add("$" + orders.orderPrice.ToString() + ".00");
                item.SubItems.Add(orders.orderDate.ToString());
                item.SubItems.Add(orders.status);
                listView1.Items.Add(item);
            }

            listView1.View = View.Details;
        }

        public static int orderNumSelected = 0;
        public static string orderStatSelected = "";
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem order in listView1.SelectedItems)
            {
                orderNumSelected = Convert.ToInt32(order.Text);
                orderStatSelected = order.SubItems[5].Text;
            }

            Form8 statusChange = new Form8();
            statusChange.ShowDialog();

            listView1.Items.Clear();

            ArrayList orderStat = getOrderStatus();

            foreach (orderStatusChange orders in orderStat)
            {
                ListViewItem item = new ListViewItem();
                item.Text = orders.orderNum.ToString();
                item.SubItems.Add(orders.custID.ToString());
                item.SubItems.Add(orders.orderPizzaInfo);
                item.SubItems.Add("$" + orders.orderPrice.ToString() + ".00");
                item.SubItems.Add(orders.orderDate.ToString());
                item.SubItems.Add(orders.status);
                listView1.Items.Add(item);
            }

            listView1.View = View.Details;
        }
    }
}
