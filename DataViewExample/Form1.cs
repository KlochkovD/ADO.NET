using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataViewExample
{
    public partial class Form1 : Form
    {

        DataView customersDataView;
        DataView ordersDataView;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            customersTableAdapter1.Fill(dataSet11.Customers);
            ordersTableAdapter1.Fill(dataSet11.Orders);
            customersDataView = new DataView(dataSet11.Customers);
            ordersDataView = new DataView(dataSet11.Orders);
            customersDataView.Sort = "CustomerID";
            CustomersGrid.DataSource = customersDataView;


        }

        private void SetDataViewPropertiesButton_Click(object sender, EventArgs e)
        {
            customersDataView.Sort = SortTextBox.Text;
            customersDataView.RowFilter = FilterTextBox.Text;

        }

        private void AddRowButton_Click(object sender, EventArgs e)
        {
            DataRowView newCustomRow = customersDataView.AddNew();
            newCustomRow["CustomerID"] = "WINGT";
            newCustomRow["CompanyName"] = "Wing Tip Toys";
            newCustomRow.EndEdit();
        }

        private void GetOrdersButton_Click(object sender, EventArgs e)
        {
            string selectedCustomerID =
            (string)CustomersGrid.SelectedCells[0].OwningRow.Cells["CustomerID"].Value;
            DataRowView selectedRow =
            customersDataView[customersDataView.Find(selectedCustomerID)];
            ordersDataView = selectedRow.CreateChildView(dataSet11.Relations["FK_Orders_Customers"]);
            OrdersGrid.DataSource = ordersDataView;

        }
    }
}
