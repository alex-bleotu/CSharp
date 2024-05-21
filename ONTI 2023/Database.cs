using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.Globalization;
using System.Data;

namespace ONTI_2023
{
    public class Result {
        public int score;
        public int type;
        public DateTime date;

        public Result(int score, int type, DateTime date) {
            this.score = score;
            this.type = type;
            this.date = date;
        }
    }

    internal class Database
    {
        SqlConnection connection;

        public Database()
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\Jocuri.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
        }

        public void DropTable(string tableName)
        {
            SqlCommand command = new SqlCommand("DELETE " + tableName + ";", connection);

            command.ExecuteNonQuery();
        }

        public void Populate()
        {
            DropTable("Rezultate");
            //DropTable("Utilizatori");

            try
            {
                using (StreamReader file = new StreamReader(Application.StartupPath + @"\\Resources\\Utilizatori.txt"))
                {
                    string line;

                    while ((line = file.ReadLine()) != null)
                    {
                        var fields = line.Split(';');

                        SqlCommand command = new SqlCommand("INSERT INTO Utilizatori (EmailUtilizator, NumeUtilizator, Parola) VALUES (@a, @b, @c);", connection);

                        command.Parameters.AddWithValue("@a", fields[0]);
                        command.Parameters.AddWithValue("@b", fields[1]);
                        command.Parameters.AddWithValue("@c", fields[2]);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }

            using (StreamReader file = new StreamReader(Application.StartupPath + @"\\Resources\\Rezultate.txt"))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    var fields = line.Split(';');

                    SqlCommand command = new SqlCommand("INSERT INTO Rezultate (TipJoc, EmailUtilizator, PunctajJoc, Data) VALUES (@a, @b, @c, @d);", connection);

                    command.Parameters.AddWithValue("@a", Convert.ToInt32(fields[0]));
                    command.Parameters.AddWithValue("@b", fields[1]);
                    command.Parameters.AddWithValue("@c", Convert.ToInt32(fields[2]));
                    command.Parameters.AddWithValue("@d", DateTime.ParseExact(fields[3], "dd.MM.yyyy", CultureInfo.InvariantCulture));

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool Login(string email, string password) {
            SqlCommand command = new SqlCommand("SELECT EmailUtilizator FROM Utilizatori WHERE EmailUtilizator='" + email + "' AND Parola='" + password + "';", connection);

            return command.ExecuteScalar() != null;
        }

        public bool EmailExists(string email)
        {
            SqlCommand command = new SqlCommand("SELECT EmailUtilizator FROM Utilizatori WHERE EmailUtilizator='" + email + "';", connection);

            return command.ExecuteScalar() != null;
        }

        public void Register(string email, string name, string password)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Utilizatori (EmailUtilizator, NumeUtilizator, Parola) VALUES (@a, @b, @c);", connection);

            command.Parameters.AddWithValue("@a", email);
            command.Parameters.AddWithValue("@b", name);
            command.Parameters.AddWithValue("@c", password);

            command.ExecuteNonQuery();
        }

        public string GetUser(string email) {
            SqlCommand command = new SqlCommand("SELECT NumeUtilizator, Parola FROM Utilizatori WHERE EmailUtilizator='" + email + "';", connection);

            return command.ExecuteScalar().ToString();
        }

        public List<Result> GetResults(string email) {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT TipJoc, PunctajJoc, Data FROM Rezultate WHERE EmailUtilizator='" + email + "';", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);

            List<Result> results = new List<Result>();

            foreach (DataRow row in table.Rows) {
                Result result = new Result(Convert.ToInt32(row[1]), Convert.ToInt32(row[0]), DateTime.Parse(row[2].ToString()));

                results.Add(result);
            }

            return results;
        }
    }
}
