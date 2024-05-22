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
    public partial class Test1 : Form {
        public Test1() {
            InitializeComponent();
        }

        private void Test1_FormClosed(object sender, FormClosedEventArgs e) {
            Form1 form = new Form1();
            form.Show();
        }

        private void deconectareToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("Deconectare");
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("Iesire");
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) {
        }

        private void admin2ToolStripMenuItem_Click(object sender, EventArgs e) {
        }
    }
}
