using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2019 {
    public partial class LogareBiblioteca : Form {
        DataBase dataBase;

        public LogareBiblioteca() {
            InitializeComponent();

            dataBase = new DataBase();
        }

        private void button2_Click(object sender, EventArgs e) {
            StartBiblioteca form = new StartBiblioteca();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) {
            int id = dataBase.Login(email.Text, password.Text);

            if (id != -1) {
                BibliotecarBiblioteca form = new BibliotecarBiblioteca(id, password.Text);
                form.Show();
                this.Hide();

                password.Clear();
                email.Clear();
            } else {
                MessageBox.Show("Email si/sau parola invaldia!");
            }
        }
    }
}
