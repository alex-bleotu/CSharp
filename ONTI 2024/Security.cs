using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2024 {
    public partial class Security : Form {
        Database db = new Database();

        private List<string> images;
        private string imageValue;

        private User user;
        private int action;

        private List<bool> selected = new List<bool>() { false, false, false, false, false, false };

        public Security(User u, int a) {
            InitializeComponent();

            user = u;
            action = a;

            List<string> values = new List<string> { "Luna", "Pamant", "Soare" };

            Random random = new Random();
            int r = random.Next(0, 3);

            label1.Text = label1.Text.Replace("-", values[r]);
            imageValue = values[r];

            images = new List<string>();

            var moon = new List<string>();
            var earth = new List<string>();
            var sun = new List<string>();
            for (int i = 1; i <= 4; i++) {
                moon.Add(Application.StartupPath + "\\Resurse\\ImaginiValidare\\Luna" + i + ".png");
                earth.Add(Application.StartupPath + "\\Resurse\\ImaginiValidare\\Pamant" + i + ".png");
                sun.Add(Application.StartupPath + "\\Resurse\\ImaginiValidare\\Soare" + i + ".png");
            }

            if (r == 0) {
                int value = random.Next(0, 4);
                images.Add(moon[value]);
                moon.RemoveAt(value);
                value = random.Next(0, 3);
                images.Add(moon[value]);
                moon.RemoveAt(value);
                value = random.Next(0, 2);
                images.Add(moon[value]);
                moon.RemoveAt(value);

                value = random.Next(0, 4);
                images.Add(sun[value]);
                sun.RemoveAt(value);

                value = random.Next(0, 4);
                images.Add(earth[value]);
                earth.RemoveAt(value);

                int value2 = random.Next(0, 2);
                value = random.Next(0, 3);
                if (value2 == 0) {
                    images.Add(earth[value]);
                    earth.RemoveAt(value);
                } else {
                    images.Add(earth[value]);
                    earth.RemoveAt(value);
                }
            } else if (r == 1) {
                int value = random.Next(0, 4);
                images.Add(earth[value]);
                earth.RemoveAt(value);
                value = random.Next(0, 3);
                images.Add(earth[value]);
                earth.RemoveAt(value);
                value = random.Next(0, 2);
                images.Add(earth[value]);
                earth.RemoveAt(value);

                value = random.Next(0, 4);
                images.Add(sun[value]);
                sun.RemoveAt(value);

                value = random.Next(0, 4);
                images.Add(moon[value]);
                moon.RemoveAt(value);

                int value2 = random.Next(0, 2);
                value = random.Next(0, 3);
                if (value2 == 0) {
                    images.Add(sun[value]);
                    sun.RemoveAt(value);
                }
                else {
                    images.Add(moon[value]);
                    moon.RemoveAt(value);
                }
            } else if (r == 2) {
                int value = random.Next(0, 4);
                images.Add(sun[value]);
                sun.RemoveAt(value);
                value = random.Next(0, 3);
                images.Add(sun[value]);
                sun.RemoveAt(value);
                value = random.Next(0, 2);
                images.Add(sun[value]);
                sun.RemoveAt(value);

                value = random.Next(0, 4);
                images.Add(moon[value]);
                moon.RemoveAt(value);

                value = random.Next(0, 4);
                images.Add(earth[value]);
                earth.RemoveAt(value);

                int value2 = random.Next(0, 2);
                value = random.Next(0, 3);
                if (value2 == 0) {
                    images.Add(moon[value]);
                    moon.RemoveAt(value);
                }
                else {
                    images.Add(earth[value]);
                    earth.RemoveAt(value);
                }
            }

            var newList = new List<string>(images);
            for (int i = 0; i < 6; i++) {
                int value = random.Next(0, newList.Count);
                images[i] = newList[value];
                newList.RemoveAt(value);
            }

            pictureBox1.Image = Image.FromFile(images[0]);
            pictureBox2.Image = Image.FromFile(images[1]);
            pictureBox3.Image = Image.FromFile(images[2]);
            pictureBox4.Image = Image.FromFile(images[3]);
            pictureBox5.Image = Image.FromFile(images[4]);
            pictureBox6.Image = Image.FromFile(images[5]);
        }

        private void button1_Click(object sender, EventArgs e) {
            bool ok = true;

            for (int i = 0; i < 6; i++)
                if (images[i].Contains(imageValue) && !selected[i] || !images[i].Contains(imageValue) && selected[i]) {
                    ok = false;
                    break;
                }

            if (ok) {
                if (action == 0) {
                    System.IO.File.WriteAllText(Application.StartupPath + "\\saved.txt", "");
                } else if (action == 1) {
                    bool result = db.Register(user);

                    if (result) MessageBox.Show("Utilizatorul a fost inregistrat");
                } else if (action == 2)
                    System.IO.File.WriteAllText(Application.StartupPath + "\\saved.txt", user.email);

                this.Hide();
                Calendar form = new Calendar(user);
                form.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            selected[0] = !selected[0];
            if (selected[0])
                panel1.BackColor = Color.Red;
            else panel1.BackColor = Color.Transparent;
        }

        private void pictureBox2_Click(object sender, EventArgs e) {
            selected[1] = !selected[1];
            if (selected[1])
                panel2.BackColor = Color.Red;
            else panel2.BackColor = Color.Transparent;
        }

        private void pictureBox3_Click(object sender, EventArgs e) {
            selected[2] = !selected[2];
            if (selected[2])
                panel3.BackColor = Color.Red;
            else panel3.BackColor = Color.Transparent;
        }

        private void pictureBox4_Click(object sender, EventArgs e) {
            selected[3] = !selected[3];
            if (selected[3])
                panel4.BackColor = Color.Red;
            else panel4.BackColor = Color.Transparent;
        }

        private void pictureBox5_Click(object sender, EventArgs e) {
            selected[4] = !selected[4];
            if (selected[4])
                panel5.BackColor = Color.Red;
            else panel5.BackColor = Color.Transparent;
        }

        private void pictureBox6_Click(object sender, EventArgs e) {
            selected[5] = !selected[5];
            if (selected[5])
                panel6.BackColor = Color.Red;
            else panel6.BackColor = Color.Transparent;
        }

        private void Security_FormClosed(object sender, FormClosedEventArgs e) {
            this.Hide();
            if (action == 1) {
                Register form = new Register();
                form.Show();
            } else {
                Auth form = new Auth();
                form.Show();
            }
        }
    }
}
