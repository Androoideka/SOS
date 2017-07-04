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
        private void Sequencer_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            MinimumSize = Size;
            MaximumSize = Size;
            SetCheckedMenuItem();
            for (int i = 0; i < 16; i++)
            {
                prk[i] = new PrikazTrake(ClientRectangle, i, true);
                prk[i].trComp.MouseClick += PROpen;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Controls.Add(prk[n].trInst);
            Controls.Add(prk[n].trComp);
            Controls.Add(prk[n].trLng);
            prj.tr[n] = new Track();
            n++;
            if (n < 16)
                button1.Top += button1.Size.Height + 39;
            else
                button1.Visible = false;
        }
        private void PROpen(object sender, MouseEventArgs e)
        {
            SaveTrack();
            if (prj.tr[Convert.ToInt32((sender as DataGridView).Tag)].instrument != null && (sender as DataGridView).HitTest(e.X, e.Y).Type == DataGridViewHitTestType.None)
            {
                PianoRoll pr = new PianoRoll(prj.tr[Convert.ToInt32((sender as DataGridView).Tag)]);
                pr.ShowDialog();
                prj.tr[Convert.ToInt32((sender as DataGridView).Tag)] = pr.t;
            }
        }
        private void Sequencer_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                ProjektAdjuster pa = new ProjektAdjuster();
                pa.ShowDialog();
                for (int i = 0; i < n; i++)
                {
                    if (prk[i].trInst.SelectedIndex != -1)
                    {
                        SaveTrack();
                        prk[i].SetInstruments(prj.tr[i].instrument.ime);
                    }
                    else
                        prk[i].SetInstruments();
                }
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveTrack();
        }
        private void SaveTrack()
        {
            for (int i = 0; i < n; i++)
            {
                if (prk[i].trInst.SelectedIndex != -1)
                {
                    string temp = prk[i].trInst.Items[prk[i].trInst.SelectedIndex].ToString();
                    temp = temp.Substring(temp.IndexOf(" ") + 1);
                    prj.tr[i].instrument = Projekt.FindInstrumentWithName(temp);
                    prj.tr[i].ImportPattern(prk[i].Generate(), prk[i].trComp.RowCount);
                }
                else
                    MessageBox.Show("Unable to save track " + i + "\nNo Instrument Selected");
            }
        }
        private void SaveTrack(int i)
        {
            if (prk[i].trInst.SelectedIndex != -1)
            {
                string temp = prk[i].trInst.Items[prk[i].trInst.SelectedIndex].ToString();
                temp = temp.Substring(temp.IndexOf(" ") + 1);
                prj.tr[i].instrument = Projekt.FindInstrumentWithName(temp);
                prj.tr[i].ImportPattern(prk[i].Generate(), prk[i].trComp.RowCount);
            }
            else
                MessageBox.Show("Unable to save track " + (i+1) + "\nNo Instrument Selected");
        }
        private void DeleteTrack(int i)
        {
            if (prj.tr[i] != null)
            {
                button1.Top -= (button1.Height + 39);
                Controls.Remove(prk[i].trInst);
                Controls.Remove(prk[i].trComp);
                Controls.Remove(prk[i].trLng);
                n--;
                prj.tr[i] = null;
            }
            else
                MessageBox.Show("Unable to delete track " + (i+1) + "\nNot there");
        }
        private void SetCheckedMenuItem()
        {
            int tempo = prj.GetTempo();
            int index = tempo == 208 ? 0 : (tempo == 200 ? 1 : (tempo == 168 ? 2 : (tempo == 120 ? 3 : (tempo == 108 ? 4 : (tempo == 76 ? 5 : (tempo == 66 ? 6 : 7))))));
            (tempoToolStripMenuItem.DropDownItems[index] as ToolStripMenuItem).CheckState = CheckState.Checked;
        }
        private void tempoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int p = Convert.ToInt32((sender as ToolStripMenuItem).Tag);
            for (int i = 0; i < tempoToolStripMenuItem.DropDownItems.Count; i++)
            {
                if(p == i)
                    (tempoToolStripMenuItem.DropDownItems[i] as ToolStripMenuItem).CheckState = CheckState.Checked;
                else
                    (tempoToolStripMenuItem.DropDownItems[i] as ToolStripMenuItem).CheckState = CheckState.Unchecked;
            }
            prj.SetTempo(p == 0 ? 208 : (p == 1 ? 200 : (p == 2 ? 168 : (p == 3 ? 120 : (p == 4 ? 108 : (p == 5 ? 76 : (p == 6 ? 66 : 40)))))));
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DeleteTrack(Convert.ToInt32((sender as ToolStripMenuItem).Text) - 1);
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            SaveTrack(Convert.ToInt32((sender as ToolStripMenuItem).Text) - 1);
        }
    }
}