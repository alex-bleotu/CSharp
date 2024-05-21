using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ONTI_2019.DataBase;

namespace ONTI_2019 {
    internal class DataBase {
        SqlConnection connection;

        string format = "MM/dd/yyyy hh/mm/ss tt";

        public class User {
            public int id;
            public string name;
            public string email;

            public User(int id, string name, string email) {
                this.id = id;
                this.name = name;
                this.email = email;
            }
        }

        public class Book {
            public int id;
            public string title;
            public string author;
            public int pageNumber;

            public Book(int id, string title, string author, int pageNumber) {
                this.id = id;
                this.title = title;
                this.author = author;
                this.pageNumber = pageNumber;
            }
        }

        public class Loan {
            public int id;
            public Book book;
            public DateTime start;
            public DateTime end;

            public Loan(int id, DateTime start, DateTime end, Book book) {
                this.id = id;
                this.book = book;
                this.start = start;
                this.end = end;
            }
        }

        public class Reservation {
            public int id;
            public Book book;
            public DateTime start;
            public DateTime end;

            public Reservation(int id, DateTime start, DateTime end, Book book) {
                this.id = id;
                this.book = book;
                this.start = start;
                this.end = end;
            }
        }

        public DataBase() {
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\Biblioteca.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();
        }

        public void EraseTable(string tableName) {
            SqlCommand command = new SqlCommand("DELETE FROM " + tableName + " DBCC CHECKIDENT (" + tableName + ", RESEED, 0);", connection);

            command.ExecuteNonQuery();
        }

        public void Populate() {
            EraseTable("Rezervari");
            EraseTable("Imprumuturi");
            EraseTable("Carti");
            EraseTable("Utilizatori");

            using (StreamReader file = new StreamReader(Application.StartupPath + "\\Resurse\\utilizatori.txt")) {
                string line;

                while ((line = file.ReadLine()) != null) {
                    var fields = line.Split(';');

                    SqlCommand command = new SqlCommand("INSERT INTO Utilizatori (TipUtilizator, NumePrenume, Email, Parola) VALUES (@a, @b, @c, @d);", connection);

                    command.Parameters.AddWithValue("@a", Int32.Parse(fields[0]));
                    command.Parameters.AddWithValue("@b", fields[1]);
                    command.Parameters.AddWithValue("@c", fields[2]);

                    if (fields[3] != "")
                        command.Parameters.AddWithValue("@d", Encrypt(fields[3]));
                    else command.Parameters.AddWithValue("@d", DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }

            using (StreamReader file = new StreamReader(Application.StartupPath + "\\Resurse\\carti.txt")) {
                string line;

                while ((line = file.ReadLine()) != null) {
                    var fields = line.Split(';');

                    SqlCommand command = new SqlCommand("INSERT INTO Carti (Titlu, Autor, Nrpag) VALUES (@a, @b, @c);", connection);

                    command.Parameters.AddWithValue("@a", fields[0]);
                    command.Parameters.AddWithValue("@b", fields[1]);
                    command.Parameters.AddWithValue("@c", Int32.Parse(fields[2]));

                    command.ExecuteNonQuery();
                }
            }

            using (StreamReader file = new StreamReader(Application.StartupPath + "\\Resurse\\rezervari.txt")) {
                string line;

                while ((line = file.ReadLine()) != null) {
                    var fields = line.Split(';');

                    SqlCommand command = new SqlCommand("INSERT INTO Rezervari (IdCititor, IdCarte, DataRezervare, StatusRezervare) VALUES (@a, @b, @c, @d);", connection);

                    command.Parameters.AddWithValue("@a", Int32.Parse(fields[0]));
                    command.Parameters.AddWithValue("@b", Int32.Parse(fields[1]));
                    command.Parameters.AddWithValue("@c", DateTime.ParseExact(fields[2], format, CultureInfo.InvariantCulture));
                    command.Parameters.AddWithValue("@d", Int32.Parse(fields[3]));

                    command.ExecuteNonQuery();
                }
            }

            using (StreamReader file = new StreamReader(Application.StartupPath + "\\Resurse\\imprumuturi.txt")) {
                string line;

                while ((line = file.ReadLine()) != null) {
                    var fields = line.Split(';');

                    SqlCommand command = new SqlCommand("INSERT INTO Imprumuturi (IdCititor, IdCarte, DataImprumut, DataRestituire) VALUES (@a, @b, @c, @d);", connection);

                    command.Parameters.AddWithValue("@a", Int32.Parse(fields[0]));
                    command.Parameters.AddWithValue("@b", Int32.Parse(fields[1]));
                    command.Parameters.AddWithValue("@c", DateTime.ParseExact(fields[2], format, CultureInfo.InvariantCulture));
                    if (fields[3] != "NULL")
                        command.Parameters.AddWithValue("@d", DateTime.ParseExact(fields[3], format, CultureInfo.InvariantCulture));
                    else command.Parameters.AddWithValue("@d", DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }

        public string Encrypt(string password) {
            char[] str = new char[password.Length];

            for (int i = 0; i < password.Length; i++) {
                char c = password[i];

                if (char.IsLower(c)) str[i] = c == 'z' ? 'a' : (char)(c + 1);
                else if (char.IsUpper(c)) str[i] = c == 'A' ? 'Z' : (char)(c - 1);
                else if (char.IsDigit(c)) str[i] = (char)('9' - (c - '0'));
                else str[i] = c;
            }

            return new string(str);
        }

        public int Login(string email, string password) {
            SqlCommand command = new SqlCommand("SELECT IdUtilizator FROM Utilizatori WHERE Email='" + email + "' AND Parola='" + Encrypt(password) + "';", connection);

            var id = command.ExecuteScalar();

            if (id == null)
                return -1;

            try {
                return Convert.ToInt32(id.ToString());
            }
            catch { return -1; }
        }

        public string GetName(int id) {
            SqlCommand command = new SqlCommand("SELECT NumePrenume From Utilizatori WHERE IdUtilizator=" + id + ";", connection);

            return command.ExecuteScalar().ToString();
        }

        public bool EmailExists(string email) {
            SqlCommand command = new SqlCommand("SELECT IdUtilizator FROM Utilizatori WHERE Email='" + email + "';", connection);

            var id = command.ExecuteScalar();

            if (id != null)
                return true;
            return false;
        }

        public void AddUser(string name, string email, int type, string password, Image image) {
            SqlCommand command = new SqlCommand("INSERT INTO Utilizatori (NumePrenume, TipUtilizator, Email, Parola) VALUES (@a, @b, @c, @d);", connection);

            command.Parameters.AddWithValue("@a", name);
            command.Parameters.AddWithValue("@b", type);
            command.Parameters.AddWithValue("@c", email);

            if (type == 1)
                command.Parameters.AddWithValue("@d", Encrypt(password));
            else
                command.Parameters.AddWithValue("@d", DBNull.Value);

            command.ExecuteNonQuery();

            SqlCommand command2 = new SqlCommand("SELECT IdUtilizator FROM Utilizatori WHERE Email='" + email + "';", connection);

            var id = command2.ExecuteScalar();

            if (id == null)
                return;

            image.Save(Application.StartupPath + @"\Resurse\Imagini\utilizatori\" + Int32.Parse(id.ToString()) + ".jpg");
        }

        public List<User> GetUsers(string filter) {
            List<User> users = new List<User>();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT IdUtilizator, Email, NumePrenume FROM Utilizatori WHERE TipUtilizator=2;", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);

            foreach(DataRow row in table.Rows) {
                if (row[2].ToString().ToLower().Contains(filter.ToLower()))
                    users.Add(new User(Int32.Parse(row[0].ToString()), row[2].ToString(), row[1].ToString()));
            }

            return users;
        }

        public Book GetBook(int id) {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Titlu, Autor, Nrpag FROM Carti WHERE IdCarte=" + id + ";", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);
            foreach (DataRow row in table.Rows) {
                return new Book(id, row[0].ToString(), row[1].ToString(), Int32.Parse(row[2].ToString()));
            }

            return null;
        }

        public List<Loan> GetLoanedBooks(int id) {
            List<Loan> loans = new List<Loan>();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT IdImprumut, IdCarte, DataImprumut FROM Imprumuturi WHERE DataRestituire is NULL AND IdCititor=" + id + ";", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);
            foreach (DataRow row in table.Rows) {
                loans.Add(new Loan(
                    Int32.Parse(row[0].ToString()),
                    DateTime.Parse(row[2].ToString()),
                    DateTime.Parse(row[2].ToString()).AddDays(7),
                    GetBook(Int32.Parse(row[1].ToString())
                    )));
            }

            return loans;
        }
        public List<Reservation> GetReservedBooks(int id) {
            List<Reservation> reserved = new List<Reservation>();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT IdRezervare, IdCarte, DataRezervare FROM Rezervari WHERE StatusRezervare=1 AND IdCititor=" + id + ";", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);
            foreach (DataRow row in table.Rows) {
                reserved.Add(new Reservation(
                    Int32.Parse(row[0].ToString()),
                    DateTime.Parse(row[2].ToString()),
                    DateTime.Parse(row[2].ToString()).AddDays(1),
                    GetBook(Int32.Parse(row[1].ToString())
                    )));
            }

            return reserved;
        }

        public void CancelLoan(int id) {
            SqlCommand command = new SqlCommand("UPDATE Imprumuturi SET DataRestituire='" + DateTime.Now + "' WHERE IdImprumut=" + id + ";", connection);

            command.ExecuteNonQuery();
        }

        public void CancelReservation(int id) {
            SqlCommand command = new SqlCommand("Update Rezervari SET StatusRezervare=0 WHERE IdRezervare=" + id, connection);

            command.ExecuteNonQuery();
        }

        public void TakeLoan(int id, int user) {
            SqlCommand command3 = new SqlCommand("SELECT IdCarte FROM Rezervari WHERE IdRezervare=" + id, connection);

            int bookId = Int32.Parse(command3.ExecuteScalar().ToString());

            SqlCommand command = new SqlCommand("Update Rezervari SET StatusRezervare=0 WHERE IdRezervare=" + id, connection);

            command.ExecuteNonQuery();

            SqlCommand command2 = new SqlCommand("INSERT INTO Imprumuturi (IdCititor, IdCarte, DataImprumut, DataRestituire) VALUES (@a, @b, @c, @d);", connection);

            command2.Parameters.AddWithValue("@a", user);
            command2.Parameters.AddWithValue("@b", bookId);
            command2.Parameters.AddWithValue("@c", DateTime.Now);
            command2.Parameters.AddWithValue("@d", DBNull.Value);

            command2.ExecuteNonQuery();
        }

        public List<Book> GetAllBooks(List<Loan> loans, List<Reservation> reserved, string title, string autor) {
            List<Book> books = new List<Book>();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT IdCarte, Titlu, Autor, Nrpag FROM Carti", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);

            foreach (DataRow row in table.Rows) {
                books.Add(new Book(Int32.Parse(row[0].ToString()), row[1].ToString(), row[2].ToString(), Int32.Parse(row[3].ToString())));
            }

            foreach (Loan loan in loans)
                for (int i = 0; i < books.Count; i++)
                    if (books[i].id == loan.book.id)
                        books.RemoveAt(i);

            foreach (Reservation reservation in reserved)
                for (int i = 0; i < books.Count; i++)
                    if (books[i].id == reservation.book.id)
                        books.RemoveAt(i);

            for (int i = 0; i < books.Count; i++)
                if (!books[i].title.ToLower().Contains(title.ToLower()) || !books[i].author.ToLower().Contains(autor.ToLower()))
                    books.RemoveAt(i);

            return books;
        }

        public void CreateReservation(int book, int user) {
            SqlCommand command = new SqlCommand("INSERT INTO Rezervari (IdCititor, IdCarte, DataRezervare, StatusRezervare) VALUES (@a, @b, @c, @d);", connection);

            command.Parameters.AddWithValue("@a", user);
            command.Parameters.AddWithValue("@b", book);
            command.Parameters.AddWithValue("@c", DateTime.Now);
            command.Parameters.AddWithValue("@d", 1);

            command.ExecuteNonQuery();
        }

        public void CreateLoan(int book, int user) {
            SqlCommand command = new SqlCommand("INSERT INTO Imprumuturi (IdCititor, IdCarte, DataImprumut, DataRestituire) VALUES (@a, @b, @c, @d);", connection);

            command.Parameters.AddWithValue("@a", user);
            command.Parameters.AddWithValue("@b", book);
            command.Parameters.AddWithValue("@c", DateTime.Now);
            command.Parameters.AddWithValue("@d", DBNull.Value);

            command.ExecuteNonQuery();
        }
    }
}
