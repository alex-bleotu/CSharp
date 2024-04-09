using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsTest
{
    public partial class Form1 : Form
    {
        private Point first = new Point(0,0), second = new Point(0,0);
        private Point currentPoint;

        private int diameter = 8;

        private class Triangle
        {
            public Point first, second, third;

            public Triangle(Point first, Point second, Point third)
            {
                this.first = first;
                this.second = second;
                this.third = third;
            }
        }

        List<Triangle> triangles;

        public Form1()
        {
            InitializeComponent();

            triangles = new List<Triangle>();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            currentPoint = e.Location;
            pictureBox.Invalidate();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            foreach (Triangle t in triangles)
            {
                e.Graphics.DrawLine(Pens.Black, t.first, t.second);
                e.Graphics.DrawLine(Pens.Black, t.second, t.third);
                e.Graphics.DrawLine(Pens.Black, t.third, t.first);

                e.Graphics.FillEllipse(Brushes.Blue, t.first.X - diameter / 2, t.first.Y - diameter / 2, diameter, diameter);
                e.Graphics.FillEllipse(Brushes.Blue, t.second.X - diameter / 2, t.second.Y - diameter / 2, diameter, diameter);
                e.Graphics.FillEllipse(Brushes.Blue, t.third.X - diameter / 2, t.third.Y - diameter / 2, diameter, diameter);
            }
            if (first != new Point(0, 0) && second == new Point(0, 0))
            {
                e.Graphics.DrawLine(Pens.Red, first, currentPoint);
                e.Graphics.FillEllipse(Brushes.Blue, first.X - diameter / 2, first.Y - diameter / 2, diameter, diameter);
            } else if (second != new Point(0, 0))
            {
                e.Graphics.DrawLine(Pens.Red, first, second);
                e.Graphics.DrawLine(Pens.Red, second, currentPoint);
                e.Graphics.DrawLine(Pens.Red, currentPoint, first);

                e.Graphics.FillEllipse(Brushes.Blue, first.X - diameter / 2, first.Y - diameter / 2, diameter, diameter);
                e.Graphics.FillEllipse(Brushes.Blue, second.X - diameter / 2, second.Y - diameter / 2, diameter, diameter);
                e.Graphics.FillEllipse(Brushes.Blue, currentPoint.X - diameter / 2, currentPoint.Y - diameter / 2, diameter, diameter);
            }
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (first == new Point(0, 0))
                first = e.Location;
            else if (second == new Point(0, 0))
                second = e.Location;
            else
            {
                triangles.Add(new Triangle(first, second, new Point(e.X, e.Y)));
                first = new Point(0, 0);
                second = new Point(0, 0);
            }
        }
    }
}
