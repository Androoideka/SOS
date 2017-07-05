using System;
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace SOS
{
    public partial class SoundbankAdjuster : Form
    {
        int i = 0;
        public SoundbankAdjuster()
        {
            InitializeComponent();
        }
        private void SoundbankAdjuster_Load(object sender, EventArgs e)
        {
            MinimumSize = Size;
            MaximumSize = Size;
            Projekt.LoadInstruments();
            GetAvailableInstruments();
        }
        private void AddDefaultSound()
        {
            if (!Directory.Exists(@"WDS"))
            {
                Directory.CreateDirectory(@"WDS");
                File.Copy(@"C:\Windows\Media\tada.wav", Path.Combine(@"WDS", @"tada.wav"), true);
            }
        }
        private void GetAvailableInstruments()
        {
            AddDefaultSound();
            string[] temp = Directory.GetDirectories(".");
            for (int j = 0; j < temp.Length; j++)
                temp[j] = Path.GetFileName(temp[j]);
            cb.Items.Clear();
            cb.Items.AddRange(temp);
            cb.Items.Add("Import Soundbanks...");
            LoadSetting();
        }
        private void Resample(string s, string d)
        {
            using (AudioFileReader reader = new AudioFileReader(s))
            {
                var resampler = new SampleToWaveProvider(new WdlResamplingSampleProvider(reader, 44100));
                WaveFileWriter.CreateWaveFile(d, resampler);
            }
        }
        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb.SelectedIndex == cb.Items.Count - 1)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    string path = Path.GetFileName(folderBrowserDialog1.SelectedPath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                        string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.wav");
                        for (int j = 0; j < files.Length; j++)
                            Resample(files[j], Path.Combine(path, Path.GetFileName(files[j])));
                    }
                    else
                        MessageBox.Show("Directory already exists.");
                }
                GetAvailableInstruments();
            }
        }
        private void SaveSetting()
        {
            if(cb.Items[cb.SelectedIndex].ToString() != Projekt.sb[i].ime)
                Projekt.sb[i] = new Soundbank(cb.Items[cb.SelectedIndex].ToString(), Directory.GetFiles(Path.Combine(Application.StartupPath, cb.Items[cb.SelectedIndex].ToString())));
        }
        private void LoadSetting()
        {
            cb.SelectedIndex = 0;
            for (int j = 0; j < cb.Items.Count; j++)
            {
                if (Projekt.sb[i].ime == cb.Items[j].ToString())
                    cb.SelectedIndex = j;
            }
        }
        private void nud_ValueChanged(object sender, EventArgs e)
        {
            SaveSetting();
            i = (int)nud.Value - 1;
            LoadSetting();
        }
        private void SoundbankAdjuster_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSetting();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
