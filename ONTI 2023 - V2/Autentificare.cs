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
    public partial class Autentificare : Form {
        Database db = new Database();

        public Autentificare() {
            InitializeComponent();

            //db.Load();
        }

        private void button2_Click(object sender, EventArgs e) {
            string name = db.Login(textBox1.Text.Trim(), textBox2.Text.Trim());
                
            if (name != null) {
                this.Hide();
                AlegeJoc form = new AlegeJoc(textBox1.Text.Trim(), name);
                form.Show();
            }
            else {
                MessageBox.Show("Date de autentificare invalide!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void Autentificare_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e) {
            this.Hide();
            Inregistrare form = new Inregistrare();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            using (OpenFileDialog dialog = new OpenFileDialog()) {
                dialog.Filter = "Images |*.png";
                dialog.InitialDirectory = Application.StartupPath + @"\Resurse\QRCode";

                if (dialog.ShowDialog() == DialogResult.OK) {
                    pictureBox1.Image = Image.FromFile(dialog.FileName);

                    MessagingToolkit.QRCode.Codec.QRCodeDecoder obj = new MessagingToolkit.QRCode.Codec.QRCodeDecoder();
                    string str = obj.decode(new MessagingToolkit.QRCode.Codec.Data.QRCodeBitmapImage(pictureBox1.Image as Bitmap));

                    var fields = str.Split('\n');
                    textBox1.Text = fields[1];
                    textBox2.Text = fields[2];
                }
            }
        }
    }
}
