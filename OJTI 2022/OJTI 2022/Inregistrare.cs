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

namespace OJTI_2022
{
    public partial class Inregistrare : Form
    {
        DataBase dataBase;

        public Inregistrare()
        {
            InitializeComponent();
            dataBase = new DataBase();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Autentificare form = new Autentificare();
            form.Show();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            if (usernameTextBox.Text.Length < 5)
                MessageBox.Show("Numele de utilizator trebuie sa fie mai lung de 4 caractere.");
            else if (passwordTextBox.Text.Length < 6)
                MessageBox.Show("Parola trebuie sa aiba minim 6 caractere.");
            else if (!regex.IsMatch(emailTextBox.Text))
                MessageBox.Show("Emailul nu este valid.");
            else if (passwordTextBox.Text != confirmPasswordTextBox.Text)
                MessageBox.Show("Parolele nu coincid.");
            else if (dataBase.CheckUserExist(usernameTextBox.Text))
                MessageBox.Show("Numele de utilizator exista deja.");
            else
            {
                dataBase.Register(usernameTextBox.Text, passwordTextBox.Text, emailTextBox.Text);
                usernameTextBox.Clear();
                passwordTextBox.Clear();
                emailTextBox.Clear();

                this.Hide();
                Autentificare form = new Autentificare();
                form.Show();
            }
        }

        private void Inregistrare_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Autentificare form = new Autentificare();
            form.Show();
        }
    }
}
