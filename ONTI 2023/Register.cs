using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2023
{
    public partial class Register : Form
    {
        Database database = new Database();
        public Register()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (emailTextBox.Text == "" || nameTextBox.Text == "" || passwordTextBox.Text == "" || confirmTextBox.Text == "")
                MessageBox.Show("Completeaza toate datele");

            if (database.EmailExists(emailTextBox.Text))
                MessageBox.Show("Emailul a fost deja utilizat.");
            else if (passwordTextBox.Text != confirmTextBox.Text)
                MessageBox.Show("Parolele nu coincid");
            else if (!Regex.IsMatch(emailTextBox.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                MessageBox.Show("Emailul nu are formatul necesar.");
            else
            {
                database.Register(emailTextBox.Text, nameTextBox.Text, passwordTextBox.Text);
                emailTextBox.Clear();
                nameTextBox.Clear();
                passwordTextBox.Clear();
                confirmTextBox.Clear();

                this.Hide();
                Auth form = new Auth();
                form.Show();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            emailTextBox.Clear();
            nameTextBox.Clear();
            passwordTextBox.Clear();
            confirmTextBox.Clear();

            this.Hide();
            Auth form = new Auth();
            form.Show();
        }
    }
}
