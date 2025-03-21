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

namespace ONTI_2024 {
    public partial class Register : Form {
        Database db = new Database();

        public Register() {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Hide();
            Auth form = new Auth();
            form.Show();
        }

        private void Register_FormClosed(object sender, FormClosedEventArgs e) {
            Auth form = new Auth();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e) {
            bool eraseEmail = false, erasePassword = false, notGood = false, eraseDate = false;
            if (!Regex.IsMatch(textBox1.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                eraseEmail = true;
            if (db.CheckUserExists(textBox1.Text))
                eraseEmail = true;
            if (textBox2.Text == "" || textBox4.Text == "")
                notGood = true;
            if (textBox3.Text.Length < 6 || !Regex.IsMatch(textBox3.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$") || textBox3.Text != textBox5.Text)
                erasePassword = true;
            if (dateTimePicker1.Value.AddYears(7) > DateTime.Now)
                eraseDate = true;

            if (eraseEmail || erasePassword || notGood || eraseDate) {
                MessageBox.Show("Date de inregistrare incorecte!");

                if (eraseEmail) textBox1.Clear();
                if (erasePassword) {
                    textBox3.Clear();
                    textBox5.Clear();
                }
                if (eraseDate)
                    dateTimePicker1.Value = DateTime.Now;
            } else {
                User user = new User(textBox1.Text, textBox2.Text, textBox4.Text, db.Encrypt(textBox3.Text), dateTimePicker1.Value);

                this.Hide();

                Security form = new Security(user, 1);
                form.Show();
            }
        }

        private void textBox3_Leave(object sender, EventArgs e) {
            textBox3.Text = textBox3.Text.Trim();
        }

        private void textBox5_Leave(object sender, EventArgs e) {
            textBox5.Text = textBox5.Text.Trim();
        }
    }
}
