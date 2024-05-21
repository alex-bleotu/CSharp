using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2018 {
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

    public class Leason {
        public int id;
        public User user;
        public string title;
        public string leason;
        public DateTime creationDate;

        public Leason(int id, User user, string title, string leason, DateTime creation) {
            this.id = id;
            this.user = user;
            this.title = title;
            this.leason = leason;
            creationDate = creation;
        }
    }

    internal class DataBase {
        SqlConnection connection;

        public DataBase() {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\CentenarDB.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();
        }

        public void DeleteTable(string tableName) {
            SqlCommand command = new SqlCommand("DELETE FROM " + tableName + " DBCC CHECKIDENT(" + tableName + ", RESEED, 0);", connection);

            command.ExecuteNonQuery();
        }

        public void Populate() {
            DeleteTable("Lectii");
            DeleteTable("Utilizatori");

            using (StreamReader file = new StreamReader(Application.StartupPath + @"\Resurse\utilizatori.txt")) {
                string line;

                while ((line = file.ReadLine()) != null) {
                    var field = line.Split('*');

                    SqlCommand command = new SqlCommand("INSERT INTO Utilizatori (Nume, Email, Parola) VALUES (@a, @b, @c)", connection);

                    command.Parameters.AddWithValue("@a", field[0]);
                    command.Parameters.AddWithValue("@b", field[2]);
                    command.Parameters.AddWithValue("@c", field[1]);

                    command.ExecuteNonQuery();
                }
            }

            using (StreamReader file = new StreamReader(Application.StartupPath + @"\Resurse\lectii.txt")) {
                string line;

                while ((line = file.ReadLine()) != null) {
                    var field = line.Split('*');

                    SqlCommand command = new SqlCommand("INSERT INTO Lectii (IdUtilizator, TitluLectie, Regiune, DataCreare) VALUES (@a, @b, @c, @d)", connection);

                    command.Parameters.AddWithValue("@a", Int32.Parse(field[0]));
                    command.Parameters.AddWithValue("@c", field[1]);
                    command.Parameters.AddWithValue("@b", field[2]);
                    command.Parameters.AddWithValue("@d", DateTime.ParseExact(field[3], "M/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));

                    command.ExecuteNonQuery();
                }
            }
        }

        public int Login(string email, string password) {
            SqlCommand command = new SqlCommand("SELECT IdUtilizator FROM Utilizatori WHERE Email='" + email + "' AND Parola='" + password + "';", connection);

            var id = command.ExecuteScalar();

            if (id == null) return -1;
            return Int32.Parse(id.ToString());
        }

        public User GetUser(int id) {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Nume, Email FROM Utilizatori WHERE IdUtilizator=" + id, connection);
            DataTable table = new DataTable();

            adapter.Fill(table);

            return new User(id, table.Rows[0][0].ToString(), table.Rows[0][1].ToString());
        }

        public List<Leason> GetLeasons() {
            List<Leason> leasons = new List<Leason>();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT IdLectie, IdUtilizator, TitluLectie, Regiune, DataCreare FROM Lectii", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);

            foreach (DataRow row in table.Rows) {
                leasons.Add(new Leason(Int32.Parse(row[0].ToString()), GetUser(Int32.Parse(row[1].ToString())), row[2].ToString(), row[3].ToString(), DateTime.Parse(row[4].ToString())));
            }

            return leasons;
        }
    }
}
