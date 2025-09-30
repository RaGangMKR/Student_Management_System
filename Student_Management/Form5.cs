using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" && txtPassword.Text == "")
            {
                
                MessageBox.Show("You are not enter something!, please enter you name and password");
                
            }
            else if (txtUsername.Text == "Admin" && txtPassword.Text == "1234")
            {
                Form7 form = new Form7();
                form.ShowDialog();
                this.Hide();
            }else
            {
                MessageBox.Show("Invalid username or password!");
                txtPassword.Clear();
                txtUsername.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
