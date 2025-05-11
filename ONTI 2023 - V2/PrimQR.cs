using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ONTI_2023___V2 {
    public partial class PrimQR : Form {
        Database db = new Database();

        string email, name;

        bool[] euclid = new bool[1000];

        List<Tuple<string, int>> scores;

        public PrimQR(string email, string name) {
            InitializeComponent();

            this.email = email;
            this.name = name;

            scores = db.GetAllScores();

            Euclid();
        }

        void Euclid() {
            euclid[0] = true;
            euclid[1] = true;
            euclid[2] = false;
            int i = 4;
            while (i < 1000) {
                euclid[i] = true;
                i += 2;
            }
            for (i = 3; i < 100; i += 2) {
                if (!euclid[i]) {
                    int j = i + i;
                    while (j < 1000) {
                        euclid[j] = true;
                        j += i;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            int max = -1, maxScore = 0, nr = 0;
            string maxEmail = "";
            foreach (var score in scores) {
                int counter = 0;
                int i = score.Item2;
                while (i < 1000 && euclid[i]) {
                    i++;
                    counter++;
                }
                if (counter > max) {
                    max = counter;
                    maxScore = score.Item2;
                    maxEmail = score.Item1;
                    nr = i;
                } else if (counter == max && String.Compare(score.Item1, maxEmail) < 0) {
                    max = counter;
                    maxScore = score.Item2;
                    maxEmail = score.Item1;
                    nr = i;
                }
            }

            MessagingToolkit.QRCode.Codec.QRCodeEncoder encoder = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();
            encoder.QRCodeScale = 8;

            Bitmap b = encoder.Encode(email + "\n" + maxScore + "\n" + nr);

            using (Graphics g = Graphics.FromImage(b)) {
                g.DrawImage(Image.FromFile(Application.StartupPath + @"\Resurse\Prim\Logo_C#.png"), new Rectangle(140, 140, 60, 60));
            }

            pictureBox1.Image = b;
        }

        private void PrimQR_FormClosed(object sender, FormClosedEventArgs e) {
            this.Hide();
            AlegeJoc form = new AlegeJoc(email, name);
            form.Show();
        }
    }
}
