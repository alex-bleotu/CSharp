using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2018 {
    public partial class VizualizareLectii : Form {
        DataBase dataBase;

        List<Leason> leasons;

        public VizualizareLectii() {
            InitializeComponent();

            dataBase = new DataBase();

            leasons = dataBase.GetLeasons();

            foreach (Leason le in leasons)
                listBox1.Items.Add(le.title);
        }

        private void button1_Click(object sender, EventArgs e) {
            MessageBox.Show(Application.StartupPath + @"\Resurse\ContinutLectii\" + listBox1.SelectedItem.ToString() + ".bmp");
            pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\Resurse\ContinutLectii\" + listBox1.SelectedItem.ToString() + ".bmp");
        }
    }
}
