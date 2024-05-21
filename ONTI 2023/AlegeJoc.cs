using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ONTI_2023
{
    public partial class AlegeJoc : Form
    {
        Database database;
        List<Result> results;
        string email;

        public AlegeJoc(string email)
        {
            InitializeComponent();

            this.email = email;

            database = new Database();

            label1.Text = "Bine ai venit " + database.GetUser(email).Split(' ')[0] + " (" + email + ")";

            results = database.GetResults(email);

            DateTime minDate = DateTime.MaxValue, maxDate = DateTime.MinValue;

            int i = 0;

            var bestScores = results
                .GroupBy(r => r.date.Date)
                .Select(g => new {
                    Date = g.Key,
                    BestScoreTesteazaMemoria = g.Where(r => r.type == 0).Any() ? g.Where(r => r.type == 0).Max(r => r.score) : (int?)null,
                    BestScorePopiceCuLitere = g.Where(r => r.type == 1).Any() ? g.Where(r => r.type == 1).Max(r => r.score) : (int?)null
                });

            foreach (var result in bestScores) {
                if (result.Date < minDate)
                    minDate = result.Date;
                if (result.Date > maxDate)
                    maxDate = result.Date;

                if (result.BestScoreTesteazaMemoria > 0)
                    chart1.Series["Testeaza memoria"].Points.AddXY(result.Date.ToOADate(), result.BestScoreTesteazaMemoria);

                if (result.BestScorePopiceCuLitere > 0)
                    chart1.Series["Popice cu litere"].Points.AddXY(result.Date.ToOADate(), result.BestScorePopiceCuLitere);
            }

            chart1.ChartAreas[0].AxisX.Minimum = minDate.ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = maxDate.ToOADate();
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
            chart1.ChartAreas[0].AxisX.Interval = 1;

            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 100;
            chart1.ChartAreas[0].AxisY.Interval = 20;
        }

        private void button1_Click(object sender, EventArgs e) {
            JocMemorie form = new JocMemorie(email);
            form.Show();
            this.Close();
        }
    }
}
