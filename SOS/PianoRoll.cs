using System;
using System.Windows.Forms;
using System.Drawing;

namespace SOS
{
    public partial class PianoRoll : Form
    {
        public Projekt p;
        private byte velocityBrush = 64;
        public PianoRoll(Track tr)
        {
            p = new Projekt();
            p.trLength++;
            p.tr[0] = tr;
            InitializeComponent();
        }
        private void PianoRoll_Load(object sender, EventArgs e)
        {
            MinimumSize = Size;
            MaximumSize = Size;
            trLng.Maximum = decimal.MaxValue;
            SetInstruments(paintInst);
            trInst.DoubleBuffered(true);
            trComp.DoubleBuffered(true);
            SetupTrComp(0, (int)trLng.Value, trInst.Height);
            if (p.tr[0] != null)
                LoadIn(p.tr[0].ExportPattern(), trComp, trInst);
            else
            {
                p.tr[0] = new Track();
                trLng.Minimum = 16;
            }
        }
        private void SetInstruments(ComboBox cb)
        {
            cb.Items.Clear();
            for (int i = 0; i < Projekt.sb.Length; i++)
            {
                if (i == 0 || Projekt.sb[i].ime != Projekt.sb[i - 1].ime)
                    cb.Items.Add(Projekt.sb[i].ime);
            }
            cb.SelectedIndex = 0;
        }
        private void ValChange(object sender, EventArgs e)
        {
            int tempCol = trComp.ColumnCount;
            if (trLng.Value > tempCol)
            {
                while (trLng.Value % 4 != 0)
                    trLng.Value += 1;
                SetupTrComp(tempCol, Convert.ToInt32(trLng.Value), trInst.Height);
            }
            else if (trLng.Value < tempCol)
            {
                while (trLng.Value % 4 != 0)
                    trLng.Value -= 1;
                trComp.ColumnCount = trInst.ColumnCount = Convert.ToInt32(trLng.Value);
            }
        }
        private void SetupTrComp(int str, int n, int szCell)
        {
            trComp.RowCount = 128;
            trInst.RowCount = 1;
            trComp.ColumnCount = trInst.ColumnCount = n;
            for (int i = str; i < trComp.ColumnCount; i++)
            {
                for (int j = 0; j < trComp.RowCount; j++)
                {
                    if (trComp.Rows[j].Height != szCell)
                        trComp.Rows[j].Height = szCell;
                    trComp.Columns[i].Width = szCell;
                    trComp[i, j].Value = 0;
                    AssignColor(i, j);
                }
                trInst.Rows[0].Height = szCell;
                trInst.Columns[i].Width = szCell;
                trInst[i, 0].Value = "0";
            }
            trLng.Enabled = true;
        }
        private void BeatTransform(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                trComp[e.ColumnIndex, e.RowIndex].Value = velocityBrush;
                AssignColor(e.ColumnIndex, e.RowIndex);
            }
        }
        private void ChangeColor(Color t, int loc1, int loc2)
        {
            trComp[loc1, loc2].Style.BackColor = t;
            trComp[loc1, loc2].Style.SelectionBackColor = t;
            trComp[loc1, loc2].Style.ForeColor = t;
            trComp[loc1, loc2].Style.SelectionForeColor = t;
        }
        private void AssignColor(int loc1, int loc2)
        {
            if (Convert.ToInt32(trComp[loc1, loc2].Value) != 0)
                ChangeColor(Color.Gold, loc1, loc2);
            else
                ChangeColor(Color.Gray, loc1, loc2);
        }
        private void InstrumentPaint(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1 && e.Button != MouseButtons.None)
                trInst[e.ColumnIndex, 0].Value = paintInst.SelectedIndex;
        }
        private byte[,] InsertFromDG(byte[,] p, DataGridView dgv, int str, int n)
        {
            for (int i = str; i < n; i++)
            {
                for (int j = 0; j < dgv.ColumnCount; j++)
                    p[i, j] = Convert.ToByte(dgv[j, i - str].Value);
            }
            return p;
        }
        private byte[,] Generate(DataGridView dgv1, DataGridView dgv2)
        {
            byte[,] t = new byte[dgv1.RowCount + dgv2.RowCount, dgv1.ColumnCount];
            InsertFromDG(t, dgv1, 0, dgv1.RowCount);
            InsertFromDG(t, dgv2, dgv1.RowCount, dgv1.RowCount + dgv2.RowCount);
            return t;
        }
        private void InsertIntoDG(byte[,] t, DataGridView dgv, int str, int n, bool ac)
        {
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                for (int j = str; j < n; j++)
                {
                    dgv[i, j - str].Value = t[j, i];
                    if (ac)
                        AssignColor(i, j - str);
                }
            }
        }
        private void LoadIn(byte[,] p, DataGridView dgv1, DataGridView dgv2)
        {
            trLng.Value = p.GetLength(1);
            InsertIntoDG(p, dgv1, 0, dgv1.RowCount, true);
            InsertIntoDG(p, dgv2, dgv1.RowCount, dgv1.RowCount + dgv2.RowCount, false);
            trLng.Minimum = 16;
        }
        private void PianoRoll_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTrack();
        }
        private void SaveTrack()
        {
            p.SaveToTrack(0, Generate(trComp, trInst), trComp.ColumnCount);
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveTrack();
        }
        private void instrumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Projekt.SetSoundbanks();
            SetInstruments(paintInst);
        }
        private void velocityBrushToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputVelocity inp = new InputVelocity(velocityBrush);
            inp.ShowDialog();
            velocityBrush = inp.velocity;
        }
        private void playStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveTrack();
            p.Play();
        }
    }
}
