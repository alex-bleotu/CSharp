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
    public partial class Dialog : Form {
        public int direction { get; private set; }

        public Dialog() {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e) {
            direction = 0;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e) {
            direction = 2;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            direction = 3;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            direction = 1;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
