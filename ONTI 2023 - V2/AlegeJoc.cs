using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2023___V2 {
    public partial class AlegeJoc : Form {
        Database db = new Database();

        string email, name;

        public AlegeJoc(string email, string name) {
            InitializeComponent();

            this.email = email;
            this.name = name;

            label1.Text = "Bine ai venit " + name + "(" + email + ")!";

            List<Score> first = db.GetFirstScores(email);
            List<Score> second = db.GetSecondsScores(email);

            foreach (var s in first)
                chart1.Series[0].Points.AddXY(s.date, s.score);

            foreach (var s in second)
                chart1.Series[1].Points.AddXY(s.date, s.score);
        }

        private void AlegeJoc_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e) {
            this.Hide();
            PrimQR form = new PrimQR(email, name);
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Hide();
            JocLitere form = new JocLitere(email, name);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Hide();
            JocMemorie form = new JocMemorie(email, name);
            form.Show();
        }
    }
}
