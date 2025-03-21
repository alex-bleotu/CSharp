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
    public partial class Calendar : Form {
        Database db = new Database();
        User user;

        private DateTime date = new DateTime(2024, 05, 14);
        private Panel[,] days = new Panel[6, 7];

        private List<Record> records = new List<Record>();
        private List<string> tips = new List<string>();

        public Calendar(User user) {
            InitializeComponent();

            this.user = user;

            records = db.GetUserRecords(user);
            ReadTips();
            int day = GetLastDaySinceFullMoon(date);
            label9.Text = "Evolutia in zile: " + day + " zile";
            DrawMoon(day);

            this.Text = "Cosmos- Calendar, " + user.email;

            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++) {
                    Panel panel = new Panel();
                    panel.Size = new Size(75, 75);
                    panel.Visible = false;
                    panel.BorderStyle = BorderStyle.FixedSingle;
                    panel.Location = new Point(j * 80 + 5, i * 80 + 5);
                    panel.Click += dateClick;

                    panel1.Controls.Add(panel);
                    days[i, j] = panel;
                }

            ShowPanels();
        }

        private string MonthToString() {
            if (date.Month == 1)
                return "ianuarie";
            else if (date.Month == 2)
                return "februarie";
            else if (date.Month == 3)
                return "martie";
            else if (date.Month == 4)
                return "aprilie";
            else if (date.Month == 5)
                return "mai";
            else if (date.Month == 6)
                return "iunie";
            else if (date.Month == 7)
                return "iulie";
            else if (date.Month == 8)
                return "august";
            else if (date.Month == 9)
                return "septembrie";
            else if (date.Month == 10)
                return "octombrie";
            else if (date.Month == 11)
                return "noiembrie";
            else if (date.Month == 12)
                return "decembrie";
            return "";
        }

        private Record GetRecordByDay(int day) {
            DateTime aux = new DateTime(date.Year, date.Month, day);

            foreach (Record record in records) {
                if (aux.Date == record.date.Date)
                    return record;
            }
            return null;
        }

        private void ShowPanels() {
            label1.Text = MonthToString() + " " + date.Year;

            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++) {
                    days[i, j].Visible = false;
                    days[i, j].Controls.Clear();
                    days[i, j].Tag = "";
                }

            DateTime first = new DateTime(date.Year, date.Month, 1);
            DateTime last = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            int startDay = ((int)first.DayOfWeek + 6) % 7, counter = 0;
            for (int i = 0; i < 6; i++) {
                for (int j = startDay; j < 7 && counter < last.Day; j++) {
                    days[i, j].Visible = true;
                    counter++;

                    Label label = new Label();
                    label.Text = counter.ToString();
                    label.Location = new Point(25, 10);
                    label.Font = new Font(label.Font.FontFamily, 12, FontStyle.Bold);

                    days[i, j].Tag = counter.ToString();
                    days[i, j].Controls.Add(label);

                    Record record = GetRecordByDay(counter);
                    if (record != null) {
                        PictureBox moon = new PictureBox();
                        moon.Location = new Point(0, 40);
                        moon.Size = new Size(30, 30);
                        moon.Image = Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiLuna\\" + record.moon + ".png");
                        moon.SizeMode = PictureBoxSizeMode.StretchImage;
                        days[i, j].Controls.Add(moon);

                        PictureBox zodiac = new PictureBox();
                        zodiac.Location = new Point(40, 40);
                        zodiac.Size = new Size(30, 30);
                        zodiac.Tag = record.zodiac;
                        zodiac.Image = Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiZodii\\Z_" + record.zodiac + ".png");
                        zodiac.SizeMode = PictureBoxSizeMode.StretchImage;
                        zodiac.Click += ZodiacClick;

                        ToolTip toolTip = new ToolTip();
                        toolTip.SetToolTip(zodiac, tips[record.zodiac - 1]);

                        days[i, j].Controls.Add(zodiac);
                    }
                }
                startDay = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            DialogResult result = MessageBox.Show("Inchidere", "Doriti să părăsiți aplicația?", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                Application.Exit();
            else if (result == DialogResult.No) {
                this.Hide();
                Auth form = new Auth();
                form.Show();
            }
        }

        private void Calendar_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e) {
            date = date.AddMonths(-1);
            ShowPanels();
        }

        private void button3_Click(object sender, EventArgs e) {
            date = date.AddMonths(1);
            ShowPanels();
        }

        private void dateClick(object sender, EventArgs e) {
            Panel panel = (Panel)sender;

            if (panel.Tag != "") {
                int day = Int32.Parse(panel.Tag.ToString());
                DateTime currentDate = new DateTime(date.Year, date.Month, day);

                int currentDay = GetLastDaySinceFullMoon(currentDate);
                label9.Text = "Evolutia in zile: " + currentDay + " zile";
                DrawMoon(currentDay);
            }
        }

        private void DrawMoon(int day) {
            Bitmap bitmap = new Bitmap(400, 400);

            using (Graphics g = Graphics.FromImage(bitmap)) {
                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiLuna\\BackLuna.jpeg"),
                    new Rectangle(0, 0, 400, 400));

                Rectangle moonRect = new Rectangle(100, 100, 200, 200);
                g.DrawImage(Image.FromFile(Application.StartupPath + "\\Resurse\\ImaginiLuna\\Luna.png"), moonRect);

                double phase = Math.Abs((day - 15.0) / 14.0);

                if (phase > 0.01) {
                    int shadowWidth = (int)(phase * 200);

                    Color transparentBlack = Color.FromArgb((int)(255 * 0.15), 0, 0, 0);
                    using (Brush brush = new SolidBrush(transparentBlack)) {
                        if (day > 15) {
                            Rectangle shadowRect = new Rectangle(100, 100, shadowWidth, 200);
                            g.FillEllipse(brush, shadowRect);
                        }
                        else {
                            Rectangle shadowRect = new Rectangle(300 - shadowWidth, 100, shadowWidth, 200);
                            g.FillEllipse(brush, shadowRect);
                        }
                    }
                }
            }

            pictureBox1.Image = bitmap;
        }

        private int GetLastDaySinceFullMoon(DateTime date) {
            int year = date.Year, month = date.Month, day = date.Day;

            if (month < 3) {
                month += 12;
                year--;
            }

            int a = year / 100;
            int b = a / 4;
            int c = 2 - a + b;
            int e = (int)(365.25 * (year + 4716));
            int f = (int)(30.6001 * (month + 1));

            double julianDate = c + day + e + f - 1524;

            int daysSinceLast = (int)(julianDate - 2451549.5);
            double fullMoons = daysSinceLast / 29.5;

            double fractionalPart = fullMoons - Math.Floor(fullMoons);

            return (int)(fractionalPart * 29.5);
        }

        private void ReadTips() {
            using (StreamReader reader = new StreamReader(Application.StartupPath + "\\Resurse\\Zodiac.txt")) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    var fields = line.Split(';');
                    tips.Add(fields[0]);
                }
            }
        }

        private void ZodiacClick(object sender, EventArgs e) {
            this.Hide();
            Space form = new Space(Int32.Parse(((PictureBox)sender).Tag.ToString()), user);
            form.Show();
        }
    }
}
