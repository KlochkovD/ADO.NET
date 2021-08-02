using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZachetnoeZadanie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = northwindDataSet1.Customers;
            sqlDataAdapter1.Fill(northwindDataSet1.Customers);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlDataAdapter1.Update(northwindDataSet1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
