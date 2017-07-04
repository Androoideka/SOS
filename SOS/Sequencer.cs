using System;
using System.Drawing;
using System.Windows.Forms;

namespace SOS
{
    public partial class Sequencer : Form
    {
        Projekt prj = new Projekt();
        PrikazTrake[] prk = new PrikazTrake[16];
        int n;
        struct PrikazTrake
        {
            public ComboBox trInst;
            public DataGridView trComp;
            public PrikazTrake(int height, int formWidth, int p)
            {
                trInst = new ComboBox();
                trInst.Left = 12;
                trInst.Top = 12 + p * (32 + height);
                trInst.Font = new Font("Arial", 32f);
                trInst.Width = 256;
                trInst.DropDownStyle = ComboBoxStyle.DropDownList;
                trInst.Tag = p;
                trComp = new DataGridView();
                trComp.Top = trInst.Top;
                trComp.Left = trInst.Left + trInst.Size.Width + 12;
                trComp.Width = formWidth - trComp.Left;
                trComp.Tag = p;
                trComp.ColumnHeadersVisible = false;
                trComp.RowHeadersVisible = false;
                trComp.ReadOnly = true;
                trComp.AllowUserToResizeColumns = false;
                trComp.AllowUserToResizeRows = false;
                trComp.ScrollBars = ScrollBars.Horizontal;
            }
        }
        public Sequencer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            prk[n].trInst.Enabled = true;
            prk[n].trComp.Enabled = true;
            Controls.Add(prk[n].trInst);
            prk[n].trComp.Height = prk[n].trInst.Height;
            Controls.Add(prk[n].trComp);
            n++;
            if (n < 16)
                button1.Top += button1.Size.Height + 32;
            else
                button1.Visible = false;
        }

        private void Sequencer_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            for (int i = 0; i < 16; i++)
            {
                prk[i] = new PrikazTrake(button1.Height, ClientRectangle.Width, i);
                prk[i].trInst.SelectedIndexChanged += SelectInstrument;
                for (int j = 0; j < prj.sbLength; j++)
                    prk[i].trInst.Items.Add(j + ". " + prj.sb[j].ime);
            }
        }
        private void SelectInstrument(object sender, EventArgs e)
        {
            int brojInst = (sender as ComboBox).SelectedIndex;
            int tag = Convert.ToInt32((sender as ComboBox).Tag);
            prj.tr[tag] = new Track(prj.sb[brojInst]);
            (sender as ComboBox).Items.Clear();
            (sender as ComboBox).SelectedIndexChanged -= SelectInstrument;
            for (int i = 0; i < prj.sbLength; i++)
            {
                if(prj.sb[i].note.Length == prj.sb[brojInst].note.Length)
                    (sender as ComboBox).Items.Add(i + ". " + prj.sb[i].ime);
                if (brojInst == i)
                    (sender as ComboBox).SelectedIndex = (sender as ComboBox).Items.Count - 1;
            }
            (sender as ComboBox).SelectedIndexChanged += SelectInstrument;
            if (prj.sb[brojInst].percussion)
            {
                prk[tag].trComp.RowCount = 1;
                prk[tag].trComp.ColumnCount = 120;
                prk[tag].trComp.Height = prk[tag].trInst.Size.Height;
                prk[tag].trComp.Width = 40 * 25 + 3;
                prk[tag].trComp.Rows[0].Height = prk[tag].trInst.Size.Height;
                for (int i = 0; i < 120; i++)
                {
                    prk[tag].trComp.Columns[i].Width = 25;
                    prk[tag].trComp[i, 0].Style.BackColor = Color.Gray;
                }
                prk[tag].trComp.CellEnter += Beat;
            }
            else
                prk[tag].trComp.Click += PROpen;
        }
        private void Beat(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (sender as DataGridView);
            if (dgv[e.ColumnIndex, e.RowIndex].Style.BackColor == Color.Gray)
                dgv[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Gold;
            else
                dgv[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Gray;
        }
        private void PROpen(object sender, EventArgs e)
        {
            PianoRoll pr = new PianoRoll();
        }
    }
}
