using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductDal _productDal = new ProductDal();
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void LoadProduct()
        {
            dgwProducts.DataSource = _productDal.GetAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Product
            {
                Name = tbxName.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                StockAmount = Convert.ToInt32(tbxStockAmount.Text)
            });
            LoadProduct();
            MessageBox.Show("Added");
            CleanTextBox();
        }

        private void CleanTextBox()
        {
            tbxName.Text = "";
            tbxUnitPrice.Text = "";
            tbxStockAmount.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productDal.Update(new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value),
                Name = tbxNameUpdate.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text),
                StockAmount = Convert.ToInt32(tbxStockAmountUpdate.Text)
            });
            LoadProduct();
            MessageBox.Show("Updated");
            CleanTextBoxUpdate();
        }

        private void CleanTextBoxUpdate()
        {
            tbxNameUpdate.Text = "";
            tbxUnitPriceUpdate.Text = "";
            tbxStockAmountUpdate.Text = "";
        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxNameUpdate.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            tbxUnitPriceUpdate.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            tbxStockAmountUpdate.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _productDal.Delete(new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value)
            });
            LoadProduct();
            MessageBox.Show("Deleted");
            CleanTextBoxUpdate();
        }
        private void SearchProduct(string key)
        {
           var result = _productDal.GetAll().Where(p=>p.Name.ToLower().Contains(key.ToLower())).ToList(); //(Koleksiyon) Buyuk kucuk harf duyarlı oldugundan kucuk harfle arandıgı zaman veri gelmez. o yuzden tolower kullanıyoruz.
          // var result = _productDal.GetByName(key);  (Veri tabani) Bu sekilde arattıgımız zaman herhangi bi sikinti yok.

            dgwProducts.DataSource = result;    
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            SearchProduct(tbxSearch.Text);
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            _productDal.GetById(1);
        }
    }
}
