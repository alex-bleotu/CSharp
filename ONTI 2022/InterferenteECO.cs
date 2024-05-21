using ONTI_2022.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ONTI_2022 {
    public partial class InterferenteECO : Form {
        int position = 0;
        bool selected = false;
        bool check = false;

        string[,] map = new string[10, 20];

        int robotX = -40, robotY = -40;
        int robotRotation;

        int[,] triangles = new int[400, 3];
        int trianglesLength;

        int drawCellX = -1, drawCellY = -1;

        int cellX;
        int cellY;

        bool start;

        public InterferenteECO(int id) {
            InitializeComponent(); 
            
            cellX = panel1.Width / 20;
            cellY = panel1.Height / 10;

            panel3.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resurse\\Background\\Back" + (id + 1) + ".jpg");

            pictureBox1.Invalidate();
            panel1.Invalidate();

            timer2.Start();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            check = !check;
            panel1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e) {
            using (OpenFileDialog dialog = new OpenFileDialog()) {
                dialog.InitialDirectory = Application.StartupPath + "\\Resurse";

                if (dialog.ShowDialog() == DialogResult.OK) {
                    var lines = System.IO.File.ReadAllLines(dialog.FileName);

                    for (int i = 0; i < 10; i++)
                        for (int j = 0; j < 20; j++)
                            map[i, j] = null;

                    trianglesLength = 0;

                    foreach (string line in lines) {
                        var parts = line.Split(' ');

                        if (parts[0] == "Robot") {
                            robotX = int.Parse(parts[1]) - 1; robotY = int.Parse(parts[2]) - 1;
                        }
                        else
                            map[Convert.ToInt32(parts[2]) - 1, Convert.ToInt32(parts[1]) - 1] = parts[0];
                    }

                    panel1.Invalidate();
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            Point[] points = { };

            if (position == 0)
                points = new Point[]{
                    new Point(0, 0),
                    new Point(pictureBox1.Width, 0),
                    new Point(0, pictureBox1.Height)
                }; 
            if (position == 1)
                points = new Point[]{
                    new Point(0, 0),
                    new Point(pictureBox1.Width, 0),
                    new Point(pictureBox1.Width, pictureBox1.Height)
                }; 
            if (position == 2)
                points = new Point[]{
                    new Point(pictureBox1.Width, 0),
                    new Point(pictureBox1.Width, pictureBox1.Height),
                    new Point(0, pictureBox1.Height)
                }; 
            if (position == 3)
                points = new Point[]{
                    new Point(0, 0),
                    new Point(pictureBox1.Width, pictureBox1.Height),
                    new Point(0, pictureBox1.Height)
                };

            e.Graphics.FillPolygon(Brushes.White, points);
        }

        private void button2_Click(object sender, EventArgs e) {
            position++;
            if (position == 4)
                position = 0;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            selected = !selected;
        }

        private Point[] GetPoints(int x, int y, int position) {
            Point[] points = new Point[] { };

            if (position == 0)
                points = new Point[]{
                    new Point(x, y),
                    new Point(x + cellX, y),
                    new Point(x, y + cellY)
                };
            if (position == 1)
                points = new Point[]{
                    new Point(x, y),
                    new Point(x + cellX, y),
                    new Point(x + cellX, y + cellY)
                };
            if (position == 2)
                points = new Point[]{
                    new Point(x + cellX, y),
                    new Point(x + cellX, y + cellY),
                    new Point(x, y + cellY)
                };
            if (position == 3)
                points = new Point[]{
                    new Point(x, y),
                    new Point(x + cellX, y + cellY),
                    new Point(x, y + cellY)
                };

            return points;
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            if (check) {
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 20; j++) {
                        Rectangle rect = new Rectangle(cellX * j, cellY * i, cellX, cellY);

                        e.Graphics.DrawRectangle(Pens.White, rect);
                    }
            }

            e.Graphics.DrawImage(new Bitmap(Resources.Robot, new Size(40,40)), new Point(cellX * robotX, cellY * robotY));

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 20; j++)
                    if (map[i, j] != null)
                        try {
                            e.Graphics.DrawImage(new Bitmap((Bitmap)Resources.ResourceManager.GetObject(map[i, j]), new Size(40, 40)), new Point(cellX * j, cellY * i + 10));
                        } catch { MessageBox.Show(map[i, j]); }

            for (int i = 0; i < trianglesLength; i++)
                e.Graphics.FillPolygon(Brushes.White, GetPoints(triangles[i, 0], triangles[i, 1], triangles[i, 2]));

            if (selected)
                e.Graphics.FillPolygon(Brushes.White, GetPoints(drawCellX * cellX, drawCellY * cellY, position));
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e) {
            if (selected) {
                triangles[trianglesLength, 0] = drawCellX * cellX;
                triangles[trianglesLength, 1] = drawCellY * cellY;
                triangles[trianglesLength, 2] = position;
                trianglesLength++;
                selected = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (robotRotation == 0)
                robotX++;
            else if (robotRotation == 1)
                robotY++;
            else if (robotRotation == 2)
                robotX--;
            else if (robotRotation == 3)
                robotY--;

            panel1.Invalidate();
        }

        private void timer2_Tick(object sender, EventArgs e) {
            if (selected)
                panel1.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e) {
            if (!start) {
                start = true;
                button4.Text = "Stop";

                timer1.Start();
            } else {
                start = false;
                button4.Text = "Start";

                timer1.Stop();

                using (SaveFileDialog save = new SaveFileDialog()) {
                    save.FileName = "image.png";
                    save.Title = "Save Text File";

                    if (save.ShowDialog() == DialogResult.OK) {
                        string filePath = save.FileName;

                        Bitmap bmp = new Bitmap(panel1.Width, panel1.Height);

                        panel1.DrawToBitmap(bmp, new Rectangle(0, 0, panel1.Width, panel1.Height));

                        bmp.Save(filePath);
                    }
                }
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {
            if (selected) {
                drawCellX = e.X / cellX;
                drawCellY = e.Y / cellY;
            }
        }
    }
}
