using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2022___V2 {
    public partial class Logare : Form {
        class User {
            public string name { get; set; }
            public string password { get; set; }
            public User (string n, string p) {
                name = n;
                password = p;
            }
        }

        List<User> users;

        public Logare() {
            InitializeComponent();

            users = new List<User>();

            using (StreamReader reader = new StreamReader(Application.StartupPath + @"\Resurse\Useri.txt")) {
                string line;

                while ((line = reader.ReadLine()) != null) {
                    var fields = line.Split(' ');

                    users.Add(new User(fields[0], fields[1]));
                    comboBox1.Items.Add(fields[0]);
                }
            }

            comboBox1.SelectedIndex = 0;
        }

        bool Verify() {
            if (comboBox1.SelectedIndex == -1 || textBox1.Text == "")
                return false; 

            if (textBox1.Text == users[comboBox1.SelectedIndex].password)
                return true;
            
            MessageBox.Show("Parola introdusa este gresita!");
            textBox1.Text = "";
            return false;
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            if (Verify()) {
                this.Hide();
                InterferenteECO form = new InterferenteECO(pictureBox1.Image, users[comboBox1.SelectedIndex].name);
                form.Show();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e) {
            if (Verify()) {
                this.Hide();
                InterferenteECO form = new InterferenteECO(pictureBox2.Image, users[comboBox1.SelectedIndex].name);
                form.Show();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e) {
            if (Verify()) {
                this.Hide();
                InterferenteECO form = new InterferenteECO(pictureBox3.Image, users[comboBox1.SelectedIndex].name);
                form.Show();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e) {
            if (Verify()) {
                this.Hide();
                InterferenteECO form = new InterferenteECO(pictureBox4.Image, users[comboBox1.SelectedIndex].name);
                form.Show();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e) {
            if (Verify()) {
                this.Hide();
                InterferenteECO form = new InterferenteECO(pictureBox5.Image, users[comboBox1.SelectedIndex].name);
                form.Show();
            }
        }

        private void Logare_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}
