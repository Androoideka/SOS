using System;
using System.Windows.Forms;
using System.IO;

namespace SOS
{
    public partial class ProjektAdjuster : Form
    {
        public int tempo;
        public string[] fileNames;
        public string name;
        public ProjektAdjuster(int t)
        {
            tempo = t;
            InitializeComponent();
        }

        private void ProjektAdjuster_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Prestissimo");
            comboBox1.Items.Add("Presto");
            comboBox1.Items.Add("Allegro");
            comboBox1.Items.Add("Moderato");
            comboBox1.Items.Add("Andante");
            comboBox1.Items.Add("Adagio");
            comboBox1.Items.Add("Larghetto");
            comboBox1.Items.Add("Largo");
            //BECAUSE FUCK YOU THAT'S WHY
            comboBox1.SelectedIndex = tempo == 208 ? 0 : (tempo == 200 ? 1 : (tempo == 168 ? 2 : (tempo == 120 ? 3 : (tempo == 108 ? 4 : (tempo == 76 ? 5 : (tempo == 66 ? 6 : 7))))));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //THIS ONE IS ALSO A BIG FUCK YOU
            tempo = comboBox1.SelectedIndex == 0 ? 208 : (comboBox1.SelectedIndex == 1 ? 200 : (comboBox1.SelectedIndex == 2 ? 168 : (comboBox1.SelectedIndex == 3 ? 120 : (comboBox1.SelectedIndex == 4 ? 108 : (comboBox1.SelectedIndex == 5 ? 76 : (comboBox1.SelectedIndex == 6 ? 66 : 40))))));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                fileNames = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.wav");
            checkedListBox1.Items.Add(Path.GetFileName(folderBrowserDialog1.SelectedPath));
            for (int i = 0; i < fileNames.Length; i++)
            {
                checkedListBox1.Items.Add(Path.GetFileName(fileNames[i]));
            }
        }
    }
}
