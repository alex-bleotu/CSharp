using ONTI_2019.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2019 {
    public partial class BibliotecarBiblioteca : Form {
        DataBase dataBase;

        string name, password;
        int id;

        int selectedId;

        List<DataBase.User> users;
        List<DataBase.Loan> loans;
        List<DataBase.Reservation> reserved;

        public BibliotecarBiblioteca(int id, string password) {
            InitializeComponent();

            this.id = id;
            this.password = password;

            dataBase = new DataBase();

            name = dataBase.GetName(id);

            label1.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            label2.Text = "Bibliotecar = " + name;
            pictureBox1.Image = (Bitmap)Resources.ResourceManager.GetObject(id.ToString());

            users = dataBase.GetUsers("");
            FillTable();

            tabControl1.TabPages[2].Enabled = false;

            timer1.Start();

            DataGridViewButtonColumn column = new DataGridViewButtonColumn() {
                Name = "Afiseaza",
                Text = "Afiseaza",
                UseColumnTextForButtonValue = true,
            }; 

            dataGridView1.Columns.Add(column);
        }

        private void FillUserData(DataBase.User user) {
            loans = dataBase.GetLoanedBooks(user.id);
            reserved = dataBase.GetReservedBooks(user.id);

            label10.Text = "Rezervari ramase: " + (3 - reserved.Count);
            label11.Text = "Imprumuturi ramase: " + (3 - loans.Count);

            tabControl1.TabPages[2].Enabled = true;
            label9.Text = "Cititor: IdCititor = " + user.id + ", Nume si prenume = " + user.name.Trim();
            pictureBox3.Image = Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\utilizatori\" + user.id + ".jpg");

            DataTable table = new DataTable();

            table.Columns.Add("IdImprumut", typeof(int));
            table.Columns.Add("IdCarte", typeof(int));
            table.Columns.Add("Titlu", typeof(string));
            table.Columns.Add("Autori", typeof(string));
            table.Columns.Add("DataImprumut", typeof(DateTime));
            table.Columns.Add("DataExpirareImprumut", typeof(DateTime));

            DataGridViewButtonColumn column = new DataGridViewButtonColumn() {
                Name = "Restituie",
                Text = "Restituie",
                UseColumnTextForButtonValue = true,
            };

            foreach (DataBase.Loan loan in loans) {
                table.Rows.Add(loan.id, loan.book.id, loan.book.title, loan.book.author, loan.start, loan.end);
            }

            dataGridView2.DataSource = table;

            if (dataGridView2.Columns["Restituie"] == null)
                dataGridView2.Columns.Add(column);

            DataTable table2 = new DataTable();

            table2.Columns.Add("Rezervare", typeof(int));
            table2.Columns.Add("IdCarte", typeof(int));
            table2.Columns.Add("Titlu", typeof(string));
            table2.Columns.Add("Autori", typeof(string));
            table2.Columns.Add("DataRezervare", typeof(DateTime));
            table2.Columns.Add("DataExpirareRezervare", typeof(DateTime));

            foreach (DataBase.Reservation reservation in reserved) {
                table2.Rows.Add(reservation.id, reservation.book.id, reservation.book.title, reservation.book.author, reservation.start, reservation.end);
            }

            DataGridViewButtonColumn column2 = new DataGridViewButtonColumn() {
                Name = "Anuleaza",
                Text = "Anuleaza",
                UseColumnTextForButtonValue = true,
            };

            DataGridViewButtonColumn column3 = new DataGridViewButtonColumn() {
                Name = "Imprumuta",
                Text = "Imprumuta",
                UseColumnTextForButtonValue = true,
            };

            dataGridView3.DataSource = table2;

            if (dataGridView3.Columns["Anuleaza"] == null)
                dataGridView3.Columns.Add(column2);

            if (dataGridView3.Columns["Imprumuta"] == null)
                dataGridView3.Columns.Add(column3);

            BookTable("", "");
            textBox2.Clear();
            textBox3.Clear();
        }

        private void dataGridView1_CellContentClick(Object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == dataGridView1.Columns["Afiseaza"].Index) {
                tabControl1.SelectedTab = tabControl1.TabPages[2];

                FillUserData(users[e.RowIndex]);
                selectedId = e.RowIndex;
            }
        }

        private void FillTable() {
            DataTable table = new DataTable();

            table.Columns.Add("IDCititor", typeof(int));
            table.Columns.Add("NumePrenume", typeof(string));
            table.Columns.Add("Email", typeof(string));

            foreach (DataBase.User user in users) {
                table.Rows.Add(user.id, user.name, user.email);
            }

            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Hide();
            LogareBiblioteca logareBiblioteca = new LogareBiblioteca();
            logareBiblioteca.Show();
        }

        private void button3_Click(object sender, EventArgs e) {
            username.Clear();
            email.Clear();
            pass.Clear();
            pictureBox2.Image = null;
            confirm.Clear();
        }

        private void button2_Click(object sender, EventArgs e) {
            if (username.Text == "" || email.Text == "" || pass.Text == "" || confirm.Text == "")
                MessageBox.Show("Datele nu au fost completate.");
            else if (!Regex.IsMatch(email.Text, @"^[a-zA-Z0-9._+-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]{2,}$"))
                MessageBox.Show("Emailul nu are formatul bun.");
            else if (pass.Text != confirm.Text)
                MessageBox.Show("Parola nu coincide.");
            else if (dataBase.EmailExists(email.Text))
                MessageBox.Show("Emailul a fost deja folosit.");
            else if (pictureBox2.Image == null)
                MessageBox.Show("Selectati o imagine");
            else if (!radioButton1.Checked && !radioButton2.Checked)
                MessageBox.Show("Alegeti un tip de utilizator.");
            else {
                dataBase.AddUser(username.Text, email.Text, radioButton1.Checked ? 1 : 2, pass.Text, pictureBox2.Image);

                MessageBox.Show("Cititorul a fost adaugat.");

                email.Clear();
                pass.Clear();
                pictureBox2.Image = null;
                confirm.Clear();
                username.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            using (OpenFileDialog open = new OpenFileDialog()) {
                open.InitialDirectory = Application.StartupPath + @"\Resurse\Imagini\altele";

                if (open.ShowDialog() == DialogResult.OK) {
                    pictureBox2.Image = Image.FromFile(open.FileName);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e) {
            users = dataBase.GetUsers(textBox1.Text);
            FillTable();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            try {
                if (e.ColumnIndex == dataGridView2.Columns["Restituie"].Index) {
                    dataBase.CancelLoan(Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString()));

                    FillUserData(users[selectedId]);
                }
            } catch { }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            try {
                if (e.ColumnIndex == dataGridView3.Columns["Anuleaza"].Index) {
                    dataBase.CancelReservation(Int32.Parse(dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString()));

                    FillUserData(users[selectedId]);
                }

                if (e.ColumnIndex == dataGridView3.Columns["Imprumuta"].Index) {
                    dataBase.TakeLoan(Int32.Parse(dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString()), users[selectedId].id);

                    FillUserData(users[selectedId]);
                }
            } catch { }
        }

        private void BookTable(string title, string author) {
            DataTable table = new DataTable();

            table.Columns.Add("IdCarte", typeof(int));
            table.Columns.Add("Titlu", typeof(string));
            table.Columns.Add("Autor", typeof(string));
            table.Columns.Add("NrPag", typeof(int));

            List<DataBase.Book> books = dataBase.GetAllBooks(loans, reserved, title, author);

            foreach (DataBase.Book book in books) {
                table.Rows.Add(book.id, book.title, book.author, book.pageNumber);
            }

            dataGridView4.DataSource = table;

            if (dataGridView4.Columns["Rezerva"] == null) {
                DataGridViewButtonColumn column = new DataGridViewButtonColumn() {
                    Name = "Rezerva",
                    Text = "Rezerva",
                    UseColumnTextForButtonValue = true,
                };

                dataGridView4.Columns.Add(column);
            }

            if (dataGridView4.Columns["Imprumuta"] == null) {
                DataGridViewButtonColumn column = new DataGridViewButtonColumn() {
                    Name = "Imprumuta",
                    Text = "Imprumuta",
                    UseColumnTextForButtonValue = true,
                };

                dataGridView4.Columns.Add(column);
            }

            if (dataGridView4.Columns["Vizualizare"] == null) {
                DataGridViewButtonColumn column = new DataGridViewButtonColumn() {
                    Name = "Vizualizare",
                    Text = "Vizualizare",
                    UseColumnTextForButtonValue = true,
                };

                dataGridView4.Columns.Add(column);
            }
        }

        private void button6_Click(object sender, EventArgs e) {
            BookTable(textBox2.Text, textBox3.Text);
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            try {
                if (e.ColumnIndex == dataGridView4.Columns["Rezerva"].Index && reserved.Count < 3) {
                    dataBase.CreateReservation(Int32.Parse(dataGridView4.Rows[e.RowIndex].Cells[3].Value.ToString()), users[selectedId].id);

                    FillUserData(users[selectedId]);
                }

                if (e.ColumnIndex == dataGridView4.Columns["Imprumuta"].Index && loans.Count < 3) {
                    dataBase.CreateLoan(Int32.Parse(dataGridView4.Rows[e.RowIndex].Cells[3].Value.ToString()), users[selectedId].id);

                    FillUserData(users[selectedId]);
                }

                if (e.ColumnIndex == dataGridView4.Columns["Vizualizare"].Index) {
                    PrevizualizareCarte form = new PrevizualizareCarte(Int32.Parse(dataGridView4.Rows[e.RowIndex].Cells[3].Value.ToString()));
                    form.Show();
                    this.Hide();
                }
            }
            catch { }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            label1.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
        }
    }
}
