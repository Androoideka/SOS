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
            AddDefaultSound();
            LoadIn();
        }
        private void AddDefaultSound()
        {
            string t = Path.Combine(Application.StartupPath, "WDS");
            if (!Directory.Exists(t))
                Directory.CreateDirectory(t);
            File.Copy(@"C:\Windows\Media\tada.wav", Path.Combine(t, @"tada.wav"), true);
        }
        private void Resample(string s, string d)
        {
            using (var reader = new AudioFileReader(s))
            {
                var resampler = new WdlResamplingSampleProvider(reader, 44100);
                WaveFileWriter.CreateWaveFile16(d, resampler);
            }
        }
        private void LoadIn()
        {
            string[] temp = Directory.GetDirectories(Application.StartupPath);
            for (int i = 0; i < temp.Length; i++)
                temp[i] = Path.GetFileName(temp[i]);
            cb.Items.Clear();
            cb.Items.AddRange(temp);
            cb.Items.Add("Import Soundbanks...");
            LoadSetting();
        }
        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb.SelectedIndex == cb.Items.Count - 1)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    string path = Path.Combine(Application.StartupPath, Path.GetFileName(folderBrowserDialog1.SelectedPath));
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.wav");
                    for (int i = 0; i < files.Length; i++)
                        Resample(files[i], Path.Combine(path, Path.GetFileName(files[i])));
                }
                LoadIn();
            }
        }
        private void SaveSetting()
        {
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
