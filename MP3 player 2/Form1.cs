using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MP3_player_2
{
    public partial class Form1 : Form
    {
        private string _filePath = string.Empty;
        private string _filePathForPlaylist = string.Empty;
        private string _durationString= string.Empty;
        List<string> playListFilePath= new List<string>();

        private int _playListCount=0;
        
        private int _seconds;
        private int _minutes;
        private int _hours;
        private int _secondsDuration;
        private int _minutesDuration;
        private int _hoursDuration;
        private int _currentSongIndex=0;

        private double _doubleSecondDuretion;

        private bool _isMediaPlaying = false;

        OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Filter = "Все файлы|*",
            Multiselect = false,
            ValidateNames = true,
        };

        WMPLib.WindowsMediaPlayer mediaPlayer = new WMPLib.WindowsMediaPlayer();
        
        public Form1()
        {
            InitializeComponent();
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
                    timer1.Start();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (_isMediaPlaying)
                mediaPlayer.controls.play();
                timer1.Start();

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _filePath = openFileDialog.FileName;
                playListFilePath.Add(_filePath);
                comboBox1.Items.Add(_filePath);
            }
                

        }

        private void button2_Click(object sender, EventArgs e)
        {
            mediaPlayer.controls.pause();
            timer1.Stop();
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
            
            trackBar2.Maximum = (int) mediaPlayer.currentMedia.duration;
            label4.Text = GetMediaDuration();
            trackBar2.Value=Convert.ToInt32(mediaPlayer.controls.currentPosition);
            if (mediaPlayer != null)
            {
                _seconds = Convert.ToInt32(mediaPlayer.controls.currentPosition);
                _hours = _seconds / 3600;
                _minutes = (_seconds - _hours / 3600) / 60;
                _seconds = _seconds % 60;
                label6.Text = String.Format("{0:D}:{1:D2}:{2:D2}", _hours, _minutes, _seconds);
                
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
        private string GetMediaDuration()
        {
            _doubleSecondDuretion=mediaPlayer.currentMedia.duration;
            _secondsDuration = Convert.ToInt32(mediaPlayer.currentMedia.duration);
            _hoursDuration = _secondsDuration / 3600;
            _minutesDuration = (_secondsDuration - _hoursDuration / 3600) / 60;
            _secondsDuration = _secondsDuration % 60;
            _durationString = String.Format("{0:D}:{1:D2}:{2:D2}", _hoursDuration, _minutesDuration, _secondsDuration);
            
            return _durationString;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void добавитьВПлейлистToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _filePathForPlaylist = openFileDialog.FileName;
                playListFilePath.Add(_filePathForPlaylist);
                comboBox1.Items.Add(_filePathForPlaylist);
            }
                
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_playListCount+1 <= playListFilePath.Count-1)
            {                               
                mediaPlayer.URL = playListFilePath[_playListCount+1];
                textBox1.Text = mediaPlayer.URL;
                _playListCount++;
                textBox2.Text = $"{_playListCount}";
            }
            else
            {
                MessageBox.Show("Вы не добавили следующую песню!");
                
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (_playListCount - 1 < 0)
            {
                MessageBox.Show("Это первая песня вашего плейлиста!");
                
            }
            else
            {
                mediaPlayer.URL = playListFilePath[_playListCount-1];
                textBox1.Text = mediaPlayer.URL;
                _playListCount--;
                textBox2.Text = $"{_playListCount}";
            }
        }

        private void label6_TextChanged(object sender, EventArgs e)
        {
            if (label6.Text == label4.Text)
            {
                
                if (_playListCount + 1 <= playListFilePath.Count - 1)
                {

                    mediaPlayer.URL = playListFilePath[_playListCount + 1];
                    textBox1.Text = mediaPlayer.URL;
                    _playListCount++;
                    textBox2.Text = $"{_playListCount}";
                }
                else
                {
                    MessageBox.Show("Конец плейлиста!");
                }

                
            }
        }
    }
}
