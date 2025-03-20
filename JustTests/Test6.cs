using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JustTests {
    public partial class Test6 : Form {
        Database db = new Database();

        public Test6() {
            InitializeComponent();
        }

        private void Test6_FormClosed(object sender, FormClosedEventArgs e) {
            Form1 form = new Form1();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (textBox1.Text != "" && textBox2.Text != "") {
                if (db.CheckIfUserExists(textBox1.Text)) {
                    MessageBox.Show("User already exists");
                    return;
                }

                bool result = db.Register(textBox1.Text.Trim(), textBox2.Text.Trim());
                if (result) MessageBox.Show("Registered");
                else MessageBox.Show("Failed");
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (textBox3.Text != "" && textBox4.Text != "") {
                int id = db.Login(textBox3.Text.Trim(), textBox4.Text.Trim());
                if (id != -1) MessageBox.Show("Logged in");
                else MessageBox.Show("Failed");
            }
        }
    }
}
