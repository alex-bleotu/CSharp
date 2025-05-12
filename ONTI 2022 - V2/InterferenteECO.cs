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

namespace ONTI_2022___V2 {
    public partial class InterferenteECO : Form {
        public class Item {
            public string name { get; set; }
            public int x { get; set; }
            public int y { get; set; }
            public int type { get; set; }
            public Image image { get; set; }
            public Item(string n, int x, int y, Image img) {
                name = n;
                this.x = x - 1;
                this.y = y - 1;
                if (name == "Robot")
                    type = 0;
                else if (name == "Plastic" || name == "Sticla" || name == "Hartie")
                    type = 1;
                else type = 2;
                image = img;
            }
            public Item(string n, int x, int y, int t, Image img) {
                name = n;
                this.x = x - 1;
                this.y = y - 1;
                type = t;
                image = img;
            }
        }

        List<Item> items;
        Image robot, plastic, glass, paper, fish1, fish2, fish3, fish4;
        Image purple, orange;

        Image start, first, second, third, last;

        int[,] map = new int[15, 25];
        List<Point> steps;
        int currentStep;
        readonly int[] dx = { -1, 0, 1, 0 };
        readonly int[] dy = { 0, 1, 0, -1 };

        int stopper,stopperX, stopperY;
        bool isStopperSelected;
        bool isRunning;
        string path;
        bool hasRestarted = true;
        bool isFinished;
        int ticks;

        const double ERROR = 0.001;
        double x, y;
        int direction;

        int plasticCounter, glassCounter, paperCounter;
        int garbageCount;

        Image GetStopper(int stopperIndex) {
            Bitmap b = new Bitmap(80, 80);

            using (Graphics g = Graphics.FromImage(b)) {
                Point[] points = new Point[3];

                if (stopperIndex == 0) {
                    points[0] = new Point(0, 0);
                    points[1] = new Point(80, 0);
                    points[2] = new Point(0, 80);
                }
                else if (stopperIndex == 1) {
                    points[0] = new Point(0, 0);
                    points[1] = new Point(80, 0);
                    points[2] = new Point(80, 80);
                }
                else if (stopperIndex == 2) {
                    points[0] = new Point(80, 0);
                    points[1] = new Point(0, 80);
                    points[2] = new Point(80, 80);
                }
                else {
                    points[0] = new Point(0, 0);
                    points[1] = new Point(0, 80);
                    points[2] = new Point(80, 80);
                }

                g.FillPolygon(Brushes.White, points);
            }

            return b;
        }

        private void button7_Click(object sender, EventArgs e) {
            Bitmap bitmap = new Bitmap(pictureBox1.BackgroundImage, 1201, 601);

            using (Graphics g = Graphics.FromImage(bitmap)) {
                List<Item> aux = new List<Item>();

                using (StreamReader reader = new StreamReader(path)) {
                    string line;

                    while ((line = reader.ReadLine()) != null) {
                        var fields = line.Split(' ');

                        Image img = robot;

                        if (fields[0] == "Robot")
                            img = robot;
                        else if (fields[0] == "Plastic") {
                            img = plastic; garbageCount++;
                        }
                        else if (fields[0] == "Sticla") {
                            img = glass; garbageCount++;
                        }
                        else if (fields[0] == "Hartie") {
                            img = paper; garbageCount++;
                        }
                        else if (fields[0] == "Meduza1")
                            img = fish1;
                        else if (fields[0] == "Meduza2")
                            img = fish2;
                        else if (fields[0] == "Meduza3")
                            img = fish3;
                        else if (fields[0] == "Meduza4")
                            img = fish4;

                        aux.Add(new Item(fields[0], Int32.Parse(fields[1]), Int32.Parse(fields[2]), img));
                    }
                }

                if (path.Contains("Harta1")) {
                    aux.Add(new Item("Stopper", 4, 3, 3, GetStopper(0)));
                } else if (path.Contains("Harta2")) {
                    aux.Add(new Item("Stopper", 8, 8, 3, GetStopper(2)));
                    aux.Add(new Item("Stopper", 8, 4, 3, GetStopper(0)));
                    aux.Add(new Item("Stopper", 9, 4, 3, GetStopper(1)));
                    aux.Add(new Item("Stopper", 9, 7, 3, GetStopper(3)));
                    aux.Add(new Item("Stopper", 16, 7, 3, GetStopper(2)));
                    aux.Add(new Item("Stopper", 16, 3, 3, GetStopper(1)));
                }
                else if (path.Contains("Harta3")) {
                    aux.Add(new Item("Stopper", 12, 3, 3, GetStopper(3)));
                    aux.Add(new Item("Stopper", 19, 3, 3, GetStopper(1)));
                    aux.Add(new Item("Stopper", 19, 7, 3, GetStopper(2)));
                    aux.Add(new Item("Stopper", 14, 7, 3, GetStopper(0)));
                    aux.Add(new Item("Stopper", 14, 10, 3, GetStopper(2)));
                    aux.Add(new Item("Stopper", 8, 10, 3, GetStopper(3)));
                }

                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 20; j++)
                        g.DrawRectangle(Pens.Yellow, j * 60, i * 60, 60, 60);

                foreach (var item in aux)
                    g.DrawImage(item.image, new Rectangle(item.x * 60, item.y * 60, 60, 60));
            }

            SaveFileDialog dialog = new SaveFileDialog();

            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.Filter = "Images |*.png";

            if (dialog.ShowDialog() == DialogResult.OK)
                bitmap.Save(dialog.FileName);
        }

        private void button6_Click(object sender, EventArgs e) {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.Filter = "Images |*.png";

            if (dialog.ShowDialog() == DialogResult.OK) {
                string path = dialog.FileName.Split('.')[0];

                if (start != null) {
                    Bitmap b = new Bitmap(pictureBox1.BackgroundImage, 1200, 600);
                    Graphics g = Graphics.FromImage(b);
                    g.DrawImage(start, new Rectangle(0, 0, 1200, 600));
                    b.Save(path + @"1.png");
                }
                if (first != null) {
                    Bitmap b = new Bitmap(pictureBox1.BackgroundImage, 1200, 600);
                    Graphics g = Graphics.FromImage(b);
                    g.DrawImage(first, new Rectangle(0, 0, 1200, 600));
                    b.Save(path + @"2.png");
                }
                if (second != null) {
                    Bitmap b = new Bitmap(pictureBox1.BackgroundImage, 1200, 600);
                    Graphics g = Graphics.FromImage(b);
                    g.DrawImage(second, new Rectangle(0, 0, 1200, 600));
                    b.Save(path + @"3.png");
                }
                if (third != null) {
                    Bitmap b = new Bitmap(pictureBox1.BackgroundImage, 1200, 600);
                    Graphics g = Graphics.FromImage(b);
                    g.DrawImage(third, new Rectangle(0, 0, 1200, 600));
                    b.Save(path + @"4.png");
                }
                if (last != null) {
                    Bitmap b = new Bitmap(pictureBox1.BackgroundImage, 1200, 600);
                    Graphics g = Graphics.FromImage(b);
                    g.DrawImage(last, new Rectangle(0, 0, 1200, 600));
                    b.Save(path + @"5.png");
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
            if (!isStopperSelected || isRunning || !hasRestarted) return;

            int x = e.X / 60;
            int y = e.Y / 60;

            if (x != stopperX || y != stopperY) {
                stopperX = x;
                stopperY = y;
                Draw();
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e) {
            if (!isStopperSelected || stopperX == -1 && stopperY == -1 || isRunning) return;

            foreach (var item in items)
                if (item.x == stopperX && item.y == stopperY)
                    return;

            isStopperSelected = false;

            items.Add(new Item("Stopper" + stopper, stopperX + 1, stopperY + 1, 3, pictureBox2.Image));

            Draw();
        }

        private void button5_Click(object sender, EventArgs e) {
            isRunning = false;
            hasRestarted = true;
            isFinished = false;
            timer1.Stop();

            List<Item> stopper = new List<Item>();
            for (int i = 0; i < items.Count; i++)
                if (items[i].type == 3)
                    stopper.Add(items[i]);

            LoadMap(path);
            foreach (var item in stopper)
                items.Add(item);

            button4.Text = "Start";
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = false;
            button6.Enabled = false;

            plasticCounter = 0;
            glassCounter = 0;
            paperCounter = 0;

            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;

            Draw();
        }

        private void button3_Click(object sender, EventArgs e) {
            items.Clear();

            Draw();

            button2.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            pictureBox2.Enabled = false;
            button4.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            ticks++;

            if (isRunning && !isFinished) {
                if (Math.Abs(x % 1.0) < ERROR && Math.Abs(y % 1.0) < ERROR && (direction == 0 && y == 0 || direction == 1 && x == 19 || direction == 2 && y == 9 || direction == 3 && x == 0)) {
                    isRunning = false;
                    button4.Text = "Start";
                    button2.Enabled = false;
                    button4.Enabled = false;

                    Draw();

                    timer1.Stop();

                    MessageBox.Show("Robotul a iesit din harta");

                    return;
                }

                if (direction == 0)
                    y -= 0.1;
                else if (direction == 1)
                    x += 0.1;
                else if (direction == 2)
                    y += 0.1;
                else if (direction == 3)
                    x -= 0.1;

                x = Math.Round(x, 1);
                y = Math.Round(y, 1);

                Draw();

                if (Math.Abs(x % 1.0) < ERROR && Math.Abs(y % 1.0) < ERROR) {
                    Check();

                    label2.Text = "Sticla: " + glassCounter;
                    label3.Text = "Hartie: " + paperCounter;
                    label4.Text = "Plastic: " + plasticCounter;

                    items.Add(new Item("Tile", (int)x + 1, (int)y + 1, 4, purple));
                }
            }

            if (isFinished) {
                if (currentStep == steps.Count) {
                    timer1.Stop();

                    last = pictureBox1.Image;
                    button5.Enabled = true;
                    return;
                }

                x = steps[currentStep].X - 1;
                y = steps[currentStep].Y - 1;
                currentStep++;

                Draw();

                items.Add(new Item("Tile", (int)x + 1, (int)y + 1, 4, purple));
            }

            if (garbageCount == 0 && !isFinished) {
                button4.Text = "Start";
                button2.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = true;

                isFinished = true;

                constructMap();

                timer1.Stop();
                timer1.Interval = 100;
                MessageBox.Show("Robotul a strans toate deseurile");
                timer1.Start();
            }

            if (ticks % 50 == 0) {
                if (first == null)
                    first = pictureBox1.Image;
                else if (second == null)
                    second = pictureBox1.Image;
                else if (third == null)
                    third = pictureBox1.Image;
            }
        }

        void lee(int startX, int startY) {
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(new Point(startX, startY));

            map[startX, startY] = 1;

            while (queue.Count != 0) {
                Point aux = queue.Dequeue();

                for (int k = 0; k < 4; k++) {
                    Point aux2 = new Point(aux.X + dx[k], aux.Y + dy[k]);
                    if (map[aux2.X, aux2.Y] == 0) {
                        map[aux2.X, aux2.Y] = map[aux.X, aux.Y] + 1;
                        queue.Enqueue(aux2);
                    }
                }
            }
        }

        void constructPath(int x, int y) {
            if (map[x, y] == 1) {
                steps.Add(new Point(y, x));
                return;
            }

            for (int k = 0; k < 4; k++) {
                Point p = new Point(x + dx[k], y + dy[k]);
                if (p.X >= 1 && p.X <= 10 && p.Y >= 1 && p.Y <= 20 && map[p.X, p.Y] == map[x, y] - 1) {
                    constructPath(p.X, p.Y);
                    steps.Add(new Point(y, x));
                    return;
                }
            }
        }

        void constructMap() {
            steps = new List<Point>();
            currentStep = 0;

            for (int i = 1; i <= 10; i++)
                for (int j = 1; j <= 20; j++)
                    map[i, j] = 0;
            for (int j = 0; j <= 21; j++)
                map[0, j] = map[11, j] = -1;
            for (int i = 0; i <= 11; i++)
                map[i, 0] = map[i, 21] = -1;

            foreach (var item in items)
                if (item.type == 2 || item.type == 3)
                    map[item.y + 1, item.x + 1] = -1;

            lee((int)y + 1, (int)x + 1);

            foreach (var item in items)
                if (item.type == 0)
                    constructPath(item.y + 1, item.x + 1);
        }

        void Check() {
            foreach (var item in items)
                if (item.type == 3 && item.x == (int)x && item.y == (int)y) {
                    if (item.name == "Stopper0") {
                        if (direction == 2)
                            direction = 0;
                        else if (direction == 1)
                            direction = 3;
                        else if (direction == 0)
                            direction = 1;
                        else if (direction == 3)
                            direction = 2;
                    } else if (item.name == "Stopper1") {
                        if (direction == 2)
                            direction = 0;
                        else if (direction == 1)
                            direction = 2;
                        else if (direction == 0)
                            direction = 3;
                        else if (direction == 3)
                            direction = 1;
                    } else if (item.name == "Stopper2") {
                        if (direction == 2)
                            direction = 3;
                        else if (direction == 1)
                            direction = 0;
                        else if (direction == 0)
                            direction = 2;
                        else if (direction == 3)
                            direction = 1;
                    } else if (item.name == "Stopper3") {
                        if (direction == 2)
                            direction = 1;
                        else if (direction == 1)
                            direction = 3;
                        else if (direction == 0)
                            direction = 2;
                        else if (direction == 3)
                            direction = 0;
                    }

                    x = (int)x;
                    y = (int)y;

                    return;
                }
        }

        private void button4_Click(object sender, EventArgs e) {
            if (!isRunning) {
                if (!hasRestarted) {
                    isRunning = true;

                    button4.Text = "Stop";
                    button2.Enabled = false;

                    timer1.Start();

                    return;
                }

                Dialog dialog = new Dialog();

                if (dialog.ShowDialog() == DialogResult.OK) {
                    hasRestarted = false;
                    isRunning = true;
                    isFinished = false;

                    direction = dialog.direction;

                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Text = "Stop";
                    button5.Enabled = true;

                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;

                    foreach (var item in items)
                        if (item.type == 0) {
                            x = item.x;
                            y = item.y;
                            break;
                        }

                    items.Add(new Item("Tile", (int)x + 1, (int)y + 1, 4, orange));

                    start = pictureBox1.Image;
                    ticks = 0;

                    timer1.Interval = 20;
                    timer1.Start();
                }
                else MessageBox.Show("Alege o directie pentru a incepe");
            } else {
                isRunning = false;
                button4.Text = "Start";

                Draw();

                timer1.Stop();
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            stopper++;
            if (stopper == 4)
                stopper = 0;

            DrawStopper();
        }

        private void pictureBox2_Click(object sender, EventArgs e) {
            if (isRunning || !hasRestarted) return;

            isStopperSelected = !isStopperSelected;

            stopperX = -1;
            stopperY = -1;

            Draw();
        }

        public InterferenteECO(Image img, string name) {
            InitializeComponent();

            pictureBox1.BackgroundImage = img;
            this.Text = "Interferente ECO - " + name;

            items = new List<Item>();
            robot = Image.FromFile(Application.StartupPath + @"\Resurse\Robot\Robot.png");
            plastic = Image.FromFile(Application.StartupPath + @"\Resurse\MaterialeReciclabile\Plastic.png");
            glass = Image.FromFile(Application.StartupPath + @"\Resurse\MaterialeReciclabile\Sticla.png");
            paper = Image.FromFile(Application.StartupPath + @"\Resurse\MaterialeReciclabile\Hartie.png");
            fish1 = Image.FromFile(Application.StartupPath + @"\Resurse\Meduze\Meduza1.png");
            fish2 = Image.FromFile(Application.StartupPath + @"\Resurse\Meduze\Meduza2.png");
            fish3 = Image.FromFile(Application.StartupPath + @"\Resurse\Meduze\Meduza3.png");
            fish4 = Image.FromFile(Application.StartupPath + @"\Resurse\Meduze\Meduza4.png");

            purple = new Bitmap(60, 60);
            using (Graphics g = Graphics.FromImage(purple))
                g.FillRectangle(Brushes.Purple, new Rectangle(0, 0, 60, 60));
            orange = new Bitmap(60, 60);
            using (Graphics g = Graphics.FromImage(orange))
                g.FillRectangle(Brushes.Orange, new Rectangle(0, 0, 60, 60));

            stopper = 0;

            Draw();
            DrawStopper();
        }

        private void InterferenteECO_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        void DrawStopper() {
            pictureBox2.Image = GetStopper(stopper);
        }

        void Draw() {
            Bitmap b = new Bitmap(1200, 600);

            bool touched = false;

            using (Graphics g = Graphics.FromImage(b)) {
                if (checkBox1.Checked)
                    for (int i = 0; i < 10; i++)
                        for (int j = 0; j < 20; j++)
                            g.DrawRectangle(Pens.Yellow, j * 60, i * 60, 60, 60);

                bool canDraw = true;

                foreach (var item in items)
                    if (item.type == 4)
                        g.DrawImage(item.image, new Rectangle(item.x * 60, item.y * 60, 60, 60));

                List<Item> toRemove = new List<Item>();

                foreach (var item in items) {
                    if (item.type == 4 || item.type == 0) continue;

                    if (Math.Abs(x % 1.0) < ERROR && Math.Abs(y % 1.0) < ERROR) {
                        if (item.type == 1 && item.x == (int)x && item.y == (int)y && (isRunning || !hasRestarted)) {
                            toRemove.Add(item);
                            garbageCount--;

                            if (item.name == "Plastic")
                                plasticCounter++;
                            else if (item.name == "Sticla")
                                glassCounter++;
                            else if (item.name == "Hartie")
                                paperCounter++;

                            continue;
                        }
                        else if (item.type == 2 && item.x == (int)x && item.y == (int)y && (isRunning || !hasRestarted)) touched = true;
                    }

                    g.DrawImage(item.image, new Rectangle(item.x * 60, item.y * 60, 60, 60));

                    if (item.x == stopperX && item.y == stopperY)
                        canDraw = false;
                }

                foreach (var item in items)
                    if (item.type == 0) {
                        if ((isRunning || !hasRestarted) && item.type == 0)
                            g.DrawImage(item.image, new Rectangle((int)(x * 60), (int)(y * 60), 60, 60));
                        else g.DrawImage(item.image, new Rectangle(item.x * 60, item.y * 60, 60, 60));

                        if (item.x == stopperX && item.y == stopperY)
                            canDraw = false;

                        break;
                    }

                if (isStopperSelected && stopperX != -1 && stopperY != -1 && canDraw)
                    g.DrawImage(pictureBox2.Image, new Rectangle(stopperX * 60, stopperY * 60, 60, 60));

                foreach (var item in toRemove)
                    items.Remove(item);
            }

            pictureBox1.Image = b;

            if (touched) {
                isRunning = false;
                button4.Text = "Start";
                button2.Enabled = false;
                button4.Enabled = false;

                timer1.Stop();

                MessageBox.Show("Robotul a atins o meduza");
            }
        }

        void LoadMap(string path) {
            items.Clear();
            garbageCount = 0;

            using (StreamReader reader = new StreamReader(path)) {
                string line;

                while ((line = reader.ReadLine()) != null) {
                    var fields = line.Split(' '); 
                    
                    Image img = robot;

                    if (fields[0] == "Robot")
                        img = robot;
                    else if (fields[0] == "Plastic") {
                        img = plastic; garbageCount++;
                    }
                    else if (fields[0] == "Sticla") {
                        img = glass; garbageCount++;
                    }
                    else if (fields[0] == "Hartie") {
                        img = paper; garbageCount++;
                    }
                    else if (fields[0] == "Meduza1")
                        img = fish1;
                    else if (fields[0] == "Meduza2")
                        img = fish2;
                    else if (fields[0] == "Meduza3")
                        img = fish3;
                    else if (fields[0] == "Meduza4")
                        img = fish4;

                    items.Add(new Item(fields[0], Int32.Parse(fields[1]), Int32.Parse(fields[2]), img));
                }
            }

            Draw();
        }

        private void button1_Click(object sender, EventArgs e) {
            using (OpenFileDialog dialog = new OpenFileDialog()) {
                dialog.Filter = "Harta |Harta1.txt; Harta2.txt; Harta3.txt";
                dialog.InitialDirectory = Application.StartupPath + @"\Resurse\";

                if (dialog.ShowDialog() == DialogResult.OK) {
                    LoadMap(dialog.FileName);
                    path = dialog.FileName;
                }
            }

            button2.Enabled = true;
            button4.Enabled = true;
            button7.Enabled = true;
            pictureBox2.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            Draw();
        }
    }
}
