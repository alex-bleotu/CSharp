using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2023___V2 {
    public partial class JocLitere : Form {
        Database db = new Database();

        string email, name;
        int time;
        int x, y;
        int score;

        Image ball;
        List<Letter> letters;
        int lettersX;

        bool canMove = true;
        string text;

        public class Letter {
            public char letter { get; set; }
            public bool visible { get; set; }
            public int index { get; set; }
            public Letter(char l, int id) {
                visible = true;
                letter = l;
                index = id;
            }
        }

        public class Cell {
            public Image img { get; set; }
            public string name { get; set; }

            public Cell(Image i, string n) {
                name = n;
                img = i;
            }
        }

        List<Cell> images;
        List<Cell> cells;

        private void JocLitere_FormClosed(object sender, FormClosedEventArgs e) {
            this.Hide();
            AlegeJoc form = new AlegeJoc(email, name);
            form.Show();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            time--;

            label1.Text = "Timp ramas: " + (time / 20);

            Draw();

            if (time == 0) {
                timer1.Stop();
                MessageBox.Show("Timpul a expirat");

                db.AddScore(email, 0, 0);

                this.Hide();
                AlegeJoc form = new AlegeJoc(email, name);
                form.Show();
            }

            if (!canMove) {
                y -= 25;

                if (y == 0) {
                    for (int i = 0; i < letters.Count; i++) {
                        if (lettersX + i * 50 == x && letters[i].visible) {
                            letters[i].visible = false;

                            text += letters[i].letter;
                            label2.Text = text;

                            canMove = true;
                            y = 350;

                            score += 10;

                            for (int j = 0; j < cells.Count; j++)
                                if (text == cells[j].name) {
                                    cells.RemoveAt(j);
                                    text = "";
                                    label2.Text = text;

                                    if (j == 0)
                                        pictureBox2.Image = null;
                                    else if (j == 1)
                                        pictureBox3.Image = null;

                                    if (cells.Count == 0) {
                                        timer1.Stop();
                                        MessageBox.Show("Ai castigat!");

                                        db.AddScore(email, score, 1);

                                        this.Hide();
                                        AlegeJoc form = new AlegeJoc(email, name);
                                        form.Show();
                                    }

                                    return;
                                }

                            return;
                        }
                    }
                }

                if (y == -50) {
                    canMove = true;
                    y = 350;
                }
            }
        }

        private void JocLitere_KeyDown(object sender, KeyEventArgs e) {
            if (!canMove) return;

            if (e.KeyCode == Keys.Up) {
                canMove = false;
            }

            if (e.KeyCode == Keys.Left)
                x -= 50;
            else if (e.KeyCode == Keys.Right)
                x += 50;
        }

        public JocLitere(string email, string name) {
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

            ball = Image.FromFile(Application.StartupPath + @"\Resurse\ball.png");

            x = 350;
            y = 350;

            score = 0;

            Random r = new Random();

            while (cells.Count < 2) {
                int val = r.Next(14);
                bool check = true;

                foreach (var cell in cells)
                    if (cell.name == images[val].name) {
                        check = false;
                        break;
                    }

                if (check)
                    cells.Add(new Cell(images[val].img, images[val].name));
            }

            pictureBox2.Image = cells[0].img;
            pictureBox3.Image = cells[1].img;

            string str = cells[0].name + cells[1].name;

            letters = new List<Letter>();

            while (letters.Count < str.Length) {
                int val = r.Next(str.Length);

                bool check = true;
                foreach (var l in letters)
                    if (l.index == val) {
                        check = false;
                        break;
                    }

                if (check) {
                    letters.Add(new Letter(str[val], val));
                }
            }

            lettersX = (800 - str.Length * 50) / 2;
            if (lettersX % 10 == 5)
                lettersX -= 25;

            text = "";
            label2.Text = text;

            time = 2000;
            timer1.Start();
        }

        void Draw() {
            Bitmap b = new Bitmap(800, 400);

            using (Graphics g = Graphics.FromImage(b)) {
                g.DrawImage(ball, new Rectangle(x, y, 50, 50));

                int i = 0;
                foreach (var l in letters) {
                    if (l.visible)
                        g.DrawString(l.letter.ToString(), new Font("Arial", 20), Brushes.Red, new Point(lettersX + i * 50 + 15, 0));
                    i++;
                }
            }

            pictureBox1.Image = b;
        }
    }
}
