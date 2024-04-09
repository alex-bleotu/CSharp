using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace OJTI_2023
{
    public class DataBase
    {
        SqlConnection connection;

        public DataBase() {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\alexb\OneDrive\Documents\JocEducativ.mdf;Integrated Security=True;Connect Timeout=30;");
        }

        public bool Login(string email, string password) {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Count(*) FROM Utilizatori WHERE EmailUtilizator='" + email + "' AND Parola='" + password + "'", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);
            if (table.Rows[0][0].ToString() == "1")
                return true;
            else return false;
        }

        public User GetUserByEmail(string email)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT EmailUtilizator, NumeUtilizator, Parola FROM Utilizatori WHERE EmailUtilizator = '" + email + "';", connection); ;
            DataTable table = new DataTable();
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                User user = new User
                {
                    email = row["EmailUtilizator"].ToString().Trim(),
                    username = row["NumeUtilizator"].ToString().Trim(),
                    password = row["Parola"].ToString().Trim()
                };
                return user;

            }
            else return null;
        }

        public List<Result> GetGuessResults()
        {
            List<Result> list = new List<Result>();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT idRezultat, TipJoc, EmailUtilizator, PunctajJoc FROM Rezultate WHERE TipJoc=0;", connection);
            DataTable table = new DataTable();
            adapter.Fill(table);

            if (table.Rows.Count > 0)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    Result result = new Result
                    {
                        idResult = Int32.Parse(row["idRezultat"].ToString().Trim()),
                        gameType = Int32.Parse(row["TipJoc"].ToString().Trim()),
                        userEmail = row["EmailUtilizator"].ToString().Trim(),
                        gameScore = Int32.Parse(row["PunctajJoc"].ToString().Trim())
                    };
                    list.Add(result);
                }

            return list;
        }

        public List<Result> GetSnakeResults()
        {
            List<Result> list = new List<Result>();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT idRezultat, TipJoc, EmailUtilizator, PunctajJoc FROM Rezultate WHERE TipJoc=1;", connection);
            DataTable table = new DataTable();
            adapter.Fill(table);

            if (table.Rows.Count > 0)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    Result result = new Result
                    {
                        idResult = Int32.Parse(row["idRezultat"].ToString().Trim()),
                        gameType = Int32.Parse(row["TipJoc"].ToString().Trim()),
                        userEmail = row["EmailUtilizator"].ToString().Trim(),
                        gameScore = Int32.Parse(row["PunctajJoc"].ToString().Trim())
                    };
                    list.Add(result);
                }

            return list;
        }

        public void ResetTable(string tableName)
        {
            SqlCommand command = new SqlCommand($"DELETE FROM {tableName};", connection);
            command.ExecuteNonQuery();
        }

        public void ResetTableWithIdent(string tableName)
        {
            SqlCommand command = new SqlCommand($"DELETE FROM {tableName}; DBCC CHECKIDENT ('{tableName}', RESEED, 0);", connection);
            command.ExecuteNonQuery();
        }

        public void Populate()
        {
            connection.Open();

            ResetTableWithIdent("Rezultate");
            ResetTable("Utilizatori");
            ResetTableWithIdent("Itemi");

            using (StreamReader file = new StreamReader(@"C:\Users\alexb\Projects\CSharp\OJTI 2023\OJTI 2023\Resources\Utilizatori.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var fields = line.Split(';');

                    SqlCommand command = new SqlCommand("INSERT INTO Utilizatori (EmailUtilizator, NumeUtilizator, Parola) VALUES (@email, @username, @password);", connection);

                    command.Parameters.AddWithValue("@email", fields[0]);
                    command.Parameters.AddWithValue("@username", fields[1]);
                    command.Parameters.AddWithValue("@password", fields[2]);

                    command.ExecuteNonQuery();
                }
            }

            using (StreamReader file = new StreamReader(@"C:\Users\alexb\Projects\CSharp\OJTI 2023\OJTI 2023\Resources\Rezultate.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var fields = line.Split(';');

                    SqlCommand command = new SqlCommand("INSERT INTO Rezultate (TipJoc, EmailUtilizator, PunctajJoc) VALUES (@TipJoc, @EmailUtilizator, @PunctajJoc);", connection);

                    command.Parameters.AddWithValue("@TipJoc", Int32.Parse(fields[1]));
                    command.Parameters.AddWithValue("@EmailUtilizator", fields[2]);
                    command.Parameters.AddWithValue("@PunctajJoc", fields[3]);

                    command.ExecuteNonQuery();
                }
            }

            using (StreamReader file = new StreamReader(@"C:\Users\alexb\Projects\CSharp\OJTI 2023\OJTI 2023\Resources\Itemi.txt"))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var fields = line.Split(';');

                    SqlCommand command = new SqlCommand("INSERT INTO Itemi (EnuntItem, Raspuns1, Raspuns2, Raspuns3, RaspunsCorect, PunctajItem) VALUES (@enumItem, @ras1, @ras2, @ras3, @rasC, @pct);", connection);

                    command.Parameters.AddWithValue("@enumItem", fields[1]);
                    command.Parameters.AddWithValue("@ras1", fields[2]);
                    command.Parameters.AddWithValue("@ras2", fields[3]);
                    command.Parameters.AddWithValue("@ras3", fields[4]);
                    command.Parameters.AddWithValue("@rasC", Int32.Parse(fields[5]));
                    command.Parameters.AddWithValue("@pct", Int32.Parse(fields[6]));

                    command.ExecuteNonQuery();
                }
            }

            connection.Close();
        }
    }
}
