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
    public partial class Sarpe : Form
    {
        string email;
        bool run;
        Queue<Point> snake = new Queue<Point>();
        Point fruit;

        enum Direction
        {
            Up, Down, Left, Right
        }
        Direction direction = Direction.Right;

        public Sarpe(string email)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.email = email;
            stopButton.Enabled = false;
        }

        private void Sarpe_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*AlegeJoc form = new AlegeJoc(email);
            this.Hide();
            form.Show();*/
        }

        private Point ChooseRandomPoint()
        {
            Random random = new Random();
            return new Point(random.Next(0, 35) * 10, random.Next(0, 35) * 10);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (run == true)
            {
                Point lastPoint = new Point(0,0);

                int index = 0;
                foreach (Point item in snake) {
                    if (index == snake.Count - 1)
                    {
                        lastPoint = item;
                        break;
                    }
                    index++;
                }

                if (direction == Direction.Up)
                    lastPoint = new Point(lastPoint.X, lastPoint.Y - 10);
                else if (direction == Direction.Down)
                    lastPoint = new Point(lastPoint.X, lastPoint.Y + 10);
                else if (direction == Direction.Left)
                    lastPoint = new Point(lastPoint.X - 10, lastPoint.Y);
                else if (direction == Direction.Right)
                    lastPoint = new Point(lastPoint.X + 10, lastPoint.Y);

                foreach (Point item in snake)
                {
                    if (Math.Abs(lastPoint.X - item.X) < 5 && Math.Abs(lastPoint.Y - item.Y) < 5)
                    {
                        run = false;
                        stopButton.Enabled = false;
                        startButton.Enabled = true;
                        timer1.Stop();
                        snake.Clear();
                        return;
                    }

                }

                bool eat = false;
                if (Math.Abs(lastPoint.X - fruit.X) < 5 && Math.Abs(lastPoint.Y - fruit.Y) < 5)
                {
                    eat = true;
                    fruit = ChooseRandomPoint();
                }

                if (lastPoint.X < -2 || lastPoint.X > 345 || lastPoint.Y < -2 || lastPoint.Y > 345)
                {
                    run = false;
                    stopButton.Enabled = false;
                    startButton.Enabled = true;
                    timer1.Stop();
                    snake.Clear();
                }
                else
                {
                    if (!eat)
                        snake.Dequeue();
                    snake.Enqueue(lastPoint);
                    pictureBox.Invalidate();
                }
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            snake.Enqueue(new Point(140, 140));
            direction = Direction.Right;
            run = true;
            fruit = ChooseRandomPoint();
            stopButton.Enabled = true;
            timer1.Start();
            startButton.Enabled = false;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            run = false;
            snake.Clear();
            timer1.Stop();
            stopButton.Enabled = false;
            startButton.Enabled = true;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            int index = 0;
            e.Graphics.FillRectangle(Brushes.Black, 0, 0, pictureBox.Width, pictureBox.Height);
            foreach (Point item in snake)
            {
                if (index == snake.Count - 1)
                    e.Graphics.FillEllipse(Brushes.White, item.X, item.Y, 10, 10);
                else
                    e.Graphics.FillEllipse(Brushes.Green, item.X, item.Y, 10, 10);
                index++;
            }

            e.Graphics.FillEllipse(Brushes.Red, fruit.X, fruit.Y, 10, 10);
        }

        private void Sarpe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                direction = Direction.Left;
            else if (e.KeyCode == Keys.D)
                direction = Direction.Right;
            else if (e.KeyCode == Keys.W)
                direction = Direction.Up;
            else if (e.KeyCode == Keys.S)
                direction = Direction.Down;
        }
    }
}
