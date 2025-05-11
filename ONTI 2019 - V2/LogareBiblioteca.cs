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
    public partial class LogareBiblioteca : Form {
        Database db = new Database();

        public LogareBiblioteca() {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Hide();
            StartBiblioteca form = new StartBiblioteca();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (textBox1.Text != "" && textBox2.Text != "") {
                int id = db.Login(textBox1.Text.Trim(), textBox2.Text.Trim());

                if (id != -1) {
                    this.Hide();
                    BibliotecarBiblioteca form = new BibliotecarBiblioteca(id);
                    form.Show();
                }
                else MessageBox.Show("Email si/ sau parola invalida!");
            }
        }
    }
}
