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

namespace ONTI_2022___V2 {
    public class User {
        public int id;
        public string name;
        public string password;
        public string email;
        public DateTime last;
    }

    public class Map {
        public int id;
        public string name;
        public string file;
    }

    public class Measurement {
        public int id;
        public int mapId;
        public int posX;
        public int posY;
        public double value;
        public DateTime date;
    }

    internal class Database {
        SqlConnection connection;

        public Database() {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\Populare.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
        }

        public void DropTable(string tableName) {
            SqlCommand command = new SqlCommand("DELETE " + tableName + ";", connection);
            command.ExecuteNonQuery();
        }

        public void Populate() {
            DropTable("Masurare");
            DropTable("Harti");

            try {
                using (StreamReader file = new StreamReader(Application.StartupPath + @"\Resources\harti.txt")) {
                    string line;

                    while ((line = file.ReadLine()) != null) {
                        var fields = line.Split('#');

                        SqlCommand command = new SqlCommand("INSERT INTO Harti (NumeHarta, FisierHarta) VALUES (@a, @b);", connection);

                        command.Parameters.AddWithValue("@a", fields[0]);
                        command.Parameters.AddWithValue("@b", fields[1]);

                        command.ExecuteNonQuery();
                    }
                }
            } catch { }

            try {
                using (StreamReader file = new StreamReader(Application.StartupPath + @"\Resources\masurari.txt")) {
                    string line;

                    while ((line = file.ReadLine()) != null) {
                        var fields = line.Split('#');

                        SqlCommand command = new SqlCommand("INSERT INTO Masurare (IdHarta, PozitieX, PozitieY, ValoareMasurare, DataMasurare) VALUES (@a, @b, @c, @d, @e);", connection);

                        SqlCommand command2 = new SqlCommand("SELECT IdHarta FROM Harti WHERE NumeHarta='" + fields[0].ToString() + "';", connection);
                        var value = command2.ExecuteScalar();

                        command.Parameters.AddWithValue("@a", Int32.Parse(value.ToString()));
                        command.Parameters.AddWithValue("@b", Int32.Parse(fields[1]));
                        command.Parameters.AddWithValue("@c", Int32.Parse(fields[2]));
                        command.Parameters.AddWithValue("@d", Double.Parse(fields[3].ToString()));
                        command.Parameters.AddWithValue("@e", DateTime.ParseExact(fields[4].ToString(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        public void setDate(int id) {
            SqlCommand command = new SqlCommand("UPDATE Utilizatori SET UltimaUtilizare='" + DateTime.Now + "' WHERE IdUtilizator=" + id + ";", connection);
            command.ExecuteNonQuery();
        }

        public int Login(string user, string password) {
            SqlCommand command = new SqlCommand("SELECT IdUtilizator FROM Utilizatori WHERE NumeUtilizator='" + user + "' AND Parola='" + password + "';", connection);

            var result = command.ExecuteScalar();
            if (result != null) {
                int id = Int32.Parse(result.ToString());
                setDate(id);
                return id;
            } else return -1;
        }

        public bool CheckUser(string user) {
            SqlCommand command = new SqlCommand("SELECT IdUtilizator FROM Utilizatori WHERE NumeUtilizator='" + user + "';", connection);

            var result = command.ExecuteScalar();
            if (result != null)
                return true;
            else return false;
        }

        public string GetUsername(int id) {
            SqlCommand command = new SqlCommand("SELECT NumeUtilizator FROM Utilizatori WHERE IdUtilizator='" + id + "';", connection);

            return command.ExecuteScalar().ToString().Trim();
        }

        public List<string> GetMapNames() {
            List<string> list = new List<string>();

            SqlDataAdapter adapter = new SqlDataAdapter("Select NumeHarta FROM Harti", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);

            foreach (DataRow row in table.Rows) {
                list.Add(row[0].ToString().Trim());
            }

            return list;
        }

        public void Register(string user, string password, string email) {
            SqlCommand command = new SqlCommand("INSERT INTO Utilizatori (NumeUtilizator, Parola, EmailUtilizator) VALUES ('" + user + "', '" + password + "', '" + email + "');", connection);

            command.ExecuteNonQuery();
        }

        public string GetImage(string name) {
            SqlCommand command = new SqlCommand("SELECT FisierHarta FROM Harti WHERE NumeHarta='" + name + "';", connection);

            return command.ExecuteScalar().ToString().Trim();
        }

        public int GetMapId(string name) {
            SqlCommand command = new SqlCommand("SELECT IdHarta FROM Harti WHERE NumeHarta='" + name + "';", connection);

            var result = command.ExecuteScalar();
            if (result != null)
                return Int32.Parse(result.ToString());
            return -1;

        }

        public List<Measurement> GetMeasurements(int id) {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT IdMasurare, IdHarta, PozitieX, PozitieY, ValoareMasurare, DataMasurare FROM Masurare WHERE IdHarta='" + id + "';", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);

            List<Measurement> list = new List<Measurement>();

            foreach (DataRow row in table.Rows) {
                Measurement measurement = new Measurement();
                measurement.id = Int32.Parse(row[0].ToString());
                measurement.mapId = Int32.Parse(row[1].ToString());
                measurement.posX = Int32.Parse(row[2].ToString());
                measurement.posY = Int32.Parse(row[3].ToString());
                measurement.value = Double.Parse(row[4].ToString());
                measurement.date = DateTime.Parse(row[5].ToString());

                list.Add(measurement);
            }

            return list;
        }

        public void AddMeasurement(int mapId, int x, int y, double value, DateTime date) {
            SqlCommand command = new SqlCommand("INSERT INTO Masurare (IdHarta, PozitieX, PozitieY, ValoareMasurare, DataMasurare) VALUES (@a, @b, @c, @d, @e);", connection);

            DateTime dateNow = DateTime.Now;

            command.Parameters.AddWithValue("@a", mapId.ToString());
            command.Parameters.AddWithValue("@b", x.ToString());
            command.Parameters.AddWithValue("@c", y.ToString());
            command.Parameters.AddWithValue("@d", value.ToString());
            command.Parameters.AddWithValue("@e", (new DateTime(date.Year, date.Month, date.Day, dateNow.Hour, dateNow.Minute, dateNow.Second)).ToString());

            command.ExecuteNonQuery();
        }
    }
}
