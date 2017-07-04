using System;
using System.Windows.Forms;
using System.IO;

namespace SOS
{
    public partial class ProjektAdjuster : Form
    {
        string[] soundbanks;
        public ProjektAdjuster()
        {
            InitializeComponent();
        }
        private void ProjektAdjuster_Load(object sender, EventArgs e)
        {
            lb[0] = new Label();
            lb[0].Top = button1.Bottom;
            lb[0].Left = button1.Left;
            lb[0].Text = "Instrument 1";
            Controls.Add(lb[0]);
            cb[0] = new ComboBox();
            cb[0].Top = button1.Bottom + 12;
            cb[0].Left = button1.Left;
            cb[0].Width = ClientRectangle.Width - (2 * cb[0].Left);
            cb[0].AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            cb[0].AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            Controls.Add(cb[0]);
            cb[0].BringToFront();
            for (int i = 1; i < 128; i++)
            {
                lb[i] = new Label();
                lb[i].Top = cb[i - 1].Bottom;
                lb[i].Left = lb[i - 1].Left;
                lb[i].Text = "Instrument " + (i + 1);
                Controls.Add(lb[i]);
                cb[i] = new ComboBox();
                cb[i].Top = cb[i - 1].Bottom + 12;
                cb[i].Left = cb[i - 1].Left;
                cb[i].Width = cb[i - 1].Width;
                cb[i].AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                cb[i].AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
                Controls.Add(cb[i]);
                cb[i].BringToFront();
            }
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
