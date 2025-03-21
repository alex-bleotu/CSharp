using ONTI_2024.CosmosDBDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2024 {
    public class User {
        public string email { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public DateTime date { get; set; }

        public User(string email, string name, string lastName, string password, DateTime date) {
            this.email = email;
            this.name = name;
            this.lastName = lastName;
            this.password = password;
            this.date = date;
        }
    }
    public class Record {
        public string email { get; set; }
        public int moon { get; set; }
        public int zodiac { get; set; }
        public DateTime date { get; set; }

        public Record(string email, int moon, int zodiac, DateTime date) {
            this.email = email;
            this.moon = moon;
            this.zodiac = zodiac;
            this.date = date;
        }
    }

    internal class Database {
        private CosmosDBDataSet db = new CosmosDBDataSet();
        private UtilizatoriTableAdapter usersAdapter = new UtilizatoriTableAdapter();
        private InregistrariTableAdapter recordingAdapter = new InregistrariTableAdapter();

        private SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"" + Application.StartupPath + "\\CosmosDB.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False");

        public void Refresh() {
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM Utilizatori; DELETE FROM Inregistrari; DBCC CHECKIDENT(Inregistrari, RESEED, 0);", connection);
            command.ExecuteNonQuery();
        }

        public void Init() {
            using (StreamReader reader = new StreamReader(Application.StartupPath + @"\Resurse\Utilizatori.txt")) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    var fields = line.Split(';');

                    DateTime date = DateTime.ParseExact(fields[4], "MM.dd.yyyy", CultureInfo.InvariantCulture);
                    usersAdapter.Insert(fields[0], fields[1], fields[2], fields[3], date);
                }
            }

            using (StreamReader reader = new StreamReader(Application.StartupPath + @"\Resurse\Inregistrari.txt")) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    var fields = line.Split(';');

                    DateTime date = DateTime.ParseExact(fields[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    recordingAdapter.Insert(fields[0], date, Int32.Parse(fields[1]), Int32.Parse(fields[3]));
                }
            }
        }
        public bool CheckUserExists(string email) {
            return usersAdapter.GetUserByEmail(email.Trim()).Rows.Count > 0;
        }

        public string Encrypt(string password) {
            StringBuilder encrypt = new StringBuilder();

            for (int i = 0; i < password.Length; i++) {
                if (Char.IsDigit(password[i]))
                    encrypt.Append((char)(password[i] % 10 + '0'));
                else if (Char.IsLower(password[i]))
                    encrypt.Append((char)(password[i] % 26 + 'a'));
                else if (Char.IsUpper(password[i]))
                    encrypt.Append((char)(password[i] % 26 + 'A'));
            }

            return encrypt.ToString();
        }

        public User Login(string email, string password) {
            email = email.Trim();
            password = password.Trim();


            var response = usersAdapter.Login(email, Encrypt(password));
            if (response.Rows.Count > 0) {
                var row = response[0];
                return new User(row.Email.Trim(), row.Prenume.Trim(), row.Nume.Trim(), row.Parola.Trim(), row.DataNastere);
            }
            return null;
        }

        public bool Register(User user) {
            usersAdapter.Insert(user.email, user.lastName, user.name, user.password, user.date);

            if (CheckUserExists(user.email))
                return true;
            return false;
        }

        public List<Record> GetUserRecords(User user) {
            List<Record> records = new List<Record>();

            var result = recordingAdapter.GetRecordByUser(user.email);

            for (int i = 0; i < result.Rows.Count; i++) {
                records.Add(new Record(result[i].Email, result[i].CodFazaLuna, result[i].CodZodia, result[i].Data));
            }

            return records;
        }
    }
}
