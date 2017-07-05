using System;
using System.Windows.Forms;

namespace SOS
{
    public partial class ProjectView : Form
    {
        private Projekt prj = new Projekt();
        private Button[] b = new Button[16];
        private string loc;
        private int n;
        public ProjectView()
        {
            InitializeComponent();
        }
        private void ProjectView_Load(object sender, EventArgs e)
        {
            Projekt.SetSoundbanks();
            SetCheckedMenuItem();
            for (int i = 0; i < 16; i++)
            {
                b[i] = new Button();
                b[i].Top = button1.Top + i * 32;
                b[i].Left = button1.Left;
                b[i].Width = ClientRectangle.Width - 2 * b[i].Left;
                b[i].Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right);
                b[i].Tag = i;
                b[i].Text = "Edit Track " + (i+1);
                b[i].Click += bClick;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Controls.Add(b[n]);
            bClick(b[n], new EventArgs());
            n++;
            deleteToolStripMenuItem.DropDownItems.Add("Track " + n);
            deleteToolStripMenuItem.DropDownItems[n - 1].Tag = n - 1;
            deleteToolStripMenuItem.DropDownItems[n - 1].Click += deleteToolStripMenuItem_Click;
            if (n < 16)
                button1.Top += 32;
            else
                button1.Visible = false;
        }
        private void bClick(object sender, EventArgs e)
        {
            int i = Convert.ToInt32((sender as Button).Tag);
            PianoRoll pr = new PianoRoll();
            pr.p.SetTempo(prj.GetTempo());
            pr.p.tr[0] = prj.tr[i];
            pr.Show();
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
        // Methods for menu
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void instrumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Projekt.SetSoundbanks();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (n == 16)
                button1.Visible = true;
            else
                button1.Top -= 32;
            n--;
            int i = Convert.ToInt32((sender as ToolStripMenuItem).Tag);
            deleteToolStripMenuItem.DropDownItems.Remove(deleteToolStripMenuItem.DropDownItems[n]);
            Controls.Remove(b[n]);
            prj.DeleteTrack(i);
        }
        private void tempoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int p = Convert.ToInt32((sender as ToolStripMenuItem).Tag);
            prj.SetTempo(p);
            SetCheckedMenuItem();
        }
        private void playStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prj.Play();
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                loc = saveFileDialog1.FileName;
                prj.Save(loc);
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loc != null)
                prj.Save(loc);
            else
                saveAsToolStripMenuItem_Click(saveAsToolStripMenuItem, e);
        }

        private void ProjectView_FormClosing(object sender, FormClosingEventArgs e)
        {
            AudioPlaybackEngine.Instance.Dispose();
        }
    }
}