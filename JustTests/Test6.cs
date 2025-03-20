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
        public Test6() {
            InitializeComponent();
        }

        private void Test6_FormClosed(object sender, FormClosedEventArgs e) {
            Form1 form = new Form1();
            form.Show();
        }
    }
}
