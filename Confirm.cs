using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystemCsharp
{  
    public partial class Confirm : Form
    {
        public Home MyParent { get; set; }
        public Confirm()
        {
            InitializeComponent();
        }

        private void Confirm_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = MyParent.ItemList;
        }

        private void Confirm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MyParent.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyParent.ItemList = "";
            MyParent.TotalPrice = 0;
            MyParent.UpdateQuery = "";
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                userdetail user = new userdetail();

                MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
                string query = "INSERT INTO `orders`(`user`,`details`,`price`) VALUES('"+user.getUname()+"','"+MyParent.ItemList+"',"+MyParent.TotalPrice+");"+MyParent.UpdateQuery+"";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Success");
                button1_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
