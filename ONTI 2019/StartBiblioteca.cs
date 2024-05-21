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
    public partial class StartBiblioteca : Form {
        DataBase dataBase;

        public StartBiblioteca() {
            InitializeComponent();

            dataBase = new DataBase();
            dataBase.Populate();
        }

        private void button1_Click(object sender, EventArgs e) {
            LogareBiblioteca form = new LogareBiblioteca();
            form.Show();
            this.Hide();
        }
    }
}
