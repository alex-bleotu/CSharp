using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2018 {
    public partial class Form1 : Form {
        DataBase dataBase;

        public Form1(int id) {
            InitializeComponent();

            dataBase = new DataBase();
            dataBase.Populate();

            if (id != -1) {
                button2.Visible = false;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Hide();
            Logare form = new Logare();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Hide();
            VizualizareLectii form = new VizualizareLectii();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e) {
            this.Hide();
            GhicesteRegiunea form = new GhicesteRegiunea();
            form.Show();
        }
    }
}
