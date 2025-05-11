using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2023___V2 {
    public partial class Inregistrare : Form {
        Database db = new Database();

        public Inregistrare() {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Hide();
            Autentificare form = new Autentificare();
            form.Show();
        }

        private void Inregistrare_FormClosed(object sender, FormClosedEventArgs e) {
            this.Hide();
            Autentificare form = new Autentificare();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                MessageBox.Show("Completeaza toate campurile!");
            else if (textBox3.Text != textBox4.Text)
                MessageBox.Show("Parola nu coincide!");
            else {
                try {
                    MailAddress mail = new MailAddress(textBox1.Text);

                    if (db.CheckIfEmailExists(textBox1.Text))
                        MessageBox.Show("Emailul a mai fost folosit!");
                    else {
                        db.Register(textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim());

                        this.Hide();
                        Autentificare form = new Autentificare();
                        form.Show();
                    }
                } catch {
                    MessageBox.Show("Emailul este invalid!");
                }
            }
        }
    }
}
