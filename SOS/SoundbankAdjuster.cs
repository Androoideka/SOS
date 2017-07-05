using System;
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Linq;

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
            GetAvailableInstruments();
        }
        private void GetAvailableInstruments()
        {
            string[] temp = Directory.GetDirectories(".");
            for (int j = 0; j < temp.Length; j++)
                temp[j] = Path.GetFileName(temp[j]);
            cb.Items.Clear();
            cb.Items.AddRange(temp);
            cb.Items.Add("Import Soundbanks...");
            LoadSetting();
        }
        private void ConvertAndOrResample(string s, string d)
        {
            if(s.EndsWith(".aiff"))
            {
                d = d.Replace("aiff", "wav");
                using (AiffFileReader reader = new AiffFileReader(s))
                {
                    s = s.Replace("aiff", "wav");
                    using (WaveFileWriter writer = new WaveFileWriter(s, reader.WaveFormat))
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead = 0;
                        do
                        {
                            bytesRead = reader.Read(buffer, 0, buffer.Length);
                            writer.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }
                }
            }
            using (AudioFileReader reader = new AudioFileReader(s))
            {
                ISampleProvider resampler = new WdlResamplingSampleProvider(reader, 44100);
                if (resampler.WaveFormat.Channels != 2)
                    resampler = new MonoToStereoSampleProvider(resampler);
                WaveFileWriter.CreateWaveFile(d, new SampleToWaveProvider(resampler));
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
                        var files = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.*").Where(s => s.EndsWith(".wav") || s.EndsWith(".aiff"));
                        for (int j = 0; j < files.Count(); j++)
                            ConvertAndOrResample(files.ElementAt(j), Path.Combine(path, Path.GetFileName(files.ElementAt(j))));
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
                Projekt.sb[i] = new Soundbank(cb.Items[cb.SelectedIndex].ToString());
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
