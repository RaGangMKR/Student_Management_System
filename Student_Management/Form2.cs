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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Student_Management
{
    public partial class tblStudies : Form
    {
        private int subjectID;

        public tblStudies()
        {
            InitializeComponent();
            getDatabase();
            getstuid();
            getTeacher();
            getSubject();

        }
        // Connection string 
        string condb = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;";
        private void button1_Click(object sender, EventArgs e)
        {

            var connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;");
            connection.Open();
            // 2. Data manipulation (search by cusID)
            String teacherID = txtSearch.Text; // Assuming textBox1 is used for entering cusID

            // SQL command to search by cusID
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.gettblTeachers(@TeacherID)", connection);
            cmd.Parameters.AddWithValue("@TeacherID", teacherID);


            // Execute the query and retrieve data
            SqlDataReader reader = cmd.ExecuteReader();

            // 3. Check if data exists and display in the ListBox
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string cusName = reader["teacherId"].ToString();
                    string cusAddress = reader["teacherName"].ToString();


                    // Add retrieved data to ListBox
                    listBox1.Items.Add("TeacherId: " + cusName);
                    listBox1.Items.Add("Teachername: " + cusAddress);

                    listBox1.Items.Add("----------------------------"); // Separator between records
                }
            }
            else
            {
                listBox1.Items.Add("No teacher found with this ID.");
            }

            // 4. Close connection
            reader.Close();
            connection.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            var connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;");
            connection.Open();
            // 2. Data manipulation (search by cusID)
            String subjectID = txtSearch.Text; // Assuming textBox1 is used for entering cusID

            // SQL command to search by cusID
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.gettblSubjects(@SubjectID)", connection);
            cmd.Parameters.AddWithValue("@SubjectID", subjectID);


            // Execute the query and retrieve data
            SqlDataReader reader = cmd.ExecuteReader();

            // 3. Check if data exists and display in the ListBox
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string stuID = reader["SubjectId"].ToString();
                    string stuName = reader["SubjectName"].ToString();


                    // Add retrieved data to ListBox
                    listBox1.Items.Add("Subject Id: " + stuID);
                    listBox1.Items.Add("Subject Name: " + stuName);
                    listBox1.Items.Add("-------------------------");
                }
            }
            else
            {
                listBox1.Items.Add("No teacher found with this ID.");
            }

            // 4. Close connection
            reader.Close();
            connection.Close();

        }
        private void button3_Click(object sender, EventArgs e)
        {
           
            Form7 form = new Form7();
            form.ShowDialog(); this.Hide();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;");
            connection.Open();
            // 2. Data manipulation (search by cusID)
            String subjectID = txtSearch.Text; // Assuming textBox1 is used for entering cusID

            // SQL command to search by cusID
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.getStudents(@SubjectID)", connection);
            cmd.Parameters.AddWithValue("@SubjectID", subjectID);


            // Execute the query and retrieve data
            SqlDataReader reader = cmd.ExecuteReader();

            // 3. Check if data exists and display in the ListBox
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string stuId = reader["StuId"].ToString();
                    string stuName = reader["StuName"].ToString();
                    string namekh = reader["namekh"].ToString();
                    string dob = reader["DOB"].ToString();
                    string address = reader["address"].ToString();


                    // Add retrieved data to ListBox
                    listBox1.Items.Add("Subject Id: " + stuId);
                    listBox1.Items.Add("Subject Name: " + stuName);
                    listBox1.Items.Add("Name Khmer: " + namekh);
                    listBox1.Items.Add("DOB: " + dob);
                    listBox1.Items.Add("Address : " + address);
                    listBox1.Items.Add("-------------------------");
                }
            }
            else
            {
                listBox1.Items.Add("No Student found with this ID.");
            }

            // 4. Close connection
            reader.Close();
            connection.Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            var connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;");
            connection.Open();
            // 2. Data manipulation (search by cusID)
            String timeId = txtSearch.Text; // Assuming textBox1 is used for entering cusID

            // SQL command to search by cusID
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.getTimestudy(@timeStudyID)", connection);
            cmd.Parameters.AddWithValue("@timeStudyId", timeId);


            // Execute the query and retrieve data
            SqlDataReader reader = cmd.ExecuteReader();

            // 3. Check if data exists and display in the ListBox
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string timeSId = reader["timeStudyId"].ToString();
                    string time = reader["timestudy"].ToString();


                    // Add retrieved data to ListBox
                    listBox1.Items.Add("Time Id: " + timeSId);
                    listBox1.Items.Add("Time : " + time);
                    listBox1.Items.Add("-------------------------");
                }
            }
            else
            {
                listBox1.Items.Add("No Time found with this ID.");
            }

            // 4. Close connection
            reader.Close();
            connection.Close();

        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(condb))
            {
                try
                {
                    connection.Open();

                    string insertQuery = "InsertStudies";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters

                        cmd.Parameters.AddWithValue("@Subject", comboBox2.Text);
                        cmd.Parameters.AddWithValue("@Teacher", comboBox3.Text);
                        cmd.Parameters.AddWithValue("@Discontinued", checkBox1.Checked ? "1" : "0");
                        cmd.Parameters.AddWithValue("@stuID", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@TimeStudy", dateTimePicker3.Value);
                        cmd.Parameters.AddWithValue("@PaymentDate", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@Enddate", dateTimePicker2.Value);
                        cmd.Parameters.AddWithValue("@Discount", txtDis.Text);
                        cmd.Parameters.AddWithValue("@Fee", txtFee.Text);
                        cmd.Parameters.AddWithValue("@Score", txtScore.Text);



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
        private void getDatabase()
        {
            // 1 open connection
            var connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;");
            //CONNECTTION .OPEN(); AFTER INPUT TABLE IN c#
            connection.Open();
            // 2 data manipulate (select , insert, delete , update)
            var command = new SqlCommand("SELECT * FROM tblStudies", connection);
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
        private void getstuid()
        {
            SqlConnection conn = new SqlConnection(condb);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT StuId from tblStudents", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["StuId"].ToString());
            }
        }
        private void getTeacher()
        {
            SqlConnection conn = new SqlConnection(condb);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Teachername from tblTeachers", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox3.Items.Add(reader["TeacherName"].ToString());
            }
        }
        private void getSubject()
        {
            SqlConnection conn = new SqlConnection(condb);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Subjectname from tblSubjects", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader["Subjectname"].ToString());
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 1 open connection
            SqlConnection connection = new SqlConnection(condb);
            connection.Open();


            String st_id = comboBox1.Text;
            String st_subject = comboBox2.Text;
            String st_Teacher = comboBox3.Text;
            String st_Fee = txtFee.Text;
            String st_Discount = txtDis.Text;
            String st_dis = checkBox1.Checked ? "1" : "0";
            String st_time = dateTimePicker3.Text;
            String st_pay = dateTimePicker1.Text;
            String st_end = dateTimePicker2.Text;
            String st_Score = txtScore.Text;


            var command = new SqlCommand("UpdateStudies", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Fee", st_Fee);
            command.Parameters.AddWithValue("@StuID", st_id);
            command.Parameters.AddWithValue("@Discount", st_Discount);
            command.Parameters.AddWithValue("@Discontinued", st_dis);
            command.Parameters.AddWithValue("@Teacher", st_Teacher);
            command.Parameters.AddWithValue("@TimeStudy", DateTime.Parse(st_time));
            command.Parameters.AddWithValue("@Subject", st_subject);
            command.Parameters.AddWithValue("@Paymentdate", DateTime.Parse(st_pay));
            command.Parameters.AddWithValue("@Enddate", DateTime.Parse(st_end));
            command.Parameters.AddWithValue("@Score", st_Score);


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
            getDatabase();
            return;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(condb))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("DeleteStudies", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Assuming you have a text box or other input to get the Student ID
                        int studyId = int.Parse(comboBox1.Text);
                        cmd.Parameters.AddWithValue("@StuID", studyId);

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
                    connection.Close ();
                   getDatabase();
                   return;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtScore.Clear();
            txtFee.Clear();
            txtDis.Clear();
            txtSearch.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Student_Management;Integrated Security=True;");
            connection.Open();
            // 2. Data manipulation (search by cusID)
            String stuID = txtSearch.Text; // Assuming textBox1 is used for entering cusID

            // SQL command to search by cusID
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.getStudies(@StuID)", connection);
            cmd.Parameters.AddWithValue("@stuId", stuID);


            // Execute the query and retrieve data
            SqlDataReader reader = cmd.ExecuteReader();

            // 3. Check if data exists and display in the ListBox
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string studentId = reader["StuId"].ToString();
                    string TeacherName = reader["Teacher"].ToString();
                    string Subject = reader["Subject"].ToString();
                    string Score = reader["Score"].ToString();


                    // Add retrieved data to ListBox
                    listBox1.Items.Add("StudentId: " + studentId);
                    listBox1.Items.Add("TeacherName: " + TeacherName);
                    listBox1.Items.Add("Subject: " + Subject);
                    listBox1.Items.Add("Score: " + Score);

                    listBox1.Items.Add("----------------------------"); 
                }
            }
            else
            {
                listBox1.Items.Add("No student found with this ID.");
            }

            // 4. Close connection
            reader.Close();
            connection.Close();
        }
    }
}
