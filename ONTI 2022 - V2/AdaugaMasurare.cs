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
    public partial class AdaugaMasurare : Form {
        Database database = new Database();
        int id, x, y;
        DateTime date;

        public AdaugaMasurare(int id, int x, int y, DateTime date) {
            InitializeComponent();

            this.id = id;
            this.x = x;
            this.y = y;
            this.date = date;
        }

        private async void button1_Click(object sender, EventArgs e) {
            if (textBox1.Text == "")
                MessageBox.Show("Completeaza o valoare!");
            else {
                database.AddMeasurement(id, x, y, Int32.Parse(textBox1.Text), date);

                this.Close();
            }
        }
    }
}
