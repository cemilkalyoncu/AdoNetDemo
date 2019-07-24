using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNetDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductsDal _productsDal = new ProductsDal();
        private void FillGridView()
        {
            dgwProducts.DataSource = _productsDal.GetAll();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FillGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productsDal.Add(new Products
            {
                Name = txtNameAdd.Text.ToString(),
                UnitPrice = Convert.ToInt32(txtUnitPriceAdd.Text),
                StockAmount = Convert.ToInt32(txtStockAmountAdd.Text)
            });
            FillGridView();

            MessageBox.Show("Product Added", "Product", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNameUpdate.Text = GetCellValue(dgwProducts,1).ToString();
            txtUnitPriceUpdate.Text = GetCellValue(dgwProducts, 2).ToString();
            txtStockAmountUpdate.Text = GetCellValue(dgwProducts, 3).ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productsDal.Update(new Products
            {
                Id = int.Parse(GetCellValue(dgwProducts, 0).ToString()),
                Name = txtNameUpdate.Text,
                UnitPrice = Convert.ToInt32(txtUnitPriceUpdate.Text),
                StockAmount = Convert.ToInt32(txtStockAmountUpdate.Text)

            });
            FillGridView();
            MessageBox.Show("Product Updated", "Product", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }
        private object GetCellValue(DataGridView dgwProducts, int index)
        {
           return dgwProducts.CurrentRow.Cells[index].Value;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = int.Parse(GetCellValue(dgwProducts, 0).ToString());
            _productsDal.Delete(id);
            FillGridView();
            MessageBox.Show("Product Deleted", "Product", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
