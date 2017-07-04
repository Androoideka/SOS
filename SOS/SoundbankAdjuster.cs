using System;
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace SOS
{
    public partial class SoundbankAdjuster : Form
    {
        Label[] lb = new Label[128];
        ComboBox[] cb = new ComboBox[128];
        public SoundbankAdjuster()
        {
            InitializeComponent();
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
        private void button1_Click(object sender, EventArgs e)
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
        private void LoadIn()
        {
            string[] temp = Directory.GetDirectories(Application.StartupPath);
            for (int i = 0; i < temp.Length; i++)
                temp[i] = Path.GetFileName(temp[i]);
            AddToCB(temp);
            ExistingSettings();
        }
        private void AddToCB(string[] p)
        {
            for (int i = 0; i < cb.Length; i++)
            {
                cb[i].Items.Clear();
                cb[i].Items.AddRange(p);
            }
        }
        private void ExistingSettings()
        {
            for (int i = 0; i < cb.Length; i++)
            {
                cb[i].SelectedIndex = 0;
                for (int j = 0; j < cb[i].Items.Count; j++)
                {
                    if (Projekt.sb[i].ime == cb[i].Items[j].ToString())
                        cb[i].SelectedIndex = j;
                }
            }
        }
        private void CreateControls()
        {
            for (int i = 0; i < cb.Length; i++)
            {
                lb[i] = new Label();
                lb[i].Left = button1.Left;
                lb[i].Text = "Instrument " + i;
                Controls.Add(lb[i]);
                cb[i] = new ComboBox();
                cb[i].Left = button1.Left;
                cb[i].Width = button1.Width;
                cb[i].AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb[i].AutoCompleteSource = AutoCompleteSource.ListItems;
                Controls.Add(cb[i]);
                cb[i].BringToFront();
                if (i == 0)
                {
                    lb[i].Top = button1.Bottom;
                    cb[i].Top = button1.Bottom + 12;
                }
                else
                {
                    lb[i].Top = cb[i - 1].Bottom;
                    cb[i].Top = cb[i - 1].Bottom + 12;
                }
            }
        }
        private void SoundbankAdjuster_Load(object sender, EventArgs e)
        {
            CreateControls();
            AddDefaultSound();
            LoadIn();
        }
        private void SoundbankAdjuster_FormClosing(object sender, FormClosingEventArgs e)
        {
            Projekt.sb = new Soundbank[128];
            for (int i = 0; i < cb.Length; i++)
                Projekt.sb[i] = new Soundbank(cb[i].Items[cb[i].SelectedIndex].ToString(), Directory.GetFiles(Path.Combine(Application.StartupPath, cb[i].Items[cb[i].SelectedIndex].ToString())));
        }
    }
}

