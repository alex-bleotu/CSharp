using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2022___V2 {
    public partial class Autentificare : Form {
        Database database = new Database();

        public Autentificare() {
            InitializeComponent();

            database.Populate();
        }

        private void button2_Click(object sender, EventArgs e) {
            Inregistrare form = new Inregistrare();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (textBox1.Text != "" || textBox2.Text != "") {
                int id = database.Login(textBox1.Text, textBox2.Text);

                if (id != -1) {
                    Vizualizare form = new Vizualizare(id);
                    form.Show();
                    this.Hide();

                    textBox1.Clear();
                    textBox2.Clear();
                }
                else MessageBox.Show("Nume de utilizator si/sau parola invalida!");
            }
        }

        private void Autentificare_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}
