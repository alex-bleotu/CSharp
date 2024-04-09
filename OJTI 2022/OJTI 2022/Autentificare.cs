using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI_2022
{
    public partial class Autentificare : Form
    {
        DataBase dataBase;

        public Autentificare()
        {
            InitializeComponent();

            dataBase = new DataBase();
            dataBase.Populate();
        }

        private void Autentificare_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == "" || passwordTextBox.Text == "")
                return;

            if (dataBase.Login(usernameTextBox.Text, passwordTextBox.Text))
            {
                this.Hide();
                Vizualizare form = new Vizualizare(usernameTextBox.Text);
                usernameTextBox.Clear();
                passwordTextBox.Clear();
                form.Show();
            } else
                MessageBox.Show("Nume de utilizator si/sau parola invalida!");
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inregistrare form = new Inregistrare();
            form.Show();
        }
    }
}
