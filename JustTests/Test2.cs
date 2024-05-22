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
    public partial class Test2 : Form {
        public Test2() {
            InitializeComponent();
        }

        private void Test2_FormClosed(object sender, FormClosedEventArgs e) {
            Form1 form = new Form1();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            DialogResult result = MessageBox.Show("Oare?", "Womp womp", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes) {

            }
        }
    }
}
