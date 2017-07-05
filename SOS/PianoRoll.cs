using System;
using System.Windows.Forms;
using System.Drawing;

namespace SOS
{
    public partial class PianoRoll : Form
    {
        public Projekt p;
        private byte velocityBrush = 64;
        public PianoRoll()
        {
            p = new Projekt();
            p.trLength++;
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
            SetupDataGrids(0, (int)trLng.Value, trInst.Height);
            if (p.tr[0] != null)
                LoadIn(p.tr[0].ExportPattern(), trComp, trInst);
            else
            {
                p.tr[0] = new Track();
                trLng.Minimum = 16;
            }
        }
        // Methods for upper 2 controls
        private void ValChange(object sender, EventArgs e)
        {
            int tempCol = trComp.ColumnCount;
            if (trLng.Value > tempCol)
            {
                while (trLng.Value % 4 != 0)
                    trLng.Value += 1;
                SetupDataGrids(tempCol, Convert.ToInt32(trLng.Value), trInst.Height);
            }
            else if (trLng.Value < tempCol)
            {
                while (trLng.Value % 4 != 0)
                    trLng.Value -= 1;
                trComp.ColumnCount = trInst.ColumnCount = Convert.ToInt32(trLng.Value);
            }
        }
        private void SetInstruments(ComboBox cb)
        {
            cb.Items.Clear();
            for (int i = 0; i < Projekt.sb.Length; i++)
            {
                if (!cb.Items.Contains(Projekt.sb[i].ime))
                    cb.Items.Add(Projekt.sb[i].ime);
            }
            cb.SelectedIndex = 0;
        }
        // Methods for data grid values in bulk
        /// <summary>
        /// Add cells of same format as others to data grids
        /// </summary>
        /// <param name="str">Start Column for Adding Cells</param>
        /// <param name="n">End Column for Adding Cells</param>
        /// <param name="szCell">Size of Cell</param>
        private void SetupDataGrids(int str, int n, int szCell)
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
                    AssignColour(i, j);
                }
                trInst.Rows[0].Height = szCell;
                trInst.Columns[i].Width = szCell;
                trInst[i, 0].Value = 0;
            }
            trLng.Enabled = true;
        }
        /// <summary>
        /// Write to byte array all values in data grid
        /// </summary>
        /// <param name="p">Array to write to</param>
        /// <param name="dgv">Data grid to add values from</param>
        /// <param name="str">Start row</param>
        /// <param name="n">End row</param>
        /// <returns></returns>
        private byte[,] InsertFromDG(byte[,] p, DataGridView dgv, int str, int n)
        {
            for (int i = str; i < n; i++)
            {
                for (int j = 0; j < dgv.ColumnCount; j++)
                    p[i, j] = Convert.ToByte(dgv[j, i - str].Value);
            }
            return p;
        }
        /// <summary>
        /// Insert values from byte array into data grid
        /// </summary>
        /// <param name="t">Array to read from</param>
        /// <param name="dgv">Data grid to add values to</param>
        /// <param name="str">Start row</param>
        /// <param name="n">End row</param>
        /// <param name="ac">Whether to apply method Assign Colour (depends on data grid format)</param>
        private void InsertIntoDG(byte[,] t, DataGridView dgv, int str, int n, bool ac)
        {
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                for (int j = str; j < n; j++)
                {
                    dgv[i, j - str].Value = t[j, i];
                    if (ac)
                        AssignColour(i, j - str);
                }
            }
        }
        /// <summary>
        /// Generate byte array for importing values into track
        /// </summary>
        /// <param name="dgv1"></param>
        /// <param name="dgv2"></param>
        /// <returns>Values to write into track</returns>
        private byte[,] Generate(DataGridView dgv1, DataGridView dgv2)
        {
            byte[,] t = new byte[dgv1.RowCount + dgv2.RowCount, dgv1.ColumnCount];
            InsertFromDG(t, dgv1, 0, dgv1.RowCount);
            InsertFromDG(t, dgv2, dgv1.RowCount, dgv1.RowCount + dgv2.RowCount);
            return t;
        }
        /// <summary>
        /// Load all values for data grids from byte array
        /// </summary>
        /// <param name="p"></param>
        /// <param name="dgv1"></param>
        /// <param name="dgv2"></param>
        private void LoadIn(byte[,] p, DataGridView dgv1, DataGridView dgv2)
        {
            trLng.Value = p.GetLength(1);
            InsertIntoDG(p, dgv1, 0, dgv1.RowCount, true);
            InsertIntoDG(p, dgv2, dgv1.RowCount, dgv1.RowCount + dgv2.RowCount, false);
            trLng.Minimum = 16;
        }
        // Methods for singular values in data grids
        private void InstrumentPaint(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
                trInst[e.ColumnIndex, 0].Value = Projekt.GetInstrumentWithName(paintInst.Items[paintInst.SelectedIndex].ToString());
        }
        private void ChangeColour(Color t, int loc1, int loc2)
        {
            trComp[loc1, loc2].Style.BackColor = t;
            trComp[loc1, loc2].Style.SelectionBackColor = t;
            trComp[loc1, loc2].Style.ForeColor = t;
            trComp[loc1, loc2].Style.SelectionForeColor = t;
        }
        private void AssignColour(int loc1, int loc2)
        {
            if (Convert.ToInt32(trComp[loc1, loc2].Value) != 0)
                ChangeColour(Color.Gold, loc1, loc2);
            else
                ChangeColour(Color.Gray, loc1, loc2);
        }
        private void BeatTransform(bool selection, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                if (e.Button == MouseButtons.Right)
                    trComp[e.ColumnIndex, e.RowIndex].Value = 0;
                else if (selection && e.Button == MouseButtons.Left)
                {
                    trComp[e.ColumnIndex, e.RowIndex].Selected = true;
                    trComp[e.ColumnIndex, e.RowIndex].Value = velocityBrush;
                    AudioPlaybackEngine.Instance.Play(Projekt.sb[Convert.ToByte(trInst[e.ColumnIndex, 0].Value)].note[e.RowIndex], Convert.ToSingle(trComp[e.ColumnIndex, e.RowIndex].Value));
                }
                AssignColour(e.ColumnIndex, e.RowIndex);
            }
        }
        private void trComp_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            BeatTransform(true, e);
        }
        private void trComp_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            BeatTransform(!trComp[e.ColumnIndex, e.RowIndex].Selected, e);
        }
        // Methods for menu and form
        private void SaveTrack()
        {
            p.SaveToTrack(0, Generate(trComp, trInst), trComp.ColumnCount);
        }
        private void PianoRoll_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTrack();
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
