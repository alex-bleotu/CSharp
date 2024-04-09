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
    public partial class MeniuFreeBook : Form
    {
        public MeniuFreeBook(string email)
        {
            InitializeComponent();

            title.Text = "Email: " + email;
        }

        private void MeniuFreeBook_Load(object sender, EventArgs e)
        {

        }
    }
}
