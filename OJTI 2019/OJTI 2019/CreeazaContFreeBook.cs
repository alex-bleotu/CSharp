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

namespace OJTI_2019
{
    public partial class CreeazaContFreeBook : Form
    {
        DataBase dataBase;

        public CreeazaContFreeBook()
        {
            InitializeComponent();
            dataBase = new DataBase();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);

            if (nameTextBox.Text == "")
                MessageBox.Show("Numele trebuie completat");
            else if (prenumeTextBox.Text == "")
                MessageBox.Show("Prenumele trebuie completat.");
            else if (parolaTextBox.Text == "")
                MessageBox.Show("Parola trebuie completata.");
            else if (parolaTextBox.Text != confirmTextBox.Text)
                MessageBox.Show("Parolele sunt diferite.");
            else if (!regex.IsMatch(emailTextBox.Text))
                MessageBox.Show("Emailul nu este valid.");
            else if (dataBase.EmailExists(emailTextBox.Text))
                MessageBox.Show("Emailul deja exista.");
            else
            {
                dataBase.Register(emailTextBox.Text, parolaTextBox.Text, nameTextBox.Text, prenumeTextBox.Text);

                emailTextBox.Clear();
                parolaTextBox.Clear();
                prenumeTextBox.Clear();
                prenumeTextBox.Clear();

                this.Hide();
                FreeBookHome form = new FreeBookHome(false);
                form.Show();
            }
        }

        private void CreeazaContFreeBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            FreeBookHome form = new FreeBookHome(false);
            form.Show();
        }
    }
}
