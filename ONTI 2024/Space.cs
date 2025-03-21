using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2024 {
    public partial class Space : Form {
        User user;
        int currentId;

        List<Tuple<string, string>> data = new List<Tuple<string, string>>();

        private int[,] coords = {
            { 100, 350 },
            { 175, 250 },
            { 275, 200 },
            { 400, 150 },
            { 525, 200 },
            { 625, 250 },
            { 700, 350 },
            { 625, 450 },
            { 525, 500 },
            { 400, 550 },
            { 275, 500 },
            { 175, 450 },
        };

        public Space(int id, User user) {
            InitializeComponent();

            ReadData();

            currentId = id;
            this.user = user;

            UpdateZodiac();

            DrawMap();
        }

        private void DrawMap() {
            Bitmap bitmap = new Bitmap(800, 600);

            using (Graphics g = Graphics.FromImage(bitmap)) {
                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiConstelatii\\earth.png"), new Rectangle(350, 250, 100, 100));


                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiConstelatii\\gemeni.png"),    new Rectangle(50, 250, 100, 100));

                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiConstelatii\\berbec.png"),    new Rectangle(125, 150, 100, 100));
                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiConstelatii\\taur.png"),      new Rectangle(225, 100, 100, 100));
                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiConstelatii\\rac.png"),       new Rectangle(350, 50, 100, 100));
                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiConstelatii\\capricorn.png"), new Rectangle(475, 100, 100, 100));
                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiConstelatii\\pesti.png"),     new Rectangle(575, 150, 100, 100));

                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiConstelatii\\balanta.png"),   new Rectangle(650, 250, 100, 100));

                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiConstelatii\\leu.png"),       new Rectangle(575, 350, 100, 100));
                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiConstelatii\\fecioara.png"),  new Rectangle(475, 400, 100, 100));
                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiConstelatii\\sagatator.png"), new Rectangle(350, 450, 100, 100));
                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiConstelatii\\scorpion.png"),  new Rectangle(225, 400, 100, 100));
                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiConstelatii\\varsator.png"),  new Rectangle(125, 350, 100, 100));

                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiZodii\\Z_" + currentId + ".png"), 
                    new Rectangle(coords[currentId - 1, 0] - 12, coords[currentId - 1, 1] - 12, 25, 25));
            }

            pictureBox1.Image = bitmap;
        }

        private void Space_FormClosed(object sender, FormClosedEventArgs e) {
            this.Hide();
            Calendar form = new Calendar(user);
            form.Show();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e) {
            Bitmap bitmap = new Bitmap(250, 200);

            int height = 200 / trackBar1.Value;
            int width = 250 / trackBar1.Value;

            Rectangle crop = new Rectangle(e.X - width / 2, e.Y - height / 2, width, height);

            using (Graphics g = Graphics.FromImage(bitmap)) {
                g.DrawImage(pictureBox1.Image, new Rectangle(0, 0, 250, 200), crop, GraphicsUnit.Pixel);
            }

            pictureBox2.Image = bitmap;
        }

        private void ReadData() {
            using (StreamReader reader = new StreamReader(Application.StartupPath + "\\Resurse\\Zodiac.txt")) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    var fields = line.Split(';');
                    data.Add(new Tuple<string, string>(fields[0], fields[3]));
                }
            }
        }

        private void UpdateZodiac() {
            label1.Text = data[currentId - 1].Item1 + "\n" + data[currentId - 1].Item2;
            DrawMap();
        }

        private void button2_Click(object sender, EventArgs e) {
            currentId++;
            if (currentId > 12)
                currentId = 1;
            UpdateZodiac();
        }

        private void button1_Click(object sender, EventArgs e) {
            currentId--;
            if (currentId < 1)
                currentId = 12;
            UpdateZodiac();
        }
    }
}
