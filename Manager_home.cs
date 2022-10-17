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

        //formulário carrega eventos
        private void Manager_home_Load(object sender, EventArgs e)
        {
            //ManagerDetails manager = new ManagerDetails();
            //manager_name.Text = manager.getMname();


            itemcode.Enabled = false;
            itemcode.Text = "Codigo automático";
            FillGridView();

            u_itemcodeTxt.Enabled = false;
            u_itemcodeTxt.Text = "Codigo automático";
            FillUpdateGridView();

            d_itemcodeTxt.Enabled = false;
            d_itemcodeTxt.Text = "Codigo automático";
            FilldeleteGridView();

            p_order_idTxt.Enabled = false;
            p_order_idTxt.Text = "Codigo automático";
            FillPaidGridView();

            unp_orderidTxt.Enabled = false;
            unp_orderidTxt.Text = "Codigo automático";
            FillUnpaidGridView();

        }

        //Fechar button
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

        //------------------------------------eventos do botão do painel de navegação
        //botão adicionar item
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
            itemcode.Text = "Codigo automático";
        }
        //botão atualizar item
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
            u_itemcodeTxt.Text = "Codigo automático";
        }
        //deleter item button
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
            d_itemcodeTxt.Text = "Codigo automático";
        }
        //botão de pedidos pagos
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
            p_order_idTxt.Text = "Codigo automático";
        }

        //Botão de pedidos não pagos
        private void unpaid_orders_Click(object sender, EventArgs e)
        {

            slide_panel.Height = unpaid_orders.Height;
            slide_panel.Top = unpaid_orders.Top;
            unp_order_panel.BringToFront();
            FillUnpaidGridView();
            unp_orderidTxt.Enabled = false;
            unp_orderidTxt.Text = "Codigo automático";
            unp_orderdetailsTxt.Clear();
            unp_partTxt.Clear();
            unp_priceTxt.Clear();
            unp_ispaidTxt.Clear();
        }

        ///////////////////////////////////----------- -FUNÇÕES DO PAINEL DE ITENS DE ENTRADA----------/////////////////////////////////// ////

        //O método é executado quando o botão adicionar item clica
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
                    MessageBox.Show("O item foi adicionado com sucesso!");
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
                MessageBox.Show("vc deve preencher todos os campos");
            }
        }//adiciona fim do método do item

        /*A função que preenche o datagridview*/
        void FillGridView()
        {
            MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
            MySqlDataAdapter sda = new MySqlDataAdapter("select * from spareparts ", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            itemlist.DataSource = dt;
        }

        /*A função que preenche as caixas de texto quando uma célula do gridview é clicada */
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

        ///////////////////////////////////----------- - ATUALIZAR FUNÇÕES DO PAINEL DE ITENS----------//////////////////////////////////// ////

        //A função é executada quando o botão atualizar-o item clica
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
                    MessageBox.Show("Os detalhes do item foram atualizados com sucesso!");
                    u_itemcodeTxt.Clear();
                    u_itemcodeTxt.Enabled = false;
                    u_itemcodeTxt.Text = "Codigo automático";
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
                MessageBox.Show("você deve selecionar um item antes de atualizar!");
            }

        }// fim do método u_itemBtn

        /*A função que preenche o datagridview*/
        void FillUpdateGridView()
        {
            MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
            MySqlDataAdapter u_sda = new MySqlDataAdapter("select * from spareparts ", conn);
            DataTable u_dt = new DataTable();
            u_sda.Fill(u_dt);
            u_dataGridView.DataSource = u_dt;
        }

        // fim do método u_itemBtn

        /*A função que preenche as caixas de texto quando uma célula de atualização do gridview é clicada */
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

        // fim do método u_itemBtn

        ///////////////////////////////////----------- -EXCLUIR FUNÇÕES DO PAINEL DE ITENS----------/////////////////////////////////// ////

        //A função que é executada quando o botão delete items clica
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
                    MessageBox.Show("O item foi excluído com sucesso!");
                    d_itemcodeTxt.Clear();
                    d_itemcodeTxt.Enabled = false;
                    d_itemcodeTxt.Text = "Codigo automático";
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
                MessageBox.Show("você deve selecionar uma linha antes de excluir!");
            }
        }

        /*A função que preenche exclui o datagridview*/
        void FilldeleteGridView()
        {
            MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
            MySqlDataAdapter d_sda = new MySqlDataAdapter("select * from spareparts ", conn);
            DataTable d_dt = new DataTable();
            d_sda.Fill(d_dt);
            d_item_dataGridView.DataSource = d_dt;
        }

        /*A função que preenche as caixas de texto quando uma célula do gridview é clicada */
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


        ///////////////////////////////////----------- -FUNÇÕES DO PAINEL DE PEDIDOS PAGOS----------/////////////////////////////////// ////

        //A função que é executada quando o botão de pedido não pago clica
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
                    MessageBox.Show("O pedido foi marcado como Não pago e enviado para a tabela Não pago!");
                    p_order_idTxt.Clear();
                    p_order_idTxt.Enabled = false;
                    p_order_idTxt.Text = "Codigo automático";
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
                MessageBox.Show("Primeiro... você deve selecionar um pedido para fazer alterações");
            }
        }

        /*A função que preenche os pedidos pagos datagridview*/
        void FillPaidGridView()
        {
            String status = "yes";
            MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
            MySqlDataAdapter p_sda = new MySqlDataAdapter("select * from orders where paid = '"+ status +"' ", conn);
            DataTable p_dt = new DataTable();
            p_sda.Fill(p_dt);
            paid_dataGridView1.DataSource = p_dt;
        }

        /*A função que preenche as caixas de texto quando uma célula de exclusão de grade é clicada*/
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

        //A função que é executada quando o botão cancelar ordem é clicado
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
                    MessageBox.Show("O pedido foi marcado como cancelado e enviado para a tabela de pedidos cancelados!");
                    p_order_idTxt.Clear();
                    p_order_idTxt.Enabled = false;
                    p_order_idTxt.Text = "Codigo automático";
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
                MessageBox.Show("Primeiro... você deve selecionar um pedido para cancelar!");
            }
        }

        ///////////////////////////////////----------- -FUNÇÕES DO PAINEL DE PEDIDOS NÃO PAGOS----------/////////////////////////////////// ////

        //A função que é executada quando o botão do pedido pago clica
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
                    MessageBox.Show("O pedido foi marcado como pago e enviado para a tabela de pedidos pagos!");
                    unp_orderidTxt.Clear();
                    unp_orderidTxt.Enabled = false;
                    unp_orderidTxt.Text = "Codigo automático";
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
                MessageBox.Show("Primeiro... você deve selecionar um pedido para fazer alterações!");
            }
        }

        /*A função que preenche o datagridview de pedidos não pagos*/
        void FillUnpaidGridView()
        {
            String status = "no";
            MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=inventorymgcsharp;");
            MySqlDataAdapter unp_sda = new MySqlDataAdapter("select * from orders where paid = '" + status + "' ", conn);
            DataTable unp_dt = new DataTable();
            unp_sda.Fill(unp_dt);
            unp_dataGridView.DataSource = unp_dt;
        }

        /*A função que preenche as caixas de texto quando uma célula de gridview não paga é clicada*/
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

        //A função que é executada quando o botão cancelar ordem é clicado
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
                    MessageBox.Show("O pedido foi marcado como cancelado e enviado para a tabela de pedidos cancelados!");
                    unp_orderidTxt.Clear();
                    unp_orderidTxt.Enabled = false;
                    unp_orderidTxt.Text = "Codigo automático";
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
                MessageBox.Show("Primeiro... você deve selecionar um pedido para cancelar!");
            }
        }


        //exporta função pdf
        public void exportgridtopdf(DataGridView dgw, string filename)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dgw.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;

            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            //adiciona cabeçalho
            foreach (DataGridViewColumn column in dgw.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                pdftable.AddCell(cell);
            }

            //adiciona linha de dados
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
            exportgridtopdf(itemlist, "Relatório de lista de itens");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            exportgridtopdf(unp_dataGridView, "Relatório de lista de pedidos não pagos");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            exportgridtopdf(paid_dataGridView1, "Relatório de lista de pedidos pagos");
        }

        private void label34_Click(object sender, EventArgs e)
        {

        }
    }
}
