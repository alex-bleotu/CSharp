using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ONTI_2023___V2.JocuriDataSetTableAdapters;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ONTI_2023___V2 {
    public class Score {
        public DateTime date { get; set; }
        public int score { get; set; }

        public Score(int score, DateTime date) {
            this.date = date;
            this.score = score;
        }
    }

    internal class Database {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\Jocuri.mdf;Integrated Security=True;Connect Timeout=30");

        UtilizatoriTableAdapter users = new UtilizatoriTableAdapter();
        RezultateTableAdapter scores = new RezultateTableAdapter();

        public Database() {
            connection.Open();
        }

        public void Load() {
            SqlCommand command = new SqlCommand(
                "DELETE Rezultate;" +
                "DELETE Utilizatori; " +
                "DBCC CHECKIDENT(Rezultate, RESEED, 1);", connection);
            command.ExecuteNonQuery();

            using (StreamReader reader = new StreamReader(Application.StartupPath + @"\Resurse\Utilizatori.txt")) {
                string line;

                while ((line = reader.ReadLine()) != null) {
                    var fields = line.Split(';');

                    users.Insert(fields[0], fields[1], fields[2]);
                }
            }

            using (StreamReader reader = new StreamReader(Application.StartupPath + @"\Resurse\Rezultate.txt")) {
                string line;

                while ((line = reader.ReadLine()) != null) {
                    var fields = line.Split(';');

                    scores.Insert(Int32.Parse(fields[0]), fields[1], Int32.Parse(fields[2]), DateTime.ParseExact(fields[3], "dd.MM.yyyy", CultureInfo.InvariantCulture));
                }
            }
        }

        public string Login(string email, string password) {
            var response = users.Login(email, password);

            if (response.Rows.Count > 0)
                return response[0].NumeUtilizator;
            return null;
        }

        public void Register(string email, string password, string name) {
            users.Insert(email, name, password);
        }

        public bool CheckIfEmailExists(string email) {
            var response = users.CheckIfEmailExists(email);

            if (response != null)
                return true;
            return false;
        }

        public List<Score> GetFirstScores(string email) {
            var response = scores.GetFirstScores(email);

            List<Score> scoresList = new List<Score>();
            foreach (var r in response)
                scoresList.Add(new Score(r.PunctajJoc, r.Data));

            return scoresList;
        }

        public List<Score> GetSecondsScores(string email) {
            var response = scores.GetSecondsScores(email);

            List<Score> scoresList = new List<Score>();
            foreach (var r in response)
                scoresList.Add(new Score(r.PunctajJoc, r.Data));

            return scoresList;
        }

        public void AddScore(string email, int score, int game) {
            scores.Insert(game, email, score, DateTime.Now);
        }

        public List<Tuple<string, int>> GetAllScores() {
            var response = scores.GetData();

            List<Tuple<string, int>> scoresList = new List<Tuple<string, int>>();
            foreach (var r in response)
                scoresList.Add(new Tuple<string, int>(r.EmailUtilizator, r.PunctajJoc));

            return scoresList;
        }
    }
}
