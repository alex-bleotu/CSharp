using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JustTests {
    public partial class Test3 : Form {
        public Test3() {
            InitializeComponent();
        }

        private void Test3_FormClosed(object sender, FormClosedEventArgs e) {
            Form1 form = new Form1();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            using (SaveFileDialog save = new SaveFileDialog()) {
                save.FileName = "image.png";
                save.InitialDirectory = Application.StartupPath;

                if (save.ShowDialog() == DialogResult.OK) {
                    MessageBox.Show(save.FileName);

                    Bitmap bitmap = new Bitmap(100, 100);
                    panel1.DrawToBitmap(bitmap, new Rectangle(0, 0, panel1.Width, panel1.Height));

                    bitmap.Save(save.FileName);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            using (OpenFileDialog open = new OpenFileDialog()) {
                open.InitialDirectory = Application.StartupPath;

                if (open.ShowDialog() == DialogResult.OK) {
                    MessageBox.Show(open.FileName);
                }
            }
        }
    }
}
