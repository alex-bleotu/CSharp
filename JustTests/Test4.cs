using System;
using System.Windows.Forms;
using System.Windows.Media;

namespace JustTests {
    public partial class Test4 : Form {

        public Test4() {
            InitializeComponent();
        }

        private void Test4_FormClosed(object sender, FormClosedEventArgs e) {
            Form1 form = new Form1();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            //SoundPlayer player = new SoundPlayer(Application.StartupPath + @"\random.wav");
            //player.Play();

            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(Application.StartupPath + @"\random.mp3"));

            player.Volume = 1;

            TimeSpan startTime = TimeSpan.FromSeconds(0);
            player.Position = startTime;

            player.Play();
        }
    }
}
