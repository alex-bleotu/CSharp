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
    public partial class JocMemorie : Form {
        Database db = new Database();

        bool isRunning = false;
        int time;
        int level;
        int cnt;

        int score = 0;

        string email, name;

        public class Cell {
            public Image img { get; set; }
            public string name { get; set; }
            public int bottomId { get; set; }
            public int timeX { get; set; }
            public bool clickedX { get; set; }
            public int timeY { get; set; }
            public bool clickedY { get; set; }
            public bool done { get; set; }

            public Cell(Image i, string n, int b) {
                name = n;
                img = i;
                timeX = 0;
                clickedX = false;
                timeY = 0;
                clickedY = false;
                done = false;
                bottomId = b;
            }

            public Cell(Image i, string n) {
                name = n;
                img = i;
                timeX = 0;
                clickedX = false;
                timeY = 0;
                clickedY = false;
                done = false;
                bottomId = 0;
            }
        }

        List<Cell> images;
        List<Cell> cells;

        public JocMemorie(string email, string name) {
            InitializeComponent();

            this.email = email;
            this.name = name;

            images = new List<Cell>();
            cells = new List<Cell>();

            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\avion.png"), "avion"));
            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\bloc.png"), "bloc"));
            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\caine.jpg"), "caine"));
            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\caprioara.jpg"), "caprioara"));
            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\iepure.png"), "iepure"));
            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\leu.jpg"), "leu"));
            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\lup.jpg"), "lup"));
            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\masina.png"), "masina"));
            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\minge.jpg"), "minge"));
            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\patine.jpg"), "patine"));
            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\pisica.jpg"), "pisica"));
            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\taur.jpg"), "taur"));
            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\urs.png"), "urs"));
            images.Add(new Cell(Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\vulpe.png"), "vulpe"));

            level = 3;
            cnt = count(level);

            label2.Text = "Runda: " + (level - 2);
        }

        int count(int x) {
            if (x <= 2)
                return 1;
            return count(x - 1) + count(x - 2);
        }

        void MakeBoard() {
            cells.Clear();

            Random r = new Random();

            while (cells.Count != cnt) {
                int val = r.Next(14);
                bool check = true;

                foreach (var cell in cells)
                    if (cell.name == images[val].name) {
                        check = false;
                        break;
                    }

                if (check) {
                    int val2 = r.Next(cnt);
                    bool check2 = true;

                    foreach (var cell in cells)
                        if (cell.bottomId == val2) {
                            check2 = false;
                            break;
                        }

                    if (check2)
                        cells.Add(new Cell(images[val].img, images[val].name, val2));
                }
            }

            DrawBoard();
        }

        void DrawBoard() {
            Bitmap bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);

            using (Graphics g = Graphics.FromImage(bitmap)) {
                for (int i = 0; i < cnt; i++) {
                    if (cells[i].done || cells[i].clickedX)
                        g.DrawImage(cells[i].img, new Rectangle(i * 100 + 10, 10, 90, 90));
                    else g.FillRectangle(Brushes.Yellow, i * 100 + 10, 10, 90, 90);

                    g.FillRectangle(Brushes.Yellow, cells[i].bottomId * 100 + 10, 110, 90, 90);
                    if (cells[i].done)
                        g.DrawString((i + 1) + " - " + cells[i].name, label1.Font, Brushes.Black, cells[i].bottomId * 100 + 10, 145);
                    else if (cells[i].clickedY)
                        g.DrawString(cells[i].name, label1.Font, Brushes.Black, cells[i].bottomId * 100 + 10, 145);
                }
            }

            pictureBox1.Image = bitmap;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            label1.Text = "Timp ramas: " + (time / 10);

            bool all = true;

            for (int i = 0; i < cnt; i++) {
                if (cells[i].done) continue;

                if (cells[i].clickedX && cells[i].clickedY) {
                    cells[i].done = true;
                    score++;
                    continue;
                }

                all = false;

                if (cells[i].clickedX == true)
                    cells[i].timeX -= 1;
                if (cells[i].timeX == 0)
                    cells[i].clickedX = false;

                if (cells[i].clickedY == true)
                    cells[i].timeY -= 1;
                if (cells[i].timeY == 0)
                    cells[i].clickedY = false;
            }

            DrawBoard();

            if (all) {
                this.Hide();
                Artificii form = new Artificii(this);
                form.Show();

                timer1.Stop();
                label1.Visible = false;
                button1.Enabled = true;
                isRunning = false;
                level++;
                pictureBox1.Image = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);

                if (level - 2 == 5) {
                    db.AddScore(email, score, 0);
                    this.Hide();
                    AlegeJoc form2 = new AlegeJoc(email, name);
                    form2.Show();
                }
                else {
                    cnt = count(level);
                    label2.Text = "Runda: " + (level - 2);
                }
            }

            if (time == 0) {
                timer1.Stop();
                label1.Visible = false;
                button1.Enabled = true;
                isRunning = false;
                MessageBox.Show("Timpul a expirat");

                db.AddScore(email, 0, 0);

                this.Hide(); 
                AlegeJoc form = new AlegeJoc(email, name);
                form.Show();
            }

            time--;
        }

        private void button1_Click(object sender, EventArgs e) {
            isRunning = true;
            time = 1000;
            label1.Text = "Timp ramas: " + time;
            label1.Visible = true;
            timer1.Start();
            button1.Enabled = false;
            MakeBoard();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e) {
            if (!isRunning) return;

            int x, y;

            if (e.X / 10 % 10 == 0 || e.Y / 10 % 10 == 0)
                return;
            else {
                x = e.X / 100;
                y = e.Y / 100;

                if (x < cnt) {
                    if (y == 0) {
                        cells[x].clickedX = true;
                        cells[x].timeX = 10;
                    }
                    else {
                        for (int i = 0; i < cells.Count; i++)
                            if (cells[i].bottomId == x) {
                                cells[i].clickedY = true;
                                cells[i].timeY = 10;
                            }
                    }
                }
            }
        }

        private void JocMemorie_FormClosed(object sender, FormClosedEventArgs e) {
            this.Hide();
            AlegeJoc form = new AlegeJoc(email, name);
            form.Show();
        }
    }
}
