using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ONTI_2019___V2.Database;

namespace ONTI_2019___V2 {
    public partial class BibliotecarBiblioteca : Form {
        Database db = new Database();

        bool name = false;
        bool email = false;
        bool password = false;
        bool repeat = false;
        bool choice = false;

        string image = "";

        int selectedId = -1;

        public BibliotecarBiblioteca(int id) {
            InitializeComponent();

            label2.Text = DateTime.Now.ToString();
            pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\utilizatori\" + id + ".jpg");
            label1.Text = "Bibliotecar: " + db.GetData(id);

            FillDataView();
        }

        private void BibliotecarBiblioteca_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e) {

            this.Hide();
            PrevizualizareCarte form = new PrevizualizareCarte();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e) {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            image = "";
            pictureBox2.Image = null;
        }

        void check() {
            if (name && email && password && repeat && image != "" && choice)
                button2.Enabled = true;
            else button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e) {
            if (db.CheckIfExists(textBox2.Text.Trim()))
                MessageBox.Show("Email is already used");
            else if (textBox3.Text != textBox4.Text)
                MessageBox.Show("Parola nu coincide");
            else {
                db.CreateUser(textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), radioButton1.Checked ? 1 : 2);
                int id = db.Login(textBox2.Text.Trim(), textBox3.Text.Trim());
                Image img = pictureBox2.Image;
                img.Save(Application.StartupPath + @"\Resurse\Imagini\utilizatori\" + id + ".jpg");

                MessageBox.Show("Utilizator inregistrat"); 
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                image = "";
                pictureBox2.Image = null;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            if (textBox1.Text == "")
                name = false;
            else name = true;
            check();
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {
            if (textBox2.Text == "")
                email = false;
            else {
                try {
                    MailAddress m = new MailAddress(textBox2.Text.Trim());
                    email = true;
                } catch {
                    email = false;
                }
            }
            check();
        }

        private void textBox3_TextChanged(object sender, EventArgs e) {
            if (textBox3.Text == "")
                password = false;
            else password = true;
            check();
        }

        private void textBox4_TextChanged(object sender, EventArgs e) {
            if (textBox1.Text == "")
                repeat = false;
            else repeat = true;
            check();
        }

        private void button4_Click(object sender, EventArgs e) {
            using (OpenFileDialog dialog = new OpenFileDialog()) {
                dialog.Filter = "Image Files|*jpg; *jpeg";
                if (dialog.ShowDialog() == DialogResult.OK) {
                    image = dialog.FileName;
                    pictureBox2.Image = Image.FromFile(image);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            choice = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            choice = true;
        }

        private void button5_Click(object sender, EventArgs e) {
            FillDataView();
        }


        void FillDataView() {
            dataGridView1.DataSource = new BindingList<User>(db.GetUsers(textBox5.Text));

            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.Text = "Afiseaza";
            col.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add(col);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0) {
                selectedId = ((User)dataGridView1.Rows[e.RowIndex].DataBoundItem).IdCititor;

                tabControl1.SelectedIndex = 2;

                label9.Text = "Cititor: IdCititor=" + selectedId + ", Nume si prenume=" + db.GetData(selectedId);
                pictureBox3.Image = Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\utilizatori\" + selectedId + ".jpg");

                List<Imp> imps = db.GetImp(selectedId);

                label10.Text = "Rezervari ramase= ";
                label11.Text = "Imprumuturi ramase= " + imps.Count;

                dataGridView2.DataSource = imps;

                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.Text = "Restituie";
                col.UseColumnTextForButtonValue = true;
                dataGridView2.Columns.Add(col);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
            if (tabControl1.SelectedIndex == 2 && selectedId == -1)
                tabControl1.SelectedIndex = 0;
        }

        private void pictureBox3_Click(object sender, EventArgs e) {
        }
    }
}
