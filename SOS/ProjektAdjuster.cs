using System;
using System.Windows.Forms;
using System.IO;

namespace SOS
{
    public partial class ProjektAdjuster : Form
    {
        public string[] fileNames;
        public string name;
        public ProjektAdjuster()
        {
            InitializeComponent();
        }
        private void ProjektAdjuster_Load(object sender, EventArgs e)
        {
            checkedListBox1.CheckOnClick = true;
            LoadIn();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
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
            checkedListBox1.Items.Clear();
            string[] temp = Directory.GetDirectories(Application.StartupPath);
            for (int i = 0; i < temp.Length; i++)
                checkedListBox1.Items.Add(Path.GetFileName(temp[i]));
        }

        private void ProjektAdjuster_FormClosing(object sender, FormClosingEventArgs e)
        {
            Projekt.sbLength = checkedListBox1.CheckedItems.Count;
            Projekt.sb = new Soundbank[Projekt.sbLength];
            for (int i = 0; i < Projekt.sbLength; i++)
                Projekt.sb[i] = new Soundbank(checkedListBox1.Items[checkedListBox1.CheckedIndices[i]].ToString(), Directory.GetFiles(Path.Combine(Application.StartupPath, checkedListBox1.Items[checkedListBox1.CheckedIndices[i]].ToString())));
        }
    }
}
