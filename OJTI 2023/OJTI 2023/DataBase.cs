using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace OJTI_2023 {
    class DataBase
    {
        SqlConnection connection;

        DataBase() {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AlexDavid\OneDrive\Documents\JocEducativ.mdf;Integrated Security=True;Connect Timeout=30;");
        }

        public bool Login(string email, string password) {
            SqlDataAdapter adapter = new SqlDataAdapter("Select Count(*) From Utilizatori where EmailUtilizator='" + email + "' and Parola='" + password + "'", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);
            if (table.Rows[0][0].ToString() == "1")
                return true;
            else return false;
        }
    }
}
