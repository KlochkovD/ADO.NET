using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAdapterWizard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = northwindDataSe1.Customers;
            sqlDataAdapter1.Fill(northwindDataSe1.Customers);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            sqlDataAdapter1.Update(northwindDataSe1);
        }
    }
}
