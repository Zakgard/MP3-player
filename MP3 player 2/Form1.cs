using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP3_player_2
{
    public partial class Form1 : Form
    {
        private string _filePath = string.Empty;
        private int seconds;
        private int minutes;
        private int hours;
        private bool _isMediaPlaying = false;
        WMPLib.WindowsMediaPlayer mediaPlayer = new WMPLib.WindowsMediaPlayer();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Value = 50;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_isMediaPlaying)
            {
                 try
                 {
                    mediaPlayer.URL = _filePath;
                    mediaPlayer.controls.play();
                    textBox1.Text = _filePath;
                    _isMediaPlaying = true;
                    trackBar2.Maximum = Convert.ToInt32(mediaPlayer.currentMedia.duration);
                    
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message, "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
            }
            if (_isMediaPlaying)
                mediaPlayer.controls.play();
            
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Все файлы|*",
                Multiselect = false,
                ValidateNames = true,
            };
            if(openFileDialog.ShowDialog() == DialogResult.OK)
                _filePath = openFileDialog.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mediaPlayer.controls.pause();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            mediaPlayer.controls.currentPosition = trackBar2.Value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            trackBar2.Value=Convert.ToInt32(mediaPlayer.controls.currentPosition);
            if (mediaPlayer != null)
            {
                seconds = Convert.ToInt32(mediaPlayer.controls.currentPosition);
                hours = seconds / 3600;
                minutes = (seconds - hours / 3600) / 60;
                seconds = seconds % 60;
                label6.Text = String.Format("{0:D}:{1:D2}:{2:D2}", hours, minutes, seconds);
            }
            else
                label6.Text = "0:00:00";
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            timer1.Enabled=true;
            timer1.Interval = 1000;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            mediaPlayer.settings.volume = trackBar1.Value;
        }

      
    }
}
