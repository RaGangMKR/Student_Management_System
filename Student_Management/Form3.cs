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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Student_Management
{
    public partial class tblTeachers : Form
    {
        public tblTeachers()
        {
            InitializeComponent();
            getDatabase();
            
        }
        String conDB = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;";
        private void getDatabase()
        {
            var connection = new SqlConnection(conDB);
            //CONNECTTION .OPEN(); AFTER INPUT TABLE IN c#
            connection.Open();
            // 2 data manipulate (select , insert, delete , update)
            var command = new SqlCommand("SELECT * FROM tblTeachers", connection);
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


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    
                    string insertQuery = "InsertTeachers";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@TeacherName", txtName.Text);
                        cmd.Parameters.AddWithValue("@TeacherID", txtId.Text);

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

        private void button4_Click(object sender, EventArgs e)
        {
            // 1 open connection
            var connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;");
            connection.Open();
            String teacher_id = txtId.Text;
            String teacher_name = txtName.Text;

            SqlCommand cmd = new SqlCommand("UpdateTeachers", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@teacherId", teacher_id);
            cmd.Parameters.AddWithValue("@teacherName", teacher_name);
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Update successfully!");
            }
            else
            {
                MessageBox.Show("Invalid teacher name or id, please try again!");
            }
            connection.Close();
            getDatabase();
            return;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(conDB))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DeleteTeachers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                string t_id = txtId.Text;
                cmd.Parameters.AddWithValue("@teacherId", t_id);
                if(cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Delete Successfully!");
                }
                else
                {
                    MessageBox.Show("Teacher not found, try again!");
                }
                conn.Close();
                getDatabase();
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            Form7 form = new Form7();
            form.ShowDialog();  this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            txtSearch.Clear();
            dataGridView1.ClearSelection();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(search))
            {
                getDatabase();
                return;
            }
            using (var connection = new SqlConnection(conDB))
            {
                try
                {
                    connection.Open();

                    using (var command = new SqlCommand("SeacrhTeachers", connection))
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
