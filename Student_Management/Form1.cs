using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Student_Management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            getDatabase();
        }
        private void getDatabase()
        {
            // 1 open connection
            var connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;");
            //CONNECTTION .OPEN(); AFTER INPUT TABLE IN c#
            connection.Open();
            // 2 data manipulate (select , insert, delete , update)
            var command = new SqlCommand("SELECT * FROM tblStudents", connection);
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

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "png|*.png|jpeg|*.jpg|bmp|*.bmp|all files|*.*";
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // 1 open connection
            var connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;");
            connection.Open();
         
            var command = new SqlCommand("SELECT * FROM tblStudents", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
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
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string insertQuery = "InsertStudents";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@stName", txtName.Text);
                        cmd.Parameters.AddWithValue("@NameKh", txtNameKh.Text);
                        cmd.Parameters.AddWithValue("@Sex", radioButton1.Checked ? "Male" : "Female");
                        cmd.Parameters.AddWithValue("@InvoiceID", txtInvoiceId.Text); 
                        cmd.Parameters.AddWithValue("@DOB", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@POB", txtPob.Text);
                        cmd.Parameters.AddWithValue("@ContactNumber", txtContact.Text);
                        cmd.Parameters.AddWithValue("@Addrees", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@Comments", txtCom.Text);

                        // Handle photo
                        if (pictureBox1.Image != null)
                        {
                            using (var ms = new System.IO.MemoryStream())
                            {
                                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                                cmd.Parameters.AddWithValue("@Photo", ms.ToArray());
                            }
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Photo", DBNull.Value);
                        }

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
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtId.Clear();
            txtContact.Clear();
            txtCom.Clear();
            txtNameKh.Clear();
            txtName.Clear();
            txtInvoiceId.Clear();
            txtPob.Clear();
            dataGridView1.ClearSelection();
            dateTimePicker1 = null;
            comboBox1 = null;
            radioButton1 = null;
            radioButton2 = null;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 1 open connection
            var connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;");
            connection.Open();

            // 2 data manipulate (select , insert, delete , update)
            String st_id = txtId.Text;
            String st_name = txtName.Text;
            String st_namekh = txtNameKh.Text;
            String st_invoice_id = txtInvoiceId.Text;
            String st_address = comboBox1.Text;
            String st_sex = radioButton1.Checked ? "Male" : "Female";
            String st_dob = dateTimePicker1.Text;
            String st_pob = txtPob.Text;
            String st_contact = txtContact.Text;
            String st_com = txtCom.Text;


            var command = new SqlCommand("UpdateStudents", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@StuName", st_name);
            command.Parameters.AddWithValue("@StuID", st_id);
            command.Parameters.AddWithValue("@Address", st_address);
            command.Parameters.AddWithValue("@InvoiceID", st_invoice_id);
            command.Parameters.AddWithValue("@NameKh", st_namekh);
            command.Parameters.AddWithValue("@ContactNumber", st_contact);
            command.Parameters.AddWithValue("@Sex", st_sex);
            command.Parameters.AddWithValue("@POB", st_pob);
            command.Parameters.AddWithValue("@Comments", st_com);

            MemoryStream stream = new MemoryStream();
            pictureBox1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] photo = stream.ToArray();
            command.Parameters.AddWithValue("@Photo", photo);
            command.Parameters.AddWithValue("@DOB", DateTime.Parse(st_dob));
      
            if (command.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Update success!");
            }
            else
            {
                MessageBox.Show("Can not update!");
            }
            // 3 close connection
            connection.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
            Form7 form = new Form7();
            form.ShowDialog(); this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("DeleteStudents", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Assuming you have a text box or other input to get the Student ID
                        int studentId = int.Parse(txtId.Text); 
                        cmd.Parameters.AddWithValue("@StuID", studentId);

                        // Execute the command
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Delete successful!");
                        }
                        else
                        {
                            MessageBox.Show("No record found with that Student ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
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
            using (var connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;"))
            {
                try
                {
                    connection.Open();

                    using (var command = new SqlCommand("SeacrhStudents", connection))
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
