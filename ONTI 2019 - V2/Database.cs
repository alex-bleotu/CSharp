using ONTI_2019___V2.BibliotecaDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2019___V2 {
    internal class Database {
        public class User {
            public int IdCititor { get; set; }
            public string NumePrenume { get; set; }
            public string Email { get; set; }
        }

        public class Imp {
            public int IdImprumut { get; set; }
            public int IdCarte { get; set; }
            public string Titlu { get; set; }
            public string Autor { get; set; }
            public DateTime DataImprumut { get; set; }
            public DateTime DataExpirareImprumut { get; set; }
        }

        public class Book {
            public string title { get; set; }
            public string autor { get; set; }
            public int nrPag { get; set; }
            public int id { get; set; }
        }

        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\Biblioteca.mdf;Integrated Security=True;Connect Timeout=30");

        BibliotecaDataSet db = new BibliotecaDataSet();
        UtilizatoriTableAdapter usersAdapter = new UtilizatoriTableAdapter();
        CartiTableAdapter cartiAdapter = new CartiTableAdapter();
        ImprumuturiTableAdapter imprumuturiAdapter = new ImprumuturiTableAdapter();
        RezervariTableAdapter rezervariAdapter = new RezervariTableAdapter();

        public Database() {
            connection.Open();
        }

        public string CriptareParola(string password) {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < password.Length; i++) {
                if (Char.IsLower(password[i])) {
                    if (password[i] == 'z')
                        str.Append('a');
                    else str.Append((char)(password[i] + 1));
                }
                else if (Char.IsUpper(password[i])) {
                    if (password[i] == 'A')
                        str.Append('Z');
                    else str.Append((char)(password[i] - 1));
                }
                else if (Char.IsDigit(password[i]))
                    str.Append((char)('9' - password[i] + '0'));
                else str.Append(password[i]);
            }

            return str.ToString();
        }

        public void Load() {
            SqlCommand command = new SqlCommand(
                "DELETE FROM Utilizatori; " +
                "DELETE FROM Carti; " +
                "DELETE FROM Rezervari; " +
                "DELETE FROM Imprumuturi; " +
                "DBCC CHECKIDENT ('Utilizatori', RESEED, 1); " +
                "DBCC CHECKIDENT ('Carti', RESEED, 1); " +
                "DBCC CHECKIDENT ('Rezervari', RESEED, 1); " +
                "DBCC CHECKIDENT ('Imprumuturi', RESEED, 1);", connection);
            command.ExecuteNonQuery();

            using (StreamReader reader = new StreamReader(Application.StartupPath + @"\Resurse\utilizatori.txt")) {
                string line;

                while ((line = reader.ReadLine()) != null) {
                    var fields = line.Split(';');

                    usersAdapter.Insert(Int32.Parse(fields[0]), fields[1], fields[2], fields[3] != "" ? CriptareParola(fields[3]) : null);
                }
            }

            using (StreamReader reader = new StreamReader(Application.StartupPath + @"\Resurse\carti.txt")) {
                string line;

                while ((line = reader.ReadLine()) != null) {
                    var fields = line.Split(';');

                    cartiAdapter.Insert(fields[0], fields[1], Int32.Parse(fields[2]));
                }
            }

            using (StreamReader reader = new StreamReader(Application.StartupPath + @"\Resurse\rezervari.txt")) {
                string line;

                while ((line = reader.ReadLine()) != null) {
                    var fields = line.Split(';');

                    rezervariAdapter.Insert(Int32.Parse(fields[0]), Int32.Parse(fields[1]), DateTime.ParseExact(fields[2], "MM/dd/yyyy hh/mm/ss tt", CultureInfo.InvariantCulture), Int32.Parse(fields[3]));
                }
            }

            using (StreamReader reader = new StreamReader(Application.StartupPath + @"\Resurse\imprumuturi.txt")) {
                string line;

                while ((line = reader.ReadLine()) != null) {
                    var fields = line.Split(';');

                    imprumuturiAdapter.Insert(Int32.Parse(fields[0]), Int32.Parse(fields[1]), DateTime.ParseExact(fields[2], "MM/dd/yyyy hh/mm/ss tt", CultureInfo.InvariantCulture), fields[3] == "NULL" ? (DateTime?)null : DateTime.ParseExact(fields[3], "MM/dd/yyyy hh/mm/ss tt", CultureInfo.InvariantCulture));
                }
            }
        }

        public int Login(string email, string password) {
            var response = usersAdapter.Login(email, CriptareParola(password));
            if (response != null)
                return response.GetValueOrDefault(-1);
            return -1;
        }

        public string GetData(int id) {
            var response = usersAdapter.GetName(id);
            return response.ToString();
        }

        public bool CheckIfExists(string email) {
            return usersAdapter.CheckIfExists(email) != null;
        }

        public void CreateUser(string name, string email, string password, int type) {
            usersAdapter.Insert(type, name, email, CriptareParola(password));
        }

        public List<User> GetUsers(string name) {
            name = name.ToLower().Trim();

            List<User> users = new List<User>();

            var response = usersAdapter.GetReaders();

            foreach (var r in response) {
                var u = new User();
                u.Email = r.Email;
                u.NumePrenume = r.NumePrenume;
                u.IdCititor = r.IdUtilizator;

                if (u.NumePrenume.ToLower().Contains(name))
                    users.Add(u);
            }

            return users;
        }

        public List<Imp> GetImp(int id) {
            var response = imprumuturiAdapter.GetImp(id);

            List<Imp> imps = new List<Imp>();

            foreach (var r in response) {
                var i = new Imp();
                i.IdImprumut = r.IdImprumut;
                i.IdCarte = r.IdCarte;

                i.Titlu = cartiAdapter.GetTitle(r.IdCarte).ToString();
                i.Autor = cartiAdapter.GetAutor(r.IdCarte);

                i.DataImprumut = r.DataImprumut;
                i.DataExpirareImprumut = r.DataImprumut.AddDays(7);

                imps.Add(i);
            }

            return imps;
        }

        public Book GetBook(int id) {
            //var response = cartiAdapter.GetBook(id);

            return null;
        }
    }
}
