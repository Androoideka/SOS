using System;
using System.Windows.Forms;

namespace SOS
{
    public partial class PianoRoll : Form
    {
        public Track t;
        PrikazTrake prk;
        public PianoRoll(Track tr)
        {
            t = tr;
            InitializeComponent();
        }
        private void PianoRoll_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            prk = new PrikazTrake(ClientRectangle, 0, false);
            prk.SetInstruments(t.instrument.ime);
            prk.SetupTrComp(0, 16, t.instrument.note.Length);
            Controls.Add(prk.trInst);
            Controls.Add(prk.trComp);
            Controls.Add(prk.trLng);
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveTrack();
        }
        private void SaveTrack()
        {
            if (prk.trInst.SelectedIndex != -1)
            {
                string temp = prk.trInst.Items[prk.trInst.SelectedIndex].ToString();
                temp = temp.Substring(temp.IndexOf(" ") + 1);
                t.instrument = Projekt.FindInstrumentWithName(temp);
                t.ImportPattern(prk.Generate(), prk.trComp.RowCount);
            }
            else
                MessageBox.Show("Unable to save track\nNo Instrument Selected");
        }
    }
}
