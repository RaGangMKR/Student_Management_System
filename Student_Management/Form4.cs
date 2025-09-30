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

namespace Student_Management
{
    public partial class tblSubjects : Form
    {
        public tblSubjects()
        {
            InitializeComponent();
            getDatabase();
        }
        String ConDB = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;";
        private void getDatabase()
        {
            // 1 open connection
            SqlConnection connection = new SqlConnection(ConDB);
            //CONNECTTION .OPEN(); AFTER INPUT TABLE IN c#
            connection.Open();
            // 2 data manipulate (select , insert, delete , update)
            var command = new SqlCommand("SELECT * FROM tblSubjects", connection);
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
        
        private void btnNew_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            txtSearch.Clear();
            dataGridView1.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection connection = new SqlConnection(ConDB))
            {
                try
                {
                    connection.Open();

                    string insertQuery = "InsertSubjects";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@SubjectName", txtName.Text);
                        cmd.Parameters.AddWithValue("@SubjectID", txtId.Text);

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

        private void btnExit_Click(object sender, EventArgs e)
        {

            Form7 form = new Form7();
            form.ShowDialog(); this.Hide();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 1 open connection
            SqlConnection connection = new SqlConnection(ConDB);
            connection.Open();
            String S_id = txtId.Text;
            String S_name = txtName.Text;

            SqlCommand cmd = new SqlCommand("UpdateSubject", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SubjectId", S_id);
            cmd.Parameters.AddWithValue("@SubjectName", S_name);
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Update successfully!");
            }
            else
            {
                MessageBox.Show("Invalid Subject name or id, please try again!");
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
                SqlCommand cmd = new SqlCommand("DeleteSubjects", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                string t_id = txtId.Text;
                cmd.Parameters.AddWithValue("@SubjectId", t_id);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Delete Successfully!");
                }
                else
                {
                    MessageBox.Show("Subject not found, try again!");
                }
                conn.Close();
                getDatabase();
                return;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(search))
            {
                getDatabase();
                return;
            }
            using (var connection = new SqlConnection(ConDB))
            {
                try
                {
                    connection.Open();

                    using (var command = new SqlCommand("SeacrhSubjects", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@search", "%" + search + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Bind data to the DataGridView
                        if (dt.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            dataGridView1.DataSource = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // show message when some error
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
