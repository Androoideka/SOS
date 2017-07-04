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
            SetCheckedMenuItem();
            for (int i = 0; i < 16; i++)
            {
                b[i] = new Button();
                b[i].Top = button1.Top + i * (button1.Height + 39);
                b[i].Left = button1.Left;
                b[i].Width = ClientRectangle.Width / 16;
                b[i].Tag = i;
                b[i].Text = "Edit Track " + i;
                b[i].Click += bClick;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PianoRoll pr = new PianoRoll(prj.tr[n]);
            pr.Show();
            prj.tr[n] = pr.t;
            Controls.Add(b[n]);
            n++;
            if (n < 16)
                button1.Top += button1.Height + 32;
            else
                button1.Visible = false;
        }
        private void bClick(object sender, EventArgs e)
        {
            int i = Convert.ToInt32((sender as Button).Tag);
            PianoRoll pr = new PianoRoll(prj.tr[i]);
            pr.ShowDialog();
            prj.tr[i] = pr.t;
        }
        private void ProjectView_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                ProjektAdjuster pa = new ProjektAdjuster();
                pa.ShowDialog();
            }
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

        private void playStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prj.Reset();
            prj.tmr.Enabled = true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
