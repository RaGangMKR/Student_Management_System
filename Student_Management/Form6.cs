using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Student_Management
{
    public partial class tblTimeStudy : Form
    {
        public tblTimeStudy()
        {
            InitializeComponent();
            getDatabase();
        }
        String ConDB = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;";
        private void getDatabase()
        {
            // 1 open connection
            var connection = new SqlConnection(ConDB);
            //CONNECTTION .OPEN(); AFTER INPUT TABLE IN c#
            connection.Open();
            // 2 data manipulate (select , insert, delete , update)
            var command = new SqlCommand("Select * from tblTimeStudy", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                //dataGridView1.Visible = true;
                dataGridView1.ColumnHeadersVisible = true;
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
            }
            // 3 close connection
            connection.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(ConDB))
            {
                try
                {
                    connection.Open();

                    string insertQuery = "InsertTimeStudy";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Add parameters
                        cmd.Parameters.AddWithValue("@timeStudyID", txtId.Text);
                        cmd.Parameters.AddWithValue("@timeStudy", dateTimePicker1.Value);
                        

                        // Execute the command
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Insert successful!");
                        }
                        else
                        {
                            MessageBox.Show("Insert failed. Please try again.");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");

                }
                connection.Close();
                getDatabase();
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 1 open connection
            SqlConnection connection = new SqlConnection(ConDB);
            connection.Open();
            String time_id = txtId.Text;
            String time = dateTimePicker1.Text;

            SqlCommand cmd = new SqlCommand("UpdateTime", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@timeStudyId", time_id);
            cmd.Parameters.AddWithValue("@timeStudy", DateTime.Parse(time));
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Update successfully!");
            }
            else
            {
                MessageBox.Show("Can't update please try again!");
            }
            connection.Close();
            getDatabase();
            return;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConDB))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DeleteTimestudy", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                string t_id = txtId.Text;
                cmd.Parameters.AddWithValue("@timestudyId", t_id);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Delete Successfully!");
                }
                else
                {
                    MessageBox.Show("Time not found, try again!");
                }
                conn.Close();
                getDatabase();
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            Form7 form = new Form7();
            form.ShowDialog(); this.Hide();
        }
    }
}
