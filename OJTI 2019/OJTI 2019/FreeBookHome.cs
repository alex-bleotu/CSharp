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
    public partial class FreeBookHome : Form
    {
        DataBase dataBase;

        public FreeBookHome(bool populate = true)
        {
            InitializeComponent();

            dataBase = new DataBase();

            if (populate)
                dataBase.Populate();
        }

        private void FreeBookHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreeazaContFreeBook form = new CreeazaContFreeBook();
            form.Show();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogareFreeBook form = new LogareFreeBook();
            form.Show();
        }
    }
}
