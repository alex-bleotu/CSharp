using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ONTI_2019___V2.Database;

namespace ONTI_2019___V2 {
    public partial class PrevizualizareCarte : Form {
        Database db = new Database();
        Book book;

        Image img;

        int zoomLevel = 0;

        public PrevizualizareCarte() {
            InitializeComponent();

            //book = db.GetBook(1);

            img = Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\carti\5.jpg");
            pictureBox1.Image = img;

            DrawImage();
        }

        void DrawImage() {
            Bitmap b = new Bitmap(400, 400);

            using (Graphics g = Graphics.FromImage(b)) {
                g.DrawImage(img, 100, 100, 200, 200);

                g.DrawImage(img, new Rectangle(50, 50, 100, 100), new Rectangle(0, 0, 150, 150), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(250, 50, 100, 100), new Rectangle(150, 0, 150, 150), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(50, 250, 100, 100), new Rectangle(0, 150, 150, 150), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(250, 250, 100, 100), new Rectangle(150, 150, 150, 150), GraphicsUnit.Pixel);

                g.DrawImage(img, new Rectangle(25, 25, 50, 50), new Rectangle(0, 0, 75, 75), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(125, 25, 50, 50), new Rectangle(75, 0, 75, 75), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(25, 125, 50, 50), new Rectangle(0, 75, 75, 75), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(125, 125, 50, 50), new Rectangle(75, 75, 75, 75), GraphicsUnit.Pixel);

                g.DrawImage(img, new Rectangle(25, 225, 50, 50), new Rectangle(0, 150, 75, 75), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(125, 225, 50, 50), new Rectangle(75, 150, 75, 75), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(25, 325, 50, 50), new Rectangle(0, 225, 75, 75), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(125, 325, 50, 50), new Rectangle(75, 225, 75, 75), GraphicsUnit.Pixel);

                g.DrawImage(img, new Rectangle(225, 25, 50, 50), new Rectangle(150, 0, 75, 75), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(325, 25, 50, 50), new Rectangle(225, 0, 75, 75), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(225, 125, 50, 50), new Rectangle(150, 75, 75, 75), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(325, 125, 50, 50), new Rectangle(225, 75, 75, 75), GraphicsUnit.Pixel);

                g.DrawImage(img, new Rectangle(225, 225, 50, 50), new Rectangle(150, 150, 75, 75), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(325, 225, 50, 50), new Rectangle(225, 150, 75, 75), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(225, 325, 50, 50), new Rectangle(150, 225, 75, 75), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(325, 325, 50, 50), new Rectangle(225, 225, 75, 75), GraphicsUnit.Pixel);
            }

            pictureBox2.Image = b;
        }

        void DrawZoom() {
            Bitmap b = new Bitmap(400, 400);

            using (Graphics g = Graphics.FromImage(b)) {
                g.DrawImage(img, new Rectangle(0, 0, 400, 400), new Rectangle(zoomLevel * 3, zoomLevel * 3, 300 - 6 * zoomLevel, 300 - 6 * zoomLevel), GraphicsUnit.Pixel);
            }

            pictureBox1.Image = b;
        }

        private void PrevizualizareCarte_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e) {
            zoomLevel++;
            DrawZoom();
        }
    }
}
