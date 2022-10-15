using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystemCsharp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp signup = new SignUp();
            signup.Show();
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
                MySqlDataAdapter sda = new MySqlDataAdapter("select * from users where username='" + bunifuMetroTextbox1.Text.Trim() + "'and password='" + MD5Hash(bunifuMetroTextbox2.Text.Trim()) + "'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    MySqlCommand cmd = new MySqlCommand("select * from users where username='" + bunifuMetroTextbox1.Text.Trim() + "'and password='" + MD5Hash(bunifuMetroTextbox2.Text.Trim()) + "'", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MySqlDataReader reader;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userdetail user = new userdetail();
                        user.setUname((string)reader["username"].ToString());
                      
                        if ((string)reader["usertype"].ToString() == "member")
                        {
                            Home home = new Home();
                            this.Hide();
                            home.Show();
                        }
                        if ((string)reader["usertype"].ToString() == "manager")
                        {
                            Manager_home manager = new Manager_home();
                            this.Hide();
                            manager.Show();
                        }
                        if ((string)reader["usertype"].ToString() == "admin")
                        {
                            Admin_home admin = new Admin_home();
                            this.Hide();
                            admin.Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Check Again");
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
