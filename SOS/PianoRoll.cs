using System;
using System.Windows.Forms;
using System.Drawing;

namespace SOS
{
    public partial class PianoRoll : Form
    {
        public Track t;
        NumericUpDown trLng;
        DataGridView trInst, trComp;
        ComboBox paintInst;
        byte velocityBrush = 64;
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
            trLng.Value = 0;
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
                LoadIn(t.ExportPattern());
            else
            {
                t = new Track();
                trLng.Minimum = 16;
            }
        }
        public void SetInstruments(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("None");
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
                trComp.ColumnCount = Convert.ToInt32(trLng.Value);
            }
        }
        public void SetupTrComp(int str, int n, int szCell)
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
        public byte[,] Generate()
        {
            byte[,] t = new byte[129, trComp.ColumnCount];
            for (int i = 0; i < 129; i++)
            {
                for (int j = 0; j < trComp.ColumnCount; j++)
                {
                    if (i == 0)
                        t[i, j] = Convert.ToByte((trInst[j, 0].Value));
                    else
                        t[i, j] = Convert.ToByte(trComp[j, i - 1].Value);
                }
            }
            return t;
        }
        public void LoadIn(byte[,] p)
        {
            trLng.Value = p.GetLength(1);
            trComp.ColumnCount = p.GetLength(1);
            for (int i = 0; i < p.GetLength(1); i++)
            {
                for (int j = 0; j < 129; j++)
                {
                    if (j == 128)
                    {
                        trInst[i, 0].Value = p[j, i];
                    }
                    else
                    {
                        trComp[i, j].Value = p[j, i];
                        AssignColor(i, j);
                    }
                }
            }
            trLng.Minimum = 16;
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            t.ImportPattern(Generate(), trComp.ColumnCount);
        }
    }
}
