using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI_2023
{
    public partial class AlegeJoc : Form
    {
        User user;
        DataBase dataBase;

        public AlegeJoc(string email)
        {
            InitializeComponent();
            dataBase = new DataBase();
            user = dataBase.GetUserByEmail(email);
            if (user == null)
                this.Close();
            else
            {
                userLabel.Text = user.username + "! (" + user.email + ")";
            }

            List<Result> guessResults = dataBase.GetGuessResults();
            guessResults.Sort((p1, p2) => p1.gameScore.CompareTo(p2.gameScore));
            guessResults.Reverse();
            List<Result> snakeResults = dataBase.GetSnakeResults();
            snakeResults.Sort((p1, p2) => p1.gameScore.CompareTo(p2.gameScore));
            snakeResults.Reverse();

            DataTable guessTable = new DataTable();
            guessTable.Columns.Add("NumeUtilizator", typeof(string));
            guessTable.Columns.Add("EmailUtilizator", typeof(string));
            guessTable.Columns.Add("PunctajJoc", typeof(int));
            if (guessResults.Count > 0)
                for (int i = 0; i < (3 < guessResults.Count ? 3 : guessResults.Count); i++)
                    guessTable.Rows.Add(dataBase.GetUserByEmail(guessResults[i].userEmail).username, guessResults[i].userEmail, guessResults[i].gameScore);
            guessDataGridView.DataSource = guessTable;

            DataTable snakeTable = new DataTable();
            snakeTable.Columns.Add("NumeUtilizator", typeof(string));
            snakeTable.Columns.Add("EmailUtilizator", typeof(string));
            snakeTable.Columns.Add("PunctajJoc", typeof(int));
            if (snakeResults.Count > 0)
                for (int i = 0; i < (3 < snakeResults.Count ? 3 : snakeResults.Count); i++)
                    snakeTable.Rows.Add(dataBase.GetUserByEmail(snakeResults[i].userEmail).username, snakeResults[i].userEmail, snakeResults[i].gameScore);
            snakeDataGridView.DataSource = snakeTable;
        }

        private void guessButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            GhicesteCuvant ghiceste = new GhicesteCuvant();
            ghiceste.Show();
        }

        private void snakeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sarpe sarpe = new Sarpe();
            sarpe.Show();
        }
    }
}
