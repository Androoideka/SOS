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
            prk.SetupTrComp(0, 16, t.instrument.note.Length);
            Controls.Add(prk.trInst);
            Controls.Add(prk.trComp);
            Controls.Add(prk.trLng);
        }
    }
}
