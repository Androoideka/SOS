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
            Projekt.SetSoundbanks();
            SetCheckedMenuItem();
            for (int i = 0; i < 16; i++)
            {
                b[i] = new Button();
                b[i].Top = button1.Top + i * 32;
                b[i].Left = button1.Left;
                b[i].Width = ClientRectangle.Width - 2 * b[i].Left;
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
            prj.trLength++;
        }
        private void SetCheckedMenuItem()
        {
            int index = prj.GetTempo();
            for (int i = 0; i < tempoToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (index == i)
                    (tempoToolStripMenuItem.DropDownItems[i] as ToolStripMenuItem).CheckState = CheckState.Checked;
                else
                    (tempoToolStripMenuItem.DropDownItems[i] as ToolStripMenuItem).CheckState = CheckState.Unchecked;
            }
        }
        private void tempoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int p = Convert.ToInt32((sender as ToolStripMenuItem).Tag);
            prj.SetTempo(p);
            SetCheckedMenuItem();
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
            prj.DeleteTrack(i - 1);
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
