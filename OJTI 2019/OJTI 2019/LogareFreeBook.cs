using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI_2019
{
    public partial class LogareFreeBook : Form
    {
        DataBase dataBase;

        public LogareFreeBook()
        {
            InitializeComponent();
            dataBase = new DataBase();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (dataBase.Login(emailTextBox.Text, parolaTextBox.Text))
            {
                this.Hide();
                MeniuFreeBook form = new MeniuFreeBook(emailTextBox.Text);
                parolaTextBox.Clear();
                emailTextBox.Clear();
                form.Show();
            }
            else
                MessageBox.Show("Emailul sau parola sunt incorecte.");
        }

        private void LogareFreeBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            FreeBookHome form = new FreeBookHome(false);
            form.Show();
        }
    }
}
