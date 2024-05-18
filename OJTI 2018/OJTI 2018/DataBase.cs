using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI_2018
{
    class DataBase
    {
        SqlConnection connection;

        public DataBase()
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\eLearning1918.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
        }

        public void Pupulate()
        {
            using (StreamReader file = new StreamReader(Application.StartupPath + @"\csarp\date.txt"))
            {
                var filds = file.ToString().Split(':');

                foreach (var fild in filds)
                {
                    if (fild == "Utilizatori")
                        continue;
                    else
                    {
                        var lines = fild.ToString().Split('\n');
                        foreach (var line in lines)
                        {
                            if (line.Contains(';')) {
                                var fields = line.Split(';');
                                foreach (var field in fields)
                                {

                                }
                            }
                            else
                                continue;
                        }
                    }
                }
            }
        }

        public int Login(string email, string password)
        {
            SqlCommand command = new SqlCommand("SELECT IdUtilizator FROM Utilizatori WHERE EmailUtilizator='" + email + "' AND ParolaUtilizator='" + password + "';", connection);

            var value = command.ExecuteScalar();

            if (value != null)
                return Convert.ToInt32(value);
            else return -1;
        }
    }
}
