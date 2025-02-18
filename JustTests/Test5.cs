using System;
using System.Drawing;
using System.Windows.Forms;

namespace JustTests {
    public partial class Test5 : Form {
        private Bitmap gifBitmap;

        public Test5() {
            InitializeComponent();

            gifBitmap = new Bitmap(Application.StartupPath + @"\gif.gif");

            ImageAnimator.Animate(gifBitmap, OnFrameChanged);

            pictureBox1.Image = gifBitmap;
        }

        private void OnFrameChanged(object sender, EventArgs e) {
            ImageAnimator.UpdateFrames();

            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            if (gifBitmap != null) {
                e.Graphics.DrawImage(gifBitmap, new Point(0, 0));
            }
        }

        private void Test5_FormClosed(object sender, FormClosedEventArgs e) {
            ImageAnimator.StopAnimate(gifBitmap, OnFrameChanged);

            Form1 form = new Form1();
            form.Show();
        }
    }
}
