using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2022 {
    public partial class Logare : Form {
        public Logare() {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
        }

        void Move(int id) {
            InterferenteECO form = new InterferenteECO(id);
            form.Show();
            this.Hide();
        }

        bool Login() {
            if (comboBox1.SelectedItem == "Ioana" && textBox1.Text == "eco") 
                return true;
            if (comboBox1.SelectedItem == "Radu" && textBox1.Text == "123")
                return true;
            if (comboBox1.SelectedItem == "Maria" && textBox1.Text == "abc")
                return true;
            if (comboBox1.SelectedItem == "Florin" && textBox1.Text == "a")
                return true;
            if (comboBox1.SelectedItem == "Mihai" && textBox1.Text == "tg")
                return true;

            MessageBox.Show("Parola este incorecta");

            return false;
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            if (Login())
                Move(0);
        }

        private void pictureBox2_Click(object sender, EventArgs e) {
            if (Login())
                Move(1);
        }

        private void pictureBox3_Click(object sender, EventArgs e) {
            if (Login())
                Move(2);
        }

        private void pictureBox4_Click(object sender, EventArgs e) {
            if (Login())
                Move(3);
        }

        private void pictureBox5_Click(object sender, EventArgs e) {
            if (Login())
                Move(4);
        }
    }
}
