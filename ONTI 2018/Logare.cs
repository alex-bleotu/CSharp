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
    public partial class Logare : Form {
        DataBase dataBase;

        public Logare() {
            InitializeComponent();

            dataBase = new DataBase();
        }

        private void button1_Click(object sender, EventArgs e) {
            int id = dataBase.Login(textBox1.Text, textBox2.Text);

            if (id != -1) {
                this.Hide();
                Form1 form = new Form1(id);
                form.Show();

                textBox1.Clear();
                textBox2.Clear();
            } else {
                MessageBox.Show("Emailul si/sau parola sunt invalide.");
            }
        }
    }
}
