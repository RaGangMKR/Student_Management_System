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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form1 form = new Form1();
            form.ShowDialog(); this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            tblStudies form = new tblStudies();
            form.ShowDialog(); this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            tblTeachers form = new tblTeachers();
            form.ShowDialog(); this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormLogin form = new FormLogin();
            form.ShowDialog(); this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            tblTimeStudy form = new tblTimeStudy();
            form.ShowDialog(); this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            tblSubjects study = new tblSubjects();
            study.ShowDialog(); this.Hide();
        }
    }
}
