using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Data;

namespace OJTI_2022
{
    class DataBase
    {
        SqlConnection connection;

        public class Point
        {
            public int x {get; set;}
            public int y { get; set; }
            public int val { get; set; }
            public DateTime date { get; set; }
        }

        public DataBase()
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\Poluare.mdf; Integrated Security=True;Connect Timeout=30");

            connection.Open();
        }

        public void Populate()
        {
            DeleteTable("Masurare");
            DeleteTable("Harti");

            using (StreamReader file = new StreamReader(Application.StartupPath + @"\harti.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var fields = line.Split('#');

                    SqlCommand command = new SqlCommand("INSERT INTO Harti (NumeHarta, FisierHarta) VALUES (@name, @file);", connection);

                    command.Parameters.Add("@name", fields[0]);
                    command.Parameters.Add("@file", fields[1]);

                    command.ExecuteNonQuery();
                }
            }

            using (StreamReader file = new StreamReader(Application.StartupPath + @"\masurari.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var fields = line.Split('#');

                    SqlCommand command = new SqlCommand("INSERT INTO Masurare (IdHarta, PozitieX, PozitieY, ValoareMasurare, DataMasurare) VALUES (@id, @x, @y, @val, @date);", connection);

                    SqlCommand getID = new SqlCommand("SELECT IdHarta FROM Harti WHERE NumeHarta='" + fields[0] + "';", connection);

                    var id = getID.ExecuteScalar();

                    if (id == null) continue;

                    string format = "dd/MM/yyyy HH:mm";
                    command.Parameters.Add("@id", Convert.ToInt32(id));
                    command.Parameters.Add("@x", Int32.Parse(fields[1]));
                    command.Parameters.Add("@y", Int32.Parse(fields[2]));
                    command.Parameters.Add("@val", Int32.Parse(fields[3]));
                    command.Parameters.Add("@date", DateTime.ParseExact(fields[4], format, CultureInfo.InvariantCulture));

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTable(string table)
        {
            SqlCommand command = new SqlCommand("DELETE " + table + ";", connection);
            command.ExecuteNonQuery();
        }

        public bool Login(string name, string password)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT COUNT(*) FROM Utilizatori WHERE NumeUtilizator='" + name + "' AND Parola='" + password + "';", connection);
            DataTable table = new DataTable();
            adapter.Fill(table);

            if (table.Rows[0][0].ToString() == "1")
            {
                SqlCommand command = new SqlCommand("UPDATE Utilizatori SET UltimaUtilizare='" + DateTime.Now.ToString() + "' WHERE NumeUtilizator='" + name + "';", connection);

                command.ExecuteNonQuery();

                return true;
            }
            return false;
        }

        public bool CheckUserExist(string name)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT COUNT(*) FROM Utilizatori WHERE NumeUtilizator='" + name + "';", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);
            if (table.Rows[0][0].ToString() == "1")
                return true;
            return false;
        }

        public void Register(string name, string password, string email)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Utilizatori (NumeUtilizator, Parola, EmailUtilizator) VALUES (@name, @pass, @email);", connection);

            command.Parameters.Add("@name", name);
            command.Parameters.Add("@pass", password);
            command.Parameters.Add("@email", email);

            command.ExecuteNonQuery();
        }

        public List<string> GetMaps()
        {
            List<string> list = new List<string>();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT NumeHarta FROM Harti", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
                list.Add(row["NumeHarta"].ToString().Trim());

            return list;
        }

        public string GetFileMap(string name)
        {
            SqlCommand command = new SqlCommand("SELECT FisierHarta FROM Harti WHERE NumeHarta='" + name + "';", connection);
            return command.ExecuteScalar().ToString();
        }

        public List<Point> GetPoints(string name)
        {
            SqlCommand command = new SqlCommand("SELECT IdHarta FROM Harti WHERE NumeHarta='" + name + "';", connection);
            int id = Convert.ToInt32(command.ExecuteScalar());

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT PozitieX, PozitieY, ValoareMasurare, DataMasurare FROM Masurare WHERE IdHarta='" + id + "';", connection);
            DataTable table = new DataTable();

            List<Point> list = new List<Point>();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                Point p = new Point
                {
                    x = Int32.Parse(row["PozitieX"].ToString()),
                    y = Int32.Parse(row["PozitieY"].ToString()),
                    val = Int32.Parse(row["ValoareMasurare"].ToString()),
                    date = DateTime.Parse(row["DataMasurare"].ToString())
                };
                list.Add(p);
            }
            return list;
        }

        public void AddMeasurement(string name, int val, int x, int y)
        {
            SqlCommand command = new SqlCommand("SELECT IdHarta FROM Harti WHERE NumeHarta='" + name + "';", connection);
            int id = Convert.ToInt32(command.ExecuteScalar());

            SqlCommand command2 = new SqlCommand("INSERT INTO Masurare (IdHarta, PozitieX, PozitieY, ValoareMasurare, DataMasurare) VALUES (@id, @x, @y, @val, @date);", connection);

            command2.Parameters.Add("@id", id);
            command2.Parameters.Add("@x", x);
            command2.Parameters.Add("@y", y);
            command2.Parameters.Add("@val", val);
            command2.Parameters.Add("@date", DateTime.Now.ToString());

            command2.ExecuteNonQuery();
        }
    }
}
