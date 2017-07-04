using System;
using System.Windows.Forms;

namespace SOS
{
    public partial class Sequencer : Form
    {
        Projekt prj = new Projekt();
        PrikazTrake[] prk = new PrikazTrake[16];
        int n;
        public Sequencer()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Controls.Add(prk[n].trInst);
            Controls.Add(prk[n].trComp);
            Controls.Add(prk[n].trLng);
            n++;
            if (n < 16)
                button1.Top += button1.Size.Height + 39;
            else
                button1.Visible = false;
        }
        private void Sequencer_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            for (int i = 0; i < 16; i++)
            {
                prk[i] = new PrikazTrake(ClientRectangle, i, true);
                prk[i].trInst.SelectedIndexChanged += SelectInstrument;
                for (int j = 0; j < prj.sbLength; j++)
                    prk[i].trInst.Items.Add(j + ". " + prj.sb[j].ime);
            }
        }
        private void SelectInstrument(object sender, EventArgs e)
        {
            int brojInst = (sender as ComboBox).SelectedIndex;
            int tag = Convert.ToInt32((sender as ComboBox).Tag);
            if (prj.tr[tag] == null)
                prj.tr[tag] = new Track(prj.sb[brojInst]);
            else
                prj.tr[tag].instrument = prj.sb[brojInst];
            for (int i = 0; i < prj.sbLength; i++)
            {
                if (prj.sb[i].note.Length != prj.sb[brojInst].note.Length)
                    (sender as ComboBox).Items.Remove(i + ". " + prj.sb[i].ime);
            }
            if (prj.sb[brojInst].note.Length == 1)
                prk[tag].SetupTrComp(0, 16, 1);
            else
                prk[tag].trComp.Click += PROpen;
        }
        private void PROpen(object sender, EventArgs e)
        {
            PianoRoll pr = new PianoRoll(prj.tr[Convert.ToInt32((sender as DataGridView).Tag)]);
            pr.ShowDialog();
            prj.tr[Convert.ToInt32((sender as DataGridView).Tag)] = pr.t;
        }
        private void Sequencer_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                ProjektAdjuster pa = new ProjektAdjuster(60000 / prj.tmr.Interval / 4);
                pa.ShowDialog();
                prj.tmr.Interval = 60000 / pa.tempo / 4;
            }
        }
    }
}