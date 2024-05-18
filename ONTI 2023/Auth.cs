using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2023
{
    public partial class Auth : Form
    {
        Database database = new Database();
        
        public Auth()
        {
            InitializeComponent();

            database.Populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (database.Login(emailTextBox.Text, passwordTextBox.Text))
            {
                this.Hide();
                AlegeJoc alegeJoc = new AlegeJoc(emailTextBox.Text);
                alegeJoc.Show();

                emailTextBox.Clear();
                passwordTextBox.Clear();
            } else
                MessageBox.Show("Date de autentificare invalide!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            emailTextBox.Clear();
            passwordTextBox.Clear();

            this.Hide();
            Register form = new Register();
            form.Show();
        }

        private void qrButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Application.StartupPath + @"\Resources\QRCode";
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        pictureBox1.Image = new Bitmap(filePath);
                    } catch { }
                }
            }
        }
    }
}
