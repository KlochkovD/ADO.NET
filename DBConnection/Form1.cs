using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace DBConnection
{
    public partial class Form1 : Form
    {
        OleDbConnection connection = new OleDbConnection();
       // string testConnect = @"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AdventureWorks2019;Data Source=DESKTOP-QAPKAKD\SQLEXPRESS";



        public Form1()
        {
            InitializeComponent();

            this.connection.StateChange += new StateChangeEventHandler(this.connection_StateChange);

        }

        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
                returnValue = settings.ConnectionString;
            return returnValue;
        }

        string testConnect = GetConnectionStringByName("DBConnect.AdventureWorks2019ConnectionString");

        private void connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            button1.Enabled =
                (e.CurrentState == ConnectionState.Closed);
            button2.Enabled =
                (e.CurrentState == ConnectionState.Open);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = testConnect;
                    connection.Open();
                    MessageBox.Show("Соединение с базой данных выполнено успешно");
                }
                else
                    MessageBox.Show("Соединение с базой данных уже установлено");
            }

            catch (Exception Xcp)
            {
                MessageBox.Show(Xcp.Message, "Unexpected Exception",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                MessageBox.Show("Соединение с базой данных закрыто");
            }
            else
                MessageBox.Show("Соединение с базой данных уже закрыто");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConnectionStringSettingsCollection settings =
            ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    MessageBox.Show("name = " + cs.Name);
                    MessageBox.Show("providerName = " + cs.ProviderName);
                    MessageBox.Show("connectionString = " + cs.ConnectionString);
                }
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                MessageBox.Show("Сначала подключитесь к базе");
                return;
            }
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT COUNT(*) FROM Person.ContactType";
            int number = (int)command.ExecuteScalar();
            label1.Text = number.ToString();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                MessageBox.Show("Сначала подключитесь к базе");
                return;
            }

            OleDbCommand command = connection.CreateCommand();
            command.CommandText = "SELECT Name FROM Person.ContactType";
            OleDbDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                listView1.Items.Add(reader["Name"].ToString());
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(testConnect);
            connection.Open();
            OleDbTransaction OleTran = connection.BeginTransaction();
            OleDbCommand command = connection.CreateCommand();
            command.Transaction = OleTran;

            if (connection.State == ConnectionState.Closed)
            {
                MessageBox.Show("Сначала подключитесь к базе");
                return;
            }

            try
            {
                command.CommandText =
              "INSERT INTO Person.ContactType (Name) VALUES('WrongName1')";
                command.ExecuteNonQuery();
                command.CommandText =
               "INSERT INTO Person.ContactType (Name) VALUES('WrongName2')";
                command.ExecuteNonQuery();

                OleTran.Commit();
                MessageBox.Show("Both records were written to database");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                try
                {
                    OleTran.Rollback();
                }
                catch (Exception exRollback)
                {
                    MessageBox.Show(exRollback.Message);
                }

            }
        
            connection.Close();
                   
                     
        }
    }
}
