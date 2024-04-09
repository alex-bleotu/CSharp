using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI_2019
{
    class DataBase
    {
        SqlConnection connection;

        public class Book
        {
            public string author { get; set; }
            public string title { get; set; }
            public string genre { get; set; }
        }

        public DataBase()
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\FreeBook.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
        }

        public void ClearTable(string table)
        {
            SqlCommand command = new SqlCommand("DELETE " + table + ";", connection);
            command.ExecuteNonQuery();
        }

        public void Populate()
        {
            /*ClearTable("utilizatori");
            ClearTable("carti");
            ClearTable("imprumut");*/

            using (StreamReader file = new StreamReader(Application.StartupPath + @"\csarp\utilizatori.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var fields = line.Split('*');

                    SqlCommand command = new SqlCommand("INSERT INTO utilizatori (email, parola, nume, prenume) VALUES (@a, @b, @c, @d);", connection);

                    command.Parameters.Add("@a", fields[0]);
                    command.Parameters.Add("@b", fields[1]);
                    command.Parameters.Add("@c", fields[2]);
                    command.Parameters.Add("@d", fields[3]);

                    command.ExecuteNonQuery();
                }
            }

            using (StreamReader file = new StreamReader(Application.StartupPath + @"\csarp\carti.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var fields = line.Split('*');

                    SqlCommand command = new SqlCommand("INSERT INTO carti (titlu, autor, gen) VALUES (@a, @b, @c);", connection);

                    command.Parameters.Add("@a", fields[0]);
                    command.Parameters.Add("@b", fields[1]);
                    command.Parameters.Add("@c", fields[2]);

                    command.ExecuteNonQuery();
                }
            }

            using (StreamReader file = new StreamReader(Application.StartupPath + @"\csarp\imprumuturi.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var fields = line.Split('*');

                    SqlCommand commandId = new SqlCommand("SELECT id_carte FROM carti WHERE titlu='" + fields[0] + "';", connection);

                    int id = Convert.ToInt32(commandId.ExecuteScalar());

                    SqlCommand command = new SqlCommand("INSERT INTO imprumut (id_carte, email, data_imprumut) VALUES (@a, @b, @c);", connection);

                    command.Parameters.Add("@a", id);
                    command.Parameters.Add("@b", fields[1]);
                    command.Parameters.Add("@c", DateTime.Parse(fields[2]));

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool Login(string email, string password)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Count(*) FROM utilizatori WHERE email='" + email + "' AND parola='" + password + "';", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);

            if (table.Rows[0][0].ToString() == "1")
                return true;
            return false;
        }

        public void Register(string email, string password, string name, string prename)
        {
            SqlCommand command = new SqlCommand("INSERT INTO utilizatori (email, parola, nume, prenume) VALUES (@a, @b, @c, @d);", connection);

            command.Parameters.Add("@a", email);
            command.Parameters.Add("@b", password);
            command.Parameters.Add("@c", name);
            command.Parameters.Add("@d", prename);

            command.ExecuteNonQuery();
        }

        public bool EmailExists(string email)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Count(*) FROM utilizatori WHERE email='" + email + "';", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);
            if (table.Rows[0][0].ToString() == "1")
                return true;
            return false;
        }

        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            List<int> ids = new List<int>();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT id, data_imprumut FROM imprumut", connection);
            DataTable table = new DataTable();
            adapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                DateTime.Parse(row["data_imprumut"].ToString());
                if ()
                ids.Add(Convert.ToInt32(row["id"]));
            }

            foreach (int id in ids)
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT")
            }
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT")

            return books;
        }
    }
}
