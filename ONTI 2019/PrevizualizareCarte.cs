using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2019 {
    public partial class PrevizualizareCarte : Form {
        DataBase.Book book;
        DataBase dataBase;

        public PrevizualizareCarte(int id) {
            InitializeComponent();

            dataBase = new DataBase();

            book = dataBase.GetBook(id);

            textBox1.Text = book.title.Trim();
            textBox2.Text = book.author.Trim();
            textBox3.Text = book.pageNumber.ToString();
            pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\Resurse\Imagini\carti\" + id + ".jpg");
        }

        private void button1_Click(object sender, EventArgs e) {
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            Image image = new Bitmap(pictureBox1.Image, new Size(200, 200));

            e.Graphics.DrawImage(image, new Point(100, 100));

            e.Graphics.DrawImage(image, new Rectangle(50, 50, 100, 100), new Rectangle(0, 0, 100, 100), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(250, 50, 100, 100), new Rectangle(100, 0, 100, 100), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(50, 250, 100, 100), new Rectangle(0, 100, 100, 100), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(250, 250, 100, 100), new Rectangle(100, 100, 100, 100), GraphicsUnit.Pixel);

            e.Graphics.DrawImage(image, new Rectangle(25, 25, 50, 50), new Rectangle(0, 0, 50, 50), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(125, 25, 50, 50), new Rectangle(50, 0, 50, 50), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(25, 125, 50, 50), new Rectangle(0, 50, 50, 50), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(125, 125, 50, 50), new Rectangle(50, 50, 50, 50), GraphicsUnit.Pixel);

            e.Graphics.DrawImage(image, new Rectangle(225, 25, 50, 50), new Rectangle(100, 0, 50, 50), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(325, 25, 50, 50), new Rectangle(150, 0, 50, 50), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(225, 125, 50, 50), new Rectangle(100, 50, 50, 50), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(325, 125, 50, 50), new Rectangle(150, 50, 50, 50), GraphicsUnit.Pixel);

            e.Graphics.DrawImage(image, new Rectangle(25, 225, 50, 50), new Rectangle(0, 100, 50, 50), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(125, 225, 50, 50), new Rectangle(50, 100, 50, 50), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(25, 325, 50, 50), new Rectangle(0, 150, 50, 50), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(125, 325, 50, 50), new Rectangle(50, 150, 50, 50), GraphicsUnit.Pixel);

            e.Graphics.DrawImage(image, new Rectangle(225, 225, 50, 50), new Rectangle(100, 100, 50, 50), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(325, 225, 50, 50), new Rectangle(150, 100, 50, 50), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(225, 325, 50, 50), new Rectangle(100, 150, 50, 50), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(image, new Rectangle(325, 325, 50, 50), new Rectangle(150, 150, 50, 50), GraphicsUnit.Pixel);
        }
    }
}
