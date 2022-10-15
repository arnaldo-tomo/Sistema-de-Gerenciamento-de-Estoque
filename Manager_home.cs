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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;



namespace InventorySystemCsharp
{
    public partial class Manager_home : Form
    {
        public Manager_home()
        {
            InitializeComponent();
            slide_panel.Height = add.Height;
            slide_panel.Top = add.Top;
            additem_panel.BringToFront();
        }

        //form load events
        private void Manager_home_Load(object sender, EventArgs e)
        {
            //ManagerDetails manager = new ManagerDetails();
            //manager_name.Text = manager.getMname();


            itemcode.Enabled = false;
            itemcode.Text = "Id Auto Number";
            FillGridView();

            u_itemcodeTxt.Enabled = false;
            u_itemcodeTxt.Text = "Id Auto Number";
            FillUpdateGridView();

            d_itemcodeTxt.Enabled = false;
            d_itemcodeTxt.Text = "Id Auto Number";
            FilldeleteGridView();

            p_order_idTxt.Enabled = false;
            p_order_idTxt.Text = "Id Auto Number";
            FillPaidGridView();

            unp_orderidTxt.Enabled = false;
            unp_orderidTxt.Text = "Id Auto Number";
            FillUnpaidGridView();

        }

        //close button
        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //logout button
        private void logout_btn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }

        //------------------------------------navigation pane button events
        //add-item button
        private void add_Click(object sender, EventArgs e)
        {
            slide_panel.Height = add.Height;
            slide_panel.Top = add.Top;
            additem_panel.BringToFront();
            FillGridView();
            model.Clear();
            part.Clear();
            price.Clear();
            instock.Clear();
            comboBox1.SelectedIndex = -1;
            itemcode.Enabled = false;
            itemcode.Text = "Id Auto Number";
        }
        //update item button
        private void update_Click(object sender, EventArgs e)
        {
            slide_panel.Height = update.Height;
            slide_panel.Top = update.Top;
            updateitems_panel.BringToFront();
            FillUpdateGridView();
            u_modelTxt.Clear();
            u_partTxt.Clear();
            u_priceTxt.Clear();
            u_stockTxt.Clear();
            u_typeCombo.SelectedIndex = -1;
            u_itemcodeTxt.Enabled = false;
            u_itemcodeTxt.Text = "Id Auto Number";
        }
        //delete item button
        private void delete_Click(object sender, EventArgs e)
        {
            slide_panel.Height = delete.Height;
            slide_panel.Top = delete.Top;
            deleteitem_panel.BringToFront();
            FilldeleteGridView();
            d_modelTxt.Clear();
            d_partTxt.Clear();
            d_priceTxt.Clear();
            d_instockTxt.Clear();
            d_typeCombo.SelectedIndex = -1;
            d_itemcodeTxt.Enabled = false;
            d_itemcodeTxt.Text = "Id Auto Number";
        }
        //paid orders button
        private void paid_orders_Click(object sender, EventArgs e)
        {
            slide_panel.Height = paid_orders.Height;
            slide_panel.Top = paid_orders.Top;
            paid_orders_panel.BringToFront();
            FillPaidGridView();
            p_order_detailsTxt.Clear();
            p_partTxt.Clear();
            p_order_priceTxt.Clear();
            p_order_paidTxt.Clear();
            p_order_idTxt.Enabled = false;
            p_order_idTxt.Text = "Id Auto Number";
        }
        //Unpaid orders button
        private void unpaid_orders_Click(object sender, EventArgs e)
        {

            slide_panel.Height = unpaid_orders.Height;
            slide_panel.Top = unpaid_orders.Top;
            unp_order_panel.BringToFront();
            FillUnpaidGridView();
            unp_orderidTxt.Enabled = false;
            unp_orderidTxt.Text = "Id Auto Number";
            unp_orderdetailsTxt.Clear();
            unp_partTxt.Clear();
            unp_priceTxt.Clear();
            unp_ispaidTxt.Clear();
        }

        ////////////////////////////////////---------------INPUT ITEMS PANEL FUNCTIONS----------///////////////////////////////////////

        //The method executes when the add item button clicks
        private void additem_Click(object sender, EventArgs e)
        {
            if(model.Text!="" && part.Text!="" && comboBox1.Text!="" && price.Text!="" && instock.Text != "")
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
                    string query = "insert into `spareparts`(`model`,`part`,`type`,`price`,`instock`) values('" + model.Text.Trim() + "','" + part.Text.Trim() + "','" + comboBox1.Text.Trim() + "','" + price.Text.Trim() + "','" + instock.Text.Trim() + "')";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("The item has been added successfully!");
                    model.Clear();
                    part.Clear();
                    price.Clear();
                    instock.Clear();
                    comboBox1.SelectedIndex = -1;
                    FillGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("u should fill the all fields");
            }
        }//add item method end

        /*The function tht fills the datagridview*/
        void FillGridView()
        {
            MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
            MySqlDataAdapter sda = new MySqlDataAdapter("select * from spareparts ", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            itemlist.DataSource = dt;
        }

        /*The function tht fills the text boxes when a gridview cell is clicked */
        private void itemlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.itemlist.Rows[e.RowIndex];
                itemcode.Text = row.Cells[0].Value.ToString();
                model.Text = row.Cells[1].Value.ToString();
                part.Text = row.Cells[2].Value.ToString();
                comboBox1.Text = row.Cells[3].Value.ToString();
                price.Text = row.Cells[4].Value.ToString();
                instock.Text = row.Cells[5].Value.ToString();
            }
        }

        ////////////////////////////////////---------------UPDATE ITEMS PANEL FUNCTIONS----------///////////////////////////////////////
        
        //The function executes when the update-the item button clicks
        private void u_itemBtn_Click(object sender, EventArgs e)
        {
            if (u_modelTxt.Text != "" && u_partTxt.Text != "" && u_typeCombo.Text != "" && u_priceTxt.Text != "" && u_stockTxt.Text != "")
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
                    string query = "update `spareparts` set `model`= '" + u_modelTxt.Text + "',`part`= '" + u_partTxt.Text + "',`type`= '" + u_typeCombo.Text + "',`price`='" + u_priceTxt.Text + "', `instock`= '" + u_stockTxt.Text + "'where `id`= '"+ u_itemcodeTxt.Text +"' ";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("The item details are updated successfully!");
                    u_itemcodeTxt.Clear();
                    u_itemcodeTxt.Enabled = false;
                    u_itemcodeTxt.Text = "Id Auto Number";
                    u_modelTxt.Clear();
                    u_partTxt.Clear();
                    u_priceTxt.Clear();
                    u_stockTxt.Clear();
                    u_typeCombo.SelectedIndex = -1;
                    FillUpdateGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("u should select an item before updating !");
            }

        }//u_itemBtn method end

        /*The function tht fills the datagridview*/
        void FillUpdateGridView()
        {
            MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
            MySqlDataAdapter u_sda = new MySqlDataAdapter("select * from spareparts ", conn);
            DataTable u_dt = new DataTable();
            u_sda.Fill(u_dt);
            u_dataGridView.DataSource = u_dt;
        }

        /*The function tht fills the text boxes when a update gridview cell is clicked */
        private void u_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.u_dataGridView.Rows[e.RowIndex];
                u_itemcodeTxt.Text = row.Cells[0].Value.ToString();
                u_modelTxt.Text = row.Cells[1].Value.ToString();
                u_partTxt.Text = row.Cells[2].Value.ToString();
                u_typeCombo.Text = row.Cells[3].Value.ToString();
                u_priceTxt.Text = row.Cells[4].Value.ToString();
                u_stockTxt.Text = row.Cells[5].Value.ToString();
            }
        }

        ////////////////////////////////////---------------DELETE ITEMS PANEL FUNCTIONS----------///////////////////////////////////////

        //The function tht executes when the delete items button clicks
        private void del_item_btn_Click(object sender, EventArgs e)
        {
            if (d_modelTxt.Text != "" && d_partTxt.Text != "" && d_typeCombo.Text != "" && d_priceTxt.Text != "" && d_instockTxt.Text != "")
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
                    string query = "delete from `spareparts` where `id`= '" + d_itemcodeTxt.Text + "' ";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("The item has been successfully deleted!");
                    d_itemcodeTxt.Clear();
                    d_itemcodeTxt.Enabled = false;
                    d_itemcodeTxt.Text = "Id Auto Number";
                    d_modelTxt.Clear();
                    d_partTxt.Clear();
                    d_priceTxt.Clear();
                    d_instockTxt.Clear();
                    d_typeCombo.SelectedIndex = -1;
                    FilldeleteGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("u should select a row before deleting !");
            }
        }

        /*The function tht fills delete the datagridview*/
        void FilldeleteGridView()
        {
            MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
            MySqlDataAdapter d_sda = new MySqlDataAdapter("select * from spareparts ", conn);
            DataTable d_dt = new DataTable();
            d_sda.Fill(d_dt);
            d_item_dataGridView.DataSource = d_dt;
        }

        /*The function tht fills the text boxes when a delete gridview cell is clicked */
        private void d_item_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.d_item_dataGridView.Rows[e.RowIndex];
                d_itemcodeTxt.Text = row.Cells[0].Value.ToString();
                d_modelTxt.Text = row.Cells[1].Value.ToString();
                d_partTxt.Text = row.Cells[2].Value.ToString();
                d_typeCombo.Text = row.Cells[3].Value.ToString();
                d_priceTxt.Text = row.Cells[4].Value.ToString();
                d_instockTxt.Text = row.Cells[5].Value.ToString();
            }
        }


        ////////////////////////////////////---------------PAID ORDES PANEL FUNCTIONS----------///////////////////////////////////////

        //The function tht executes when the unpaid order button clicks
        private void make_unPaid_btn_Click(object sender, EventArgs e)
        {
            if (p_order_idTxt.Text != ""&& p_order_detailsTxt.Text !="")
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
                    string query = "update `orders` set `paid`= '" + "no" + "'where `id`= '" + p_order_idTxt.Text + "' ";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("The Order was marked as Unpaid & sent to the Unpaid table !");
                    p_order_idTxt.Clear();
                    p_order_idTxt.Enabled = false;
                    p_order_idTxt.Text = "Id Auto Number";
                    p_order_detailsTxt.Clear();
                    p_partTxt.Clear();
                    p_order_priceTxt.Clear();
                    p_order_paidTxt.Clear();
                    FillPaidGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("First...u should select an order to make changes");
            }
        }

        /*The function tht fills paid orders datagridview*/
        void FillPaidGridView()
        {
            String status = "yes";
            MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
            MySqlDataAdapter p_sda = new MySqlDataAdapter("select * from orders where paid = '"+ status +"' ", conn);
            DataTable p_dt = new DataTable();
            p_sda.Fill(p_dt);
            paid_dataGridView1.DataSource = p_dt;
        }

        /*The function tht fills the text boxes when a delete gridview cell is clicked*/
        private void paid_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.paid_dataGridView1.Rows[e.RowIndex];
                p_order_idTxt.Text = row.Cells[0].Value.ToString();
                p_order_detailsTxt.Text = row.Cells[1].Value.ToString();
                p_partTxt.Text = row.Cells[2].Value.ToString();
                p_order_priceTxt.Text = row.Cells[3].Value.ToString();
                p_order_paidTxt.Text = row.Cells[4].Value.ToString();
            }
        }

        //The function tht executes when the cancel order button is clicked
        private void cancel_order_btn_Click(object sender, EventArgs e)
        {
            if (p_order_idTxt.Text != ""&& p_order_detailsTxt.Text !="")
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
                    string query = "update `orders` set `paid`= '" + "cancelled" + "'where `id`= '" + p_order_idTxt.Text + "' ";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("The Order was marked as Cancelled & sent to the Cancelled orders table !");
                    p_order_idTxt.Clear();
                    p_order_idTxt.Enabled = false;
                    p_order_idTxt.Text = "Id Auto Number";
                    p_order_detailsTxt.Clear();
                    p_partTxt.Clear();
                    p_order_priceTxt.Clear();
                    p_order_paidTxt.Clear();
                    FillPaidGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("First...u should select an order to cancel !");
            }
        }

        ////////////////////////////////////---------------UNPAID ORDES PANEL FUNCTIONS----------///////////////////////////////////////

        //The function tht executes when the paid order button clicks
        private void unp_make_btn_Click(object sender, EventArgs e)
        {
            if (unp_orderidTxt.Text != "" && unp_orderdetailsTxt.Text !="")
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
                    string query = "update `orders` set `paid`= '" + "yes" + "'where `id`= '" + unp_orderidTxt.Text + "' ";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("The Order was marked as Paid & sent to the paid order table !");
                    unp_orderidTxt.Clear();
                    unp_orderidTxt.Enabled = false;
                    unp_orderidTxt.Text = "Id Auto Number";
                    unp_orderdetailsTxt.Clear();
                    unp_partTxt.Clear();
                    unp_priceTxt.Clear();
                    unp_ispaidTxt.Clear();
                    FillUnpaidGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("First...u should select an order to make changes !");
            }
        }

        /*The function tht fills Unpaid orders datagridview*/
        void FillUnpaidGridView()
        {
            String status = "no";
            MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
            MySqlDataAdapter unp_sda = new MySqlDataAdapter("select * from orders where paid = '" + status + "' ", conn);
            DataTable unp_dt = new DataTable();
            unp_sda.Fill(unp_dt);
            unp_dataGridView.DataSource = unp_dt;
        }

        /*The function tht fills the text boxes when a unpaid gridview cell is clicked*/
        private void unp_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.unp_dataGridView.Rows[e.RowIndex];
                unp_orderidTxt.Text = row.Cells[0].Value.ToString();
                unp_orderdetailsTxt.Text = row.Cells[1].Value.ToString();
                unp_partTxt.Text = row.Cells[2].Value.ToString();
                unp_priceTxt.Text = row.Cells[3].Value.ToString();
                unp_ispaidTxt.Text = row.Cells[4].Value.ToString();
            }
        }

        //The function tht executes when the cancel order button is clicked
        private void unp_cancelorder_btn_Click(object sender, EventArgs e)
        {
            if (unp_orderidTxt.Text != "" && unp_orderdetailsTxt.Text != "")
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
                    string query = "update `orders` set `paid`= '" + "cancelled" + "'where `id`= '" + unp_orderidTxt.Text + "' ";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("The Order was marked as Cancelled & sent to the Cancelled orders table !");
                    unp_orderidTxt.Clear();
                    unp_orderidTxt.Enabled = false;
                    unp_orderidTxt.Text = "Id Auto Number";
                    unp_orderdetailsTxt.Clear();
                    unp_partTxt.Clear();
                    unp_priceTxt.Clear();
                    unp_ispaidTxt.Clear();
                    FillUnpaidGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("First....u should select an order to cancel !");
            }
        }


        //export pdf function
        public void exportgridtopdf(DataGridView dgw, string filename)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dgw.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;

            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            //add header
            foreach (DataGridViewColumn column in dgw.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                pdftable.AddCell(cell);
            }

            //add datarow
            foreach (DataGridViewRow row in dgw.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdftable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }

            var savefiledialoge = new SaveFileDialog();
            savefiledialoge.FileName = filename;
            savefiledialoge.DefaultExt = ".pdf";
            if (savefiledialoge.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefiledialoge.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdftable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            exportgridtopdf(itemlist, "item-List Report");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            exportgridtopdf(unp_dataGridView, "unpaid order-List Report");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            exportgridtopdf(paid_dataGridView1, "Paid order-List Report");
        }
    }
}
