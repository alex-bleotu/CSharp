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
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Hide();
            Test1 form = new Test1();
            form.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Hide();
            Test2 form = new Test2();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e) {
            this.Hide();
            Test3 form = new Test3();
            form.Show();
        }
    }
}
