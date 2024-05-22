using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2018 {
    public partial class GhicesteRegiunea : Form {
        List<Tuple<int, int>> coords;

        public GhicesteRegiunea() {
            InitializeComponent();

            coords = new List<Tuple<int, int>>();
        }

        Tuple<int, int> GetClosest(Tuple<int, int> point) {
            Tuple<int, int> closest = new Tuple<int, int>(0, 0);
            float minimum = float.MaxValue;

            foreach (var coord in coords) {
                float distance = (coord.Item1 - point.Item1) * (coord.Item1 - point.Item1) + (coord.Item2 - point.Item2) * (coord.Item2 - point.Item2);

                if (distance < minimum) {
                    minimum = distance;
                    closest = coord;
                }
            }

            return closest;
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            e.Graphics.FillRectangle(Brushes.LightBlue, new Rectangle(0, 0, panel1.Width, panel1.Height));

            using (StreamReader file = new StreamReader(Application.StartupPath + @"\Resurse\Harti\RomaniaMare.txt")) {
                string line;

                var last = file.ReadLine().Split('*');

                List<Point> points = new List<Point>();

                points.Add(new Point(Int32.Parse(last[0]), Int32.Parse(last[1])));

                while ((line = file.ReadLine()) != null) {
                    var coords = line.Split('*');

                    e.Graphics.DrawLine(new Pen(Color.Green, 5), float.Parse(last[0]), float.Parse(last[1]), float.Parse(coords[0]), float.Parse(coords[1]));

                    last = coords;

                    points.Add(new Point(Int32.Parse(last[0]), Int32.Parse(last[1])));
                }

                GraphicsPath path = new GraphicsPath();
                path.AddPolygon(points.ToArray());
                PathGradientBrush brush = new PathGradientBrush(path);

                brush.InterpolationColors = new ColorBlend {
                    Colors = new Color[] {
                        Color.Red, Color.Yellow, Color.Blue
                    },
                    Positions = new float[] { 0f, 0.25f, 1f }
                };

                e.Graphics.FillPolygon(brush, points.ToArray());
            }

            var filePaths = Directory.GetFiles(Application.StartupPath + @"\Resurse\Harti");

            foreach(var path in filePaths) {
                if (path.Contains("RomaniaMare"))
                    continue;

                using (StreamReader file = new StreamReader(path)) {
                    string line;

                    var cityCoords = file.ReadLine().Split('*');

                    var last = file.ReadLine().Split('*');

                    while ((line = file.ReadLine()) != null) {
                        var coords = line.Split('*');

                        e.Graphics.DrawLine(new Pen(Color.White, 2), float.Parse(last[0]), float.Parse(last[1]), float.Parse(coords[0]), float.Parse(coords[1]));

                        last = coords;
                    }

                    e.Graphics.DrawEllipse(new Pen(Color.Black, 2), new Rectangle(Int32.Parse(cityCoords[0]), Int32.Parse(cityCoords[1]), 10, 10));
                    e.Graphics.DrawString(cityCoords[2], DefaultFont, Brushes.Black, Int32.Parse(cityCoords[0]) + 20, Int32.Parse(cityCoords[1]));

                    coords.Add(new Tuple<int, int>(Int32.Parse(cityCoords[0]) + 5, Int32.Parse(cityCoords[1]) + 5));
                }
            }

            var closest = coords[0];

            while (coords.Count > 1) {
                coords.Remove(closest);

                Tuple<int, int> coord = closest;

                closest = GetClosest(coord);

                e.Graphics.DrawLine(new Pen(Color.Black, 2), coord.Item1, coord.Item2, closest.Item1, closest.Item2);
            }
        }
    }
}
