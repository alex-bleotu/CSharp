using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI_2023
{
    public partial class Autentificare : Form
    {
        DataBase dataBase;

        public Autentificare()
        {
            InitializeComponent();
            dataBase = new DataBase();
            dataBase.Populate();

            this.Hide();
            Sarpe form = new Sarpe("");
            form.Show();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            bool action = dataBase.Login(emailTextBox.Text, passwordTextBox.Text);
            if (action)
            {
                this.Hide();
                AlegeJoc alegeJoc = new AlegeJoc(emailTextBox.Text);
                alegeJoc.Show();
                emailTextBox.Clear();
                passwordTextBox.Clear();
            }
            else
            {
                emailTextBox.Clear();
                passwordTextBox.Clear();
                MessageBox.Show("Date de autentificare invalide!");
            }
        }
    }
}
