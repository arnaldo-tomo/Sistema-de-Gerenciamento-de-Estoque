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
using System.Security.Cryptography;

namespace InventorySystemCsharp
{
    public partial class Admin_home : Form
    {
        public Admin_home()
        {
            InitializeComponent();
            slide_panel.Height = dashboard_btn.Height;
            dashboard_panel.BringToFront();
        }

        //logout button
        private void logout_btn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            User_list userlist = new User_list();
            userlist.Close();
            this.Close();
            
            login.Show();
            
        }
        //close button
        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //------------------------------------navigation pane button events
        //dashboard button
        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            slide_panel.Height = dashboard_btn.Height;
            slide_panel.Top = dashboard_btn.Top;
            dashboard_panel.BringToFront();
        }
        //add manager button
        private void add_manager_btn_Click(object sender, EventArgs e)
        {
            slide_panel.Height = add_manager_btn.Height;
            slide_panel.Top = add_manager_btn.Top;
            add_manager_panel.BringToFront();
            User_list user = new User_list();
            user.Close();
        }
        private void user_list_btn_Click(object sender, EventArgs e)
        {
            slide_panel.Height = user_list_btn.Height;
            slide_panel.Top = user_list_btn.Top;
            User_list users_list = new User_list();
            users_list.Show();
            this.Hide();
        }


        ////////////////////////////////////---------------DASHBOARD PANEL FUNCTIONS----------///////////////////////////////////////

        //The method executes when the user home button clicks
        private void user_home_btn_Click(object sender, EventArgs e)
        {
            Home user_home = new Home();
            user_home.Show();

        }

        private void manager_home_btn_Click(object sender, EventArgs e)
        {
            Manager_home manager = new Manager_home();
            manager.Show();
        }

        private void item_check_btn_Click(object sender, EventArgs e)
        {
            Manager_home manager = new Manager_home();
            manager.Show();
        }

        private void check_orders_btn_Click(object sender, EventArgs e)
        {
            Manager_home manager = new Manager_home();
            manager.Show();
        }


        ////////////////////////////////////---------------Add manager PANEL FUNCTIONS----------///////////////////////////////////////

        //The method executes when the create manager acc button clicks
        private void Mregister_btn_Click(object sender, EventArgs e)
        {
            if (MfnameTxt.Text != "" && MlnameTxt.Text != "" && MusernameTxt.Text != "" && MphonenumTxt.Text != "" && MpassTxt.Text != "" && MrepassTxt.Text != "" && typecomboTxt.Text != "")
            {
                if (MpassTxt.Text == MrepassTxt.Text)
                {
                    try
                    {
                        //String status = "manager";
                        MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
                        string query = "insert into `users`(`first`,`last`,`username`,`phone`,`password`,`usertype`) values('" + MfnameTxt.Text.Trim() + "','" + MlnameTxt.Text.Trim() + "','" + MusernameTxt.Text.Trim() + "','" + MphonenumTxt.Text.Trim() + "','" + MD5Hash(MpassTxt.Text.Trim()) + "','" + typecomboTxt.Text.Trim() + "')";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("New Manager account has been created successfully!");
                        
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("This Username is already taken!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Given password doesn't match!!");
                }
            }
            else
                MessageBox.Show("Fill all fields");
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

        
    }
}
