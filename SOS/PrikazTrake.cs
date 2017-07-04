using System;
using System.Windows.Forms;
using System.Drawing;

namespace SOS
{
    class PrikazTrake
    {
        public ComboBox trInst;
        public DataGridView trComp;
        public NumericUpDown trLng;
        int szCell;
        public PrikazTrake(Rectangle form, int p, bool t)
        {
            trInst = new ComboBox();
            trInst.Top = 30 + p * 64;
            trInst.Left = 0;
            trInst.Font = new Font("Arial", 32f);
            trInst.Width = form.Width / 5;
            trInst.Tag = p;
            trInst.DropDownStyle = ComboBoxStyle.DropDownList;
            trInst.Enabled = true;
            SetInstruments();

            trComp = new DataGridView();
            trComp.Tag = p;
            trComp.ColumnHeadersVisible = false;
            trComp.ReadOnly = true;
            trComp.AllowUserToResizeColumns = false;
            trComp.AllowUserToResizeRows = false;
            trComp.AllowUserToAddRows = false;
            trComp.AllowUserToDeleteRows = false;
            trComp.ScrollBars = ScrollBars.Both;
            trComp.DoubleBuffered(true);
            trComp.Enabled = false;
            szCell = form.Width / 16;

            trLng = new NumericUpDown();
            trLng.Top = trInst.Top;
            trLng.Font = new Font("Arial", 32f);
            trLng.Tag = p;
            trLng.Minimum = 16;
            trLng.Maximum = decimal.MaxValue;
            trLng.Enabled = false;

            trInst.SelectedIndexChanged += SelectInstrument;
            trComp.CellMouseMove += BeatTransform;
            trLng.ValueChanged += ValChange;

            if (t)
            {
                trComp.Top = trInst.Top;
                trComp.Left = trInst.Right + form.Width / 128;
                trComp.Width = form.Width - trComp.Left - form.Width / 8;
                trComp.Height = trInst.Height;
                trComp.RowHeadersVisible = false;
                trLng.Left = trComp.Right + form.Width / 128;
                trLng.Width = form.Width - trLng.Left;
            }
            else
            {
                trComp.Top = trInst.Bottom + form.Width / 128;
                trComp.Left = 0;
                trComp.Width = form.Width;
                trComp.Height = form.Height - trComp.Top;
                trLng.Left = trInst.Right + form.Width / 128;
            }
        }
        public void SetupTrComp(int str, int n, int noN)
        {
            trComp.RowCount = noN;
            trComp.ColumnCount = n;
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
            }
            trLng.Enabled = true;
        }
        private void BeatTransform(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1 && e.Button != MouseButtons.None)
            {
                if (e.Button == MouseButtons.Left && trComp[e.ColumnIndex, e.RowIndex].Style.BackColor != Color.Gold)
                    trComp[e.ColumnIndex, e.RowIndex].Value = 64;
                else if (e.Button == MouseButtons.Right)
                    trComp[e.ColumnIndex, e.RowIndex].Value = 0;
                else if (e.Button == MouseButtons.Middle)
                {
                    InputVelocityForm inp = new InputVelocityForm(Convert.ToInt32(trComp[e.ColumnIndex, 0].Value));
                    inp.ShowDialog();
                    trComp[e.ColumnIndex, e.RowIndex].Value = inp.velocity;
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
        public void SetInstruments(string p)
        {
            trInst.Items.Clear();
            for (int i = 0; i < Projekt.sbLength; i++)
                trInst.Items.Add((i + 1) + ". " + Projekt.sb[i].ime);
            trInst.SelectedIndex = GetInstrument(p);
        }
        public void SetInstruments()
        {
            trInst.Items.Clear();
            for (int i = 0; i < Projekt.sbLength; i++)
                trInst.Items.Add((i + 1) + ". " + Projekt.sb[i].ime);
        }
        private void SelectInstrument(object sender, EventArgs e)
        {
            int brojInst = trInst.SelectedIndex;
            int tag = Convert.ToInt32((sender as ComboBox).Tag);
            for (int i = 0; i < Projekt.sbLength; i++)
            {
                if (Projekt.sb[i].note.Length != Projekt.sb[brojInst].note.Length)
                    (sender as ComboBox).Items.Remove(i + ". " + Projekt.sb[i].ime);
            }
            trComp.Enabled = true;
            if (Projekt.sb[brojInst].note.Length == 1)
                SetupTrComp(0, 16, 1);
        }
        private int GetInstrument(string p)
        {
            for (int i = 0; i < trInst.Items.Count; i++)
            {
                string temp = trInst.Items[i].ToString();
                temp = temp.Substring(temp.IndexOf(" ") + 1);
                if (temp == p)
                    return i;
            }
            throw new System.ArgumentException("MISSING INSTRUMENT", "combobox instrument");
        }
        private void ValChange(object sender, EventArgs e)
        {
            int tempCol = trComp.ColumnCount;
            if (trLng.Value > tempCol)
            {
                while (trLng.Value % 4 != 0)
                    trLng.Value += 1;
                SetupTrComp(tempCol, Convert.ToInt32(trLng.Value), trComp.RowCount);
            }
            else if (trLng.Value < tempCol)
            {
                while (trLng.Value % 4 != 0)
                    trLng.Value -= 1;
                trComp.ColumnCount = Convert.ToInt32(trLng.Value);
            }
        }
        public byte[,] Generate()
        {
            byte[,] t = new byte[trComp.RowCount, trComp.ColumnCount];
            for (int i = 0; i < trComp.RowCount; i++)
            {
                for (int j = 0; j < trComp.ColumnCount; j++)
                    t[i, j] = Convert.ToByte(trComp[j, i].Value);
            }
            return t;
        }
        public void LoadIn(byte[,] p)
        {
            trComp.ColumnCount = p.GetLength(1);
            for (int i = 0; i < p.GetLength(1); i++)
            {
                for (int j = 0; j < trComp.RowCount; j++)
                {
                    trComp[i, j].Value = p[j, i];
                    AssignColor(i, j);
                }
            }
        }
    }
}
