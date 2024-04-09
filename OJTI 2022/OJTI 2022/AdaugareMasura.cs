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
    public partial class AdaugareMasura : Form
    {
        string name;
        int x, y;
        string map;
        DataBase dataBase;

        public AdaugareMasura(string name, int x, int y, string map)
        {
            this.x = x;
            this.y = y;
            this.map = map;
            this.name = name;
            InitializeComponent();

            dataBase = new DataBase();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (textBox.Text == "")
                return;
            
            try
            {
                int nr = Int32.Parse(textBox.Text);
            } catch
            {
                MessageBox.Show("Valoarea nu este un numar.");
                return;
            }


            dataBase.AddMeasurement(map, Int32.Parse(textBox.Text), x, y);
            this.Hide();
            Vizualizare form = new Vizualizare(name);
            form.Show();
        }

        private void AdaugareMasura_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Vizualizare form = new Vizualizare(name);
            form.Show();
        }
    }
}
