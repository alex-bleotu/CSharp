using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2022___V2 {
    public partial class Vizualizare : Form {
        Database database = new Database();
        int id = -1;
        int show = 0;

        public Vizualizare(int id) {
            InitializeComponent();

            label1.Text = "Utilizator: " + database.GetUsername(id);

            List<string> names = database.GetMapNames();
            comboBox1.Items.AddRange(names.ToArray());
            comboBox2.SelectedIndex = 0;

            dateTimePicker1.Value = new DateTime(2022, 05, 14);
        }

        private void Vizualizare_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            id = database.GetMapId(comboBox1?.SelectedItem.ToString());
                
            SetPoints(id);
        }

        private void SetPoints(int id) {
            string path = database.GetImage(comboBox1?.SelectedItem.ToString());
            List<Measurement> list = database.GetMeasurements(id);

            if (path == "-1")
                path = "default_harta.png";

            Bitmap bitmap = new Bitmap(Image.FromFile(Application.StartupPath + @"\Resources\Harti\" + path));

            using (Graphics g = Graphics.FromImage(bitmap)) {
                foreach (var val in list) {
                    if (val.date.Date == dateTimePicker1.Value.Date) {
                        if (val.value < 20 && (show == 0 || show == 1)) {
                            g.DrawEllipse(new Pen(Color.Green, 1), new Rectangle(val.posX, val.posY, 20, 20));
                            g.DrawString(val.value.ToString(), new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Green), new PointF(val.posX, val.posY));
                        } else if (val.value >= 20 && val.value <= 40 && (show == 0 || show == 2)) {
                            g.DrawEllipse(new Pen(Color.Orange, 1), new Rectangle(val.posX, val.posY, 20, 20));
                            g.DrawString(val.value.ToString(), new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Orange), new PointF(val.posX, val.posY));
                        } else if (val.value > 40 && (show == 0 || show == 3)) {
                            g.DrawEllipse(new Pen(Color.Red, 1), new Rectangle(val.posX, val.posY, 20, 20));
                            g.DrawString(val.value.ToString(), new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Red), new PointF(val.posX, val.posY));
                        }
                    }
                }
            }

            pictureBox1.Image = bitmap;
            pictureBox2.Image = bitmap;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) {
            if (id != -1) SetPoints(id);
        }

        private void button1_Click(object sender, EventArgs e) {
            comboBox2.SelectedIndex = 0;
            show = 0; 
            if (id != -1) SetPoints(id);
        }

        private void button2_Click(object sender, EventArgs e) {
            show = comboBox2.SelectedIndex;

            if (id != -1)
                SetPoints(id);
        }

        private double DistanceToPoint(int x, int y, int targetX, int targetY) {
            return Math.Sqrt((targetX - x) * (targetX - x) + (targetY - y) * (targetY - y));
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e) {
            if (id == -1) return;

            List<Measurement> list = database.GetMeasurements(id);

            bool action = true;
            foreach (var val in list) {
                if (val.date.Date == dateTimePicker1.Value.Date && DistanceToPoint(e.X, e.Y, val.posX + 10, val.posY + 10) < 10)
                {
                    action = false;
                    break;
                }
            }

            if (action) {
                AdaugaMasurare form = new AdaugaMasurare(id, e.X - 10, e.Y - 10, dateTimePicker1.Value);
                form.Show();
                form.FormClosed += (s, args) => SetPoints(id);
            }
        }

        public Tuple<Measurement, Measurement> FindBiggest(int x, int y) {
            Measurement max1 = null;
            Measurement max2 = null;

            List<Measurement> list = database.GetMeasurements(id);

            foreach (var val in list) {
                if (val.date.Date == dateTimePicker1.Value.Date) {
                    if (max1 == null || val.value > max1.value) {
                        max2 = max1;
                        max1 = val;
                    }
                    else if (max2 == null || val.value > max2.value) {
                        max2 = val;
                    }
                    else if (val.value == max1?.value && DistanceToPoint(x, y, val.posX, val.posY) < DistanceToPoint(x, y, max1.posX, max1.posY)) {
                        max1 = val;
                    }
                    else if (val.value == max2?.value && DistanceToPoint(x, y, val.posX, val.posY) < DistanceToPoint(x, y, max2.posX, max2.posY)) {
                        max2 = val;
                    }
                }
            }

            if (max2 != null && DistanceToPoint(x, y, max2.posX, max2.posY) < DistanceToPoint(x, y, max1.posX, max1.posY)) {
                (max1, max2) = (max2, max1);
            }

            return Tuple.Create(max1, max2);

            return Tuple.Create(max1, max2);
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e) {
            if (id == -1) return;

            Measurement selected = new Measurement();
            List<Measurement> list = database.GetMeasurements(id);

            bool found = false;
            foreach (var val in list) {
                if (val.date.Date == dateTimePicker1.Value.Date && DistanceToPoint(e.X, e.Y, val.posX + 10, val.posY + 10) < 10)
                {
                    selected = val;
                    found = true;
                    break;
                }
            }

            if (found) {
                if (selected.value < 40) {
                    MessageBox.Show("Selectați un punct de pe hartă corespunzător\r\nunei măsurări existente în baza de date!");
                    return;
                }

                Tuple<Measurement, Measurement> pair = FindBiggest(e.X, e.Y);

                bool first = false, second = false;
                if (pair.Item1.id == selected.id)
                    first = true;
                if (pair.Item2.id == selected.id)
                    second = true;

                Bitmap bitmap = new Bitmap(pictureBox1.Image);

                if (!first && !second) {
                    using (Graphics g = Graphics.FromImage(bitmap)) {
                        g.DrawLine(new Pen(Color.Red, 1), selected.posX, selected.posY, pair.Item1.posX, pair.Item1.posY);
                        g.DrawLine(new Pen(Color.Red, 1), pair.Item1.posX, pair.Item1.posY, pair.Item2.posX, pair.Item2.posY);
                    }
                } else if (first && !second) {
                    using (Graphics g = Graphics.FromImage(bitmap)) {
                        g.DrawLine(new Pen(Color.Red, 1), selected.posX, selected.posY, pair.Item2.posX, pair.Item2.posY);
                    }
                }
                else if (!first && second) {
                    using (Graphics g = Graphics.FromImage(bitmap)) {
                        g.DrawLine(new Pen(Color.Red, 1), selected.posX, selected.posY, pair.Item1.posX, pair.Item1.posY);
                    }
                }

                pictureBox2.Image = bitmap;
            }
        }
    }
}
