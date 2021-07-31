using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DBCommand
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder results = new System.Text.StringBuilder();
            sqlCommand1.CommandType = CommandType.Text;
            sqlConnection1.Open();
            SqlDataReader reader = sqlCommand1.ExecuteReader();
            bool MoreResults = false;
            do
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        results.Append(reader[i].ToString() + "\t");
                    }
                    results.Append(Environment.NewLine);
                }
                MoreResults = reader.NextResult();
            } while (MoreResults);

            reader.Close();
            sqlCommand1.Connection.Close();
            textBox1.Text = results.ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder results = new System.Text.StringBuilder();
            sqlCommand2.CommandType = CommandType.StoredProcedure;
            sqlCommand2.CommandText = "spo";
            sqlCommand2.Connection.Open();
            SqlDataReader reader = sqlCommand2.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    results.Append(reader[i].ToString() + "\t");
                }
                results.Append(Environment.NewLine);
            }
            reader.Close();
            sqlCommand2.Connection.Close();
            textBox1.Text = results.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                sqlCommand3.CommandType = CommandType.Text;

                {

                    sqlCommand3.CommandType = CommandType.Text;
                    sqlCommand3.CommandText = "CREATE TABLE SalesPersons (" +
                           "[SalesPersonID] [int] IDENTITY(1,1) NOT NULL, " +
                           "[FirstName] [nvarchar](50)  NULL, " +
                           "[LastName] [nvarchar](50)  NULL)";
                    sqlCommand3.Connection.Open();
                    sqlCommand3.ExecuteNonQuery();
                    sqlCommand3.Connection.Close();
                    MessageBox.Show("Таблица SalesPersons создана");
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                sqlCommand3.Connection.Close();
            }
            }
        

        private void button4_Click(object sender, EventArgs e)
        {
            
            System.Text.StringBuilder results = new System.Text.StringBuilder();
            sqlCommand4.CommandType = CommandType.Text;
            sqlCommand4.Parameters["@City"].Value = CityTextBox.Text;
            sqlConnection1.Open();
            SqlDataReader reader = sqlCommand4.ExecuteReader();
            bool MoreResults = false;
            do
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        results.Append(reader[i].ToString() + "\t");
                    }
                    results.Append(Environment.NewLine);
                }
                MoreResults = reader.NextResult();
            } while (MoreResults);
            reader.Close();
            sqlCommand4.Connection.Close();
            CityTextBox.Text = results.ToString();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder results = new System.Text.StringBuilder();
            sqlCommand5.Parameters["@ContactTypeID"].Value = OrdYearTextBox.Text;
            sqlCommand5.Connection.Open();
         
            SqlDataReader reader = sqlCommand5.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    results.Append(reader[i].ToString() + "\t");
                }
                results.Append(Environment.NewLine);
            }
            reader.Close();
            sqlCommand5.Connection.Close();
            CategoryNameTextBox.Text = results.ToString();
            

            System.Text.StringBuilder results2 = new System.Text.StringBuilder();
            sqlCommand6.Parameters["@Name"].Value = OrdYearTextBox.Text;
            sqlCommand6.Connection.Open();

            SqlDataReader reader2 = sqlCommand6.ExecuteReader();
            while (reader2.Read())
            {
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    results2.Append(reader2[i].ToString() + "\t");
                }
                results2.Append(Environment.NewLine);
            }
            reader.Close();
            sqlCommand6.Connection.Close();
           
            OrdYearTextBox.Text = results2.ToString();

        }
    }
}
