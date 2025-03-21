using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2024 {
    public partial class Auth : Form {
        Database db = new Database();

        public Auth() {
            InitializeComponent();

            //db.Refresh();
            //db.Init();

            var text = System.IO.File.ReadAllText(Application.StartupPath + "\\saved.txt");
            if (text != "")
                textBox1.Text = text.Trim();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Hide();
            Register form = new Register();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e) {
            User user = db.Login(textBox1.Text, textBox2.Text);

            if (user != null) {
                this.Hide();
                Security form = new Security(user, checkBox1.Checked ? 2 : 0);
                form.Show();
            } else {
                MessageBox.Show("Eroare de autentificare!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void Auth_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}
