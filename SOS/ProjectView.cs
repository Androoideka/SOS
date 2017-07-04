using System;
using System.Windows.Forms;

namespace SOS
{
    public partial class ProjectView : Form
    {
        Projekt prj = new Projekt();
        Button[] b = new Button[16];
        int n;
        public ProjectView()
        {
            InitializeComponent();
        }
        private void ProjectView_Load(object sender, EventArgs e)
        {
            Projekt.CreateAllInstruments();
            SetCheckedMenuItem();
            for (int i = 0; i < 16; i++)
            {
                b[i] = new Button();
                b[i].Top = button1.Top + i * 32;
                b[i].Left = button1.Left;
                b[i].Width = ClientRectangle.Width / 16;
                b[i].Tag = i;
                b[i].Text = "Edit Track " + i;
                b[i].Click += bClick;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Controls.Add(b[n]);
            bClick(b[n], new EventArgs());
            n++;
            if (n < 16)
                button1.Top += 32;
            else
                button1.Visible = false;
        }
        private void bClick(object sender, EventArgs e)
        {
            int i = Convert.ToInt32((sender as Button).Tag);
            PianoRoll pr = new PianoRoll(prj.tr[i]);
            pr.Show();
            prj.tr[i] = pr.p.tr[0];
        }
        private void SetCheckedMenuItem()
        {
            double tempo = prj.GetTempo();
            int index = tempo == 208 ? 0 : (tempo == 200 ? 1 : (tempo == 168 ? 2 : (tempo == 120 ? 3 : (tempo == 108 ? 4 : (tempo == 76 ? 5 : (tempo == 66 ? 6 : 7))))));
            (tempoToolStripMenuItem.DropDownItems[index] as ToolStripMenuItem).CheckState = CheckState.Checked;
        }
        private void tempoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int p = Convert.ToInt32((sender as ToolStripMenuItem).Tag);
            for (int i = 0; i < tempoToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (p == i)
                    (tempoToolStripMenuItem.DropDownItems[i] as ToolStripMenuItem).CheckState = CheckState.Checked;
                else
                    (tempoToolStripMenuItem.DropDownItems[i] as ToolStripMenuItem).CheckState = CheckState.Unchecked;
            }
            prj.Tempo(p);
        }

        private void playStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prj.Reset();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (n == 16)
                button1.Visible = true;
            else
                button1.Top -= 32;
            n--;
            Controls.Remove(b[n]);
            int i = Convert.ToInt32((sender as ToolStripMenuItem).Text);
            prj.DeleteTrack(i-1);
        }
        private void soundbanksToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Projekt.SetSoundbanks();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
