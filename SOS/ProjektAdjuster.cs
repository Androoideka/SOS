using System;
using System.Windows.Forms;
using System.IO;

namespace SOS
{
    public partial class ProjektAdjuster : Form
    {
        //ComboBox[] cb = new ComboBox[128];
        //Label[] lb = new Label[128];
        string[] soundbanks;
        public ProjektAdjuster()
        {
            InitializeComponent();
        }
        private void ProjektAdjuster_Load(object sender, EventArgs e)
        {
            AddDefaultSound();
            LoadIn();
        }
        private void AddDefaultSound()
        {
            string t = Path.Combine(Application.StartupPath, "WDS");
            if (!Directory.Exists(t))
                Directory.CreateDirectory(t);
            File.Copy(@"C:\Windows\Media\tada.wav", Path.Combine(t, @"tada.wav"), false);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                //Copied from MSDN
                string path = Path.Combine(Application.StartupPath, Path.GetFileName(folderBrowserDialog1.SelectedPath));
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.wav");
                foreach (string s in files)
                {
                    string fileName = Path.GetFileName(s);
                    string destFile = Path.Combine(path, fileName);
                    File.Copy(s, destFile, true);
                }
            }
            LoadIn();
        }
        private void LoadIn()
        {
            string[] temp = Directory.GetDirectories(Application.StartupPath);
            soundbanks = new string[temp.Length];
            for (int i = 0; i < temp.Length; i++)
                soundbanks[i] = Path.GetFileName(temp[i]);
            AddToCB();
            ExistingSettings();
        }
        private void AddToCB()
        {
            for (int i = 0; i < 128; i++)
                cb[i].Items.AddRange(soundbanks);
        }
        private void ExistingSettings()
        {
            for (int i = 0; i < 128; i++)
            {
                cb[i].SelectedIndex = 0;
                for (int j = 0; j < cb[i].Items.Count; j++)
                {
                    if (Projekt.sb[i].ime == cb[i].Items[j].ToString())
                        cb[i].SelectedIndex = j;
                }
            }
        }
        private void ProjektAdjuster_FormClosing(object sender, FormClosingEventArgs e)
        {
            Projekt.sb = new Soundbank[128];
            for (int i = 0; i < 128; i++)
                Projekt.sb[i] = new Soundbank(cb[i].Items[cb[i].SelectedIndex].ToString(), Directory.GetFiles(Path.Combine(Application.StartupPath, cb[i].Items[cb[i].SelectedIndex].ToString())));
        }
    }
}
