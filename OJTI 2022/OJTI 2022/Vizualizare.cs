using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI_2022
{
    public partial class Vizualizare : Form
    {
        DataBase dataBase;
        string username;
        List<string> maps;

        public Vizualizare(string username)
        {
            InitializeComponent();

            dataBase = new DataBase();

            filtersComboBox.SelectedItem = "Niciun filtru";
            userLabel.Text = "Utilizator: " + username;
            this.username = username;

            dateTimerPicker.Value = DateTime.Parse("5/14/2022 19:10:00");

            maps = dataBase.GetMaps();
            foreach (string map in maps)
                mapComboBox.Items.Add(map);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            filtersComboBox.SelectedItem = "Niciun filtru";
        }

        private void Vizualizare_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Autentificare form = new Autentificare();
            form.Show();
        }

        private void mapComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            if (filtersComboBox.SelectedIndex == -1 || mapComboBox.SelectedIndex == -1)
                return;

            pictureBox.Image = Image.FromFile(Application.StartupPath + @"\Harti\" + dataBase.GetFileMap(mapComboBox.Items[mapComboBox.SelectedIndex].ToString()));

            List<DataBase.Point> points = dataBase.GetPoints(mapComboBox.Items[mapComboBox.SelectedIndex].ToString());

            Bitmap bmp = new Bitmap(pictureBox.Image);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                foreach (DataBase.Point p in points)
                {
                    if (dateTimerPicker.Value.Date != p.date.Date)
                        continue;

                    string str = filtersComboBox.Items[filtersComboBox.SelectedIndex].ToString();
                    if (str == "Valoarea < 20" && p.val >= 20)
                        continue;
                    else if (str == "20 <= Valoarea <= 40" && (p.val < 20 || p.val > 40))
                        continue;
                    else if (str == "Valoarea > 40" && p.val <= 40)
                        continue;

                    Pen pen;
                    Brush brush;
                    if (p.val < 20)
                    { pen = new Pen(Color.Green); brush = Brushes.Green; }
                    else if (p.val <= 40 && p.val >= 20)
                    { pen = new Pen(Color.Yellow); brush = Brushes.Yellow; }
                    else
                    { pen = new Pen(Color.Red); brush = Brushes.Red; }

                    g.DrawEllipse(pen, p.x, p.y, 20, 20);
                    g.DrawString(p.val.ToString(), new Font("Arial", 12), brush, p.x, p.y);
                }
            }

            pictureBox.Image = bmp;
            pictureBox2.Image = bmp;
        }

        private void filtersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void dateTimerPicker_ValueChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            AdaugareMasura form = new AdaugareMasura(username, e.X, e.Y, mapComboBox.Items[mapComboBox.SelectedIndex].ToString());
            form.Show();
        }

        private void IsInRange()

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (filtersComboBox.SelectedIndex == -1 || mapComboBox.SelectedIndex == -1)
                return;

            pictureBox.Image = Image.FromFile(Application.StartupPath + @"\Harti\" + dataBase.GetFileMap(mapComboBox.Items[mapComboBox.SelectedIndex].ToString()));

            List<DataBase.Point> points = dataBase.GetPoints(mapComboBox.Items[mapComboBox.SelectedIndex].ToString());

            Bitmap bmp = new Bitmap(pictureBox.Image);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                foreach (DataBase.Point p in points)
                {
                    if (dateTimerPicker.Value.Date != p.date.Date)
                        continue;

                    string str = filtersComboBox.Items[filtersComboBox.SelectedIndex].ToString();
                    if (str == "Valoarea < 20" && p.val >= 20)
                        continue;
                    else if (str == "20 <= Valoarea <= 40" && (p.val < 20 || p.val > 40))
                        continue;
                    else if (str == "Valoarea > 40" && p.val <= 40)
                        continue;
                }
            }
        }
    }
}
