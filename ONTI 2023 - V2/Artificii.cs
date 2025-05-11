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
    public partial class Artificii : Form {
        int time = 0;
        Random r;

        List<Tuple<int, int>> points = new List<Tuple<int, int>>();

        JocMemorie f;

        public Artificii(JocMemorie form) {
            InitializeComponent();

            r = new Random();
            pictureBox1.Image = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            f = form;

            timer1.Start();
        }

        void Draw() {
            Bitmap b = new Bitmap(pictureBox1.Image);

            using (Graphics g = Graphics.FromImage(b)) {
                int x = r.Next(490);
                int y = r.Next(320);
                int a = r.Next(33);
                a++;

                Image img;

                if (a < 10)
                    img = Image.FromFile(Application.StartupPath + @"\Resurse\Artificii\artificie_0" + a + ".png");
                else
                    img = Image.FromFile(Application.StartupPath + @"\Resurse\Artificii\artificie_" + a + ".png");

                g.DrawImage(img, new Point(x, y));
            }

            pictureBox1.Image = b;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            time++;

            if (time == 20) {
                timer1.Stop();
                this.Hide();
                f.Show();
            }

            Draw();
        }

        private void Artificii_FormClosed(object sender, FormClosedEventArgs e) {
            f.Show();
        }
    }
}
