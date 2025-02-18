using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2022___V2 {
    public partial class Inregistrare : Form {
        Database database = new Database();

        public Inregistrare() {
            InitializeComponent();
        }

        private void Inregistrare_FormClosed(object sender, FormClosedEventArgs e) {
            Autentificare form = new Autentificare();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Hide();
            Autentificare form = new Autentificare();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (textBox1.Text.Length < 4)
                MessageBox.Show("Numele de utilizator trebuie sa aiba cel putin 4 caractere!");
            else if (database.CheckUser(textBox1.Text))
                MessageBox.Show("Numele de utilizator este folosit deja!");
            else if (textBox2.Text.Length < 6)
                MessageBox.Show("Parola trebuie sa aiba cel putin 6 caractere!");
            else if (textBox2.Text != textBox4.Text)
                MessageBox.Show("Parolele trebuie sa coincida!");
            else if (!Regex.IsMatch(textBox3.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                MessageBox.Show("Emailul nu este valid!");
            else {
                database.Register(textBox1.Text, textBox2.Text, textBox3.Text);
                MessageBox.Show("Utilizatorul a fost inregistrat cu succes!");
                this.Hide();
                Autentificare form = new Autentificare();
                form.Show();
            }
        }
    }
}
