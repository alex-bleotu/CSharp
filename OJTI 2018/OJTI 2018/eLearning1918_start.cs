using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OJTI_2018
{
    public partial class eLearning1918_start : Form
    {
        List<Image> images;
        int currentIndex = 0;
        DataBase dataBase;

        public eLearning1918_start()
        {
            InitializeComponent();

            dataBase = new DataBase();

            images = new List<Image>();
            var files = Directory.GetFiles(Application.StartupPath + @"\csarp\imaginislideshow");
            foreach (var file in files)
            {
                images.Add(Image.FromFile(file));
            }
            timer1.Start();
            forwardButton.Enabled = false;
            backButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentIndex++;
            if (currentIndex == images.Count)
                currentIndex = 0;
            if (currentIndex == 0)
                progressBar.Value = 20;
            else
                progressBar.Value = (int)(100 / ((float)images.Count / (currentIndex + 1)));
            pictureBox.Image = images[currentIndex];
        }

        private void modeButton_Click(object sender, EventArgs e)
        {
            if (modeButton.Text == "Manual")
            {
                modeButton.Text = "Auto";
                timer1.Stop();
                if (currentIndex == images.Count - 1)
                {
                    forwardButton.Enabled = false;
                    backButton.Enabled = true;
                }
                if (currentIndex == images.Count)
                {
                    currentIndex = 0;
                    backButton.Enabled = false;
                    forwardButton.Enabled = true;
                }
                else
                {
                    forwardButton.Enabled = true;
                    backButton.Enabled = true;
                }
            } else
            {
                modeButton.Text = "Manual";
                timer1.Start();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            currentIndex--;
            forwardButton.Enabled = true;
            if (currentIndex == 0)
                backButton.Enabled = false;
            pictureBox.Image = images[currentIndex];
            if (currentIndex == 0)
                progressBar.Value = 20;
            else
                progressBar.Value = (int)(100 / ((float)images.Count / (currentIndex + 1)));
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            currentIndex++;
            backButton.Enabled = true;
            if (currentIndex == images.Count - 1)
                forwardButton.Enabled = false;
            pictureBox.Image = images[currentIndex];
            if (currentIndex == 0)
                progressBar.Value = 20;
            else
                progressBar.Value = (int)(100 / ((float)images.Count / (currentIndex + 1)));
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (emailTextBox.Text != "" && passwordTextBox.Text != "")
            {
                int id = dataBase.Login(emailTextBox.Text, passwordTextBox.Text);

                if (id != -1)
                {
                    eLearning1918_Elev form = new eLearning1918_Elev(id);
                    emailTextBox.Clear();
                    passwordTextBox.Clear();
                    this.Hide();
                    form.Show();
                }
                else
                    MessageBox.Show("Emailul sau parola sunt incorecte.");
            }

        }
    }
}
