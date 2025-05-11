using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2019___V2 {
    public partial class StartBiblioteca : Form {
        Database db = new Database();

        public StartBiblioteca() {
            InitializeComponent();

            db.Load();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Hide();
            LogareBiblioteca form = new LogareBiblioteca();
            form.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}
