using System;
using System.Windows.Forms;
using System.Drawing;

namespace SOS
{
    public partial class PianoRoll : Form
    {
        public Track t;
        private NumericUpDown trLng;
        private DataGridView trInst, trComp;
        private ComboBox paintInst;
        private byte velocityBrush = 64;
        public PianoRoll(Track tr)
        {
            t = tr;
            InitializeComponent();
        }
        private void PianoRoll_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            MinimumSize = Size;
            MaximumSize = Size;

            trLng = new NumericUpDown();
            trLng.Top = menuStrip1.Bottom;
            trLng.Font = new Font("Arial", 32f);
            trLng.Maximum = decimal.MaxValue;
            trLng.Enabled = true;
            trLng.ValueChanged += ValChange;

            paintInst = new ComboBox();
            paintInst.Top = menuStrip1.Bottom;
            paintInst.Left = trLng.Right + ClientRectangle.Width / 128;
            paintInst.Width = ClientRectangle.Width / 5;
            paintInst.Font = new Font("Arial", 32f);
            paintInst.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            paintInst.AutoCompleteSource = AutoCompleteSource.ListItems;
            paintInst.Enabled = true;
            SetInstruments(paintInst);

            trInst = new DataGridView();
            trInst.Top = trLng.Bottom + ClientRectangle.Height / 128;
            trInst.Width = ClientRectangle.Width;
            trInst.Height = ClientRectangle.Height / 16;
            trInst.ReadOnly = true;
            trInst.RowHeadersVisible = false;
            trInst.ColumnHeadersVisible = false;
            trInst.AllowUserToResizeColumns = false;
            trInst.AllowUserToResizeRows = false;
            trInst.AllowUserToAddRows = false;
            trInst.AllowUserToDeleteRows = false;
            trInst.ScrollBars = ScrollBars.Both;
            trInst.DoubleBuffered(true);
            trInst.Enabled = true;
            trInst.CellMouseMove += InstrumentPaint;

            trComp = new DataGridView();
            trComp.Top = trInst.Bottom + ClientRectangle.Height / 128;
            trComp.Width = ClientRectangle.Width;
            trComp.Height = ClientRectangle.Height - trComp.Top;
            trComp.ReadOnly = true;
            trComp.ColumnHeadersVisible = false;
            trComp.AllowUserToResizeColumns = false;
            trComp.AllowUserToResizeRows = false;
            trComp.AllowUserToAddRows = false;
            trComp.AllowUserToDeleteRows = false;
            trComp.ScrollBars = ScrollBars.Both;
            trComp.DoubleBuffered(true);
            trComp.Enabled = true;
            trComp.CellMouseMove += BeatTransform;

            trInst.Left = trComp.RowHeadersWidth;

            Controls.Add(trLng);
            Controls.Add(paintInst);
            Controls.Add(trInst);
            Controls.Add(trComp);
            SetupTrComp(0, (int)trLng.Value, ClientRectangle.Height / 16);
            if (t != null)
            {
                LoadIn(t.ExportPattern(), trComp, trInst);
            }
            else
            {
                t = new Track();
                trLng.Minimum = 16;
            }
        }
        private void SetInstruments(ComboBox cb)
        {
            cb.Items.Clear();
            for (int i = 0; i < Projekt.sb.Length; i++)
                cb.Items.Add(Projekt.sb[i].ime);
            cb.SelectedIndex = 0;
        }
        private void ValChange(object sender, EventArgs e)
        {
            int tempCol = trComp.ColumnCount;
            if (trLng.Value > tempCol)
            {
                while (trLng.Value % 4 != 0)
                    trLng.Value += 1;
                SetupTrComp(tempCol, Convert.ToInt32(trLng.Value), ClientRectangle.Height / 16);
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
                trInst[i, 0].Value = "1";
            }
            trLng.Enabled = true;
        }
        private void BeatTransform(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1 && e.Button != MouseButtons.None)
            {
                if (e.Button == MouseButtons.Left && trComp[e.ColumnIndex, e.RowIndex].Style.BackColor != Color.Gold)
                    trComp[e.ColumnIndex, e.RowIndex].Value = velocityBrush;
                else if (e.Button == MouseButtons.Right)
                    trComp[e.ColumnIndex, e.RowIndex].Value = 0;
                else if (e.Button == MouseButtons.Middle)
                {
                    InputVelocityForm inp = new InputVelocityForm(Convert.ToByte(trComp[e.ColumnIndex, 0].Value));
                    inp.ShowDialog();
                    trComp[e.ColumnIndex, e.RowIndex].Value = inp.velocity;
                    velocityBrush = inp.velocity;
                }
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
                    p[i, j] = Convert.ToByte(dgv[j, i-str].Value);
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
        private void InsertIntoDG(byte [,] t, DataGridView dgv, int str, int n, bool ac)
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
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t.ImportPattern(Generate(trComp, trInst), trComp.ColumnCount);
        }
    }
}
