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
    public partial class Home : Form
    {
        public string ItemList="";
        public float TotalPrice=0;
        public string UpdateQuery="";


        public Home()
        {
            InitializeComponent();
        }
        private void Home_Load(object sender, EventArgs e)
        {
            FillComboBox();
            ItemList = "";
            TotalPrice = 0;
            UpdateQuery = "";

            userdetail user = new userdetail();
            label9.Text = user.getUname();
        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
                MySqlDataAdapter sda = new MySqlDataAdapter("select * from spareparts where model='" + comboBox1.Text + "' && part='" + comboBox2.Text + "'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        

        void FillComboBox()
        {
            MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
            MySqlDataAdapter sda = new MySqlDataAdapter("select DISTINCT model from spareparts", conn);
            DataSet dt = new DataSet();
            sda.Fill(dt);

            comboBox1.DataSource = dt.Tables[0];
            comboBox1.DisplayMember = "model";
            comboBox1.ValueMember = "model";

            MySqlDataAdapter sda1 = new MySqlDataAdapter("select DISTINCT part from spareparts", conn);
            DataSet dt1 = new DataSet();
            sda1.Fill(dt1);

            comboBox2.DataSource = dt1.Tables[0];
            comboBox2.DisplayMember = "part";
            comboBox2.ValueMember = "part";

            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
                textBox6.Text = row.Cells[5].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(textBox6.Text) >= int.Parse(textBox7.Text))
                {
                    ItemList += textBox2.Text + " " + textBox3.Text + " " + textBox4.Text + " " + textBox5.Text + "*" + textBox7.Text+Environment.NewLine;
                    TotalPrice += float.Parse(textBox5.Text) * float.Parse(textBox7.Text);
                    UpdateQuery += "update spareparts set instock='" + (int.Parse(textBox6.Text) - int.Parse(textBox7.Text)) + "' where id='" + textBox1.Text + "';";
                    String msg = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text + " " + textBox4.Text + "*" + textBox7.Text;
                    MessageBox.Show(msg+Environment.NewLine+"Added to Cart");
                }
                else
                {
                    MessageBox.Show("Not Enough Items in Stock");
                }
            }
            catch(FormatException)
            {
                MessageBox.Show("Enter in Correct Format");
            }
            catch(Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.ItemList == "")
            {
                MessageBox.Show("No Items Selected");
            }
            else
            {
                var childform = new Confirm();
                childform.MyParent = this;
                childform.Show();
                this.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyOrders orders = new MyOrders();
            this.Hide();
            orders.Show();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
