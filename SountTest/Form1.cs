using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace SountTest {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + @"\sound.wav");
            player.Play();
        }

        private void button2_Click(object sender, EventArgs e) {
            WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
            player.URL = Application.StartupPath + @"\sound.mp3";
            player.controls.play();
        }
    }
}
