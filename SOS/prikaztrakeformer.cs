        public ComboBox trInst;
        public DataGridView trComp;
        public NumericUpDown trLng;
        public PrikazTrake(int height, int formWidth, int p)
        {
            trInst = new ComboBox();
            trInst.Top = 12 + p * (32 + height);
            trInst.Left = 12;
            trInst.Font = new Font("Arial", 32f);
            trInst.Width = 256;
            trInst.Tag = p;
            trInst.DropDownStyle = ComboBoxStyle.DropDownList;
            trInst.Enabled = true;

            trComp = new DataGridView();
            trComp.Top = trInst.Top;
            trComp.Left = trInst.Right + 12;
            trComp.Width = formWidth - trComp.Left - formWidth / 3;
            trComp.Height = trInst.Height;
            trComp.Tag = p;
            trComp.ColumnHeadersVisible = false;
            trComp.RowHeadersVisible = false;
            trComp.ReadOnly = true;
            trComp.AllowUserToResizeColumns = false;
            trComp.AllowUserToResizeRows = false;
            trComp.AllowUserToAddRows = false;
            trComp.AllowUserToDeleteRows = false;
            trComp.ScrollBars = ScrollBars.Horizontal;
            trComp.Enabled = true;

            trLng = new NumericUpDown();
            trLng.Top = trInst.Top;
            trLng.Left = trComp.Right + 12;
            trLng.Font = new Font("Arial", 32f);
            trLng.Tag = p;
            trLng.Minimum = 16;
            trLng.Maximum = decimal.MaxValue;
            trLng.Enabled = false;

            trComp.CellMouseMove += BeatTransform;
            trLng.ValueChanged += ValChange;
        }
        public void SetupTrComp(int str, int n)
        {
            trComp.RowCount = 1;
            trComp.Rows[0].Height = trComp.Height;
            trComp.ColumnCount = n;
            for (int i = str; i < n; i++)
            {
                trComp.Columns[i].Width = 25;
                trComp[i, 0].Value = 0;
                AssignColor(i);
            }
        }
        private void BeatTransform(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1 && e.Button != MouseButtons.None)
            {
                if (e.Button == MouseButtons.Left && trComp[e.ColumnIndex, 0].Style.BackColor != Color.Gold)
                    trComp[e.ColumnIndex, 0].Value = 64;
                else if (e.Button == MouseButtons.Right)
                    trComp[e.ColumnIndex, 0].Value = 0;
                else if (e.Button == MouseButtons.Middle)
                {
                    InputVelocityForm inp = new InputVelocityForm(Convert.ToInt32(trComp[e.ColumnIndex, 0].Value));
                    inp.ShowDialog();
                    trComp[e.ColumnIndex, 0].Value = inp.velocity;
                }
                AssignColor(e.ColumnIndex);
            }
        }
        private void ChangeColor(Color t, int loc)
        {
            trComp[loc, 0].Style.BackColor = t;
            trComp[loc, 0].Style.SelectionBackColor = t;
            trComp[loc, 0].Style.ForeColor = t;
            trComp[loc, 0].Style.SelectionForeColor = t;
        }
        private void AssignColor(int loc)
        {
            if (Convert.ToInt32(trComp[loc, 0].Value) != 0)
                ChangeColor(Color.Gold, loc);
            else
                ChangeColor(Color.Gray, loc);
        }
        private void ValChange(object sender, EventArgs e)
        {
            int tempCol = trComp.ColumnCount;
            if (trLng.Value > tempCol)
            {
                while (trLng.Value % 4 != 0)
                    trLng.Value += 1;
                SetupTrComp(tempCol, Convert.ToInt32(trLng.Value));
            }
            else if (trLng.Value < tempCol)
            {
                while (trLng.Value % 4 != 0)
                    trLng.Value -= 1;
                trComp.ColumnCount = Convert.ToInt32(trLng.Value);
            }
        }
        public byte[] Generate(DataGridView dgv)
        {
            byte[] t = new Byte[dgv.ColumnCount];
            for (int i = 0; i < dgv.ColumnCount; i++)
                t[i] = Convert.ToByte(dgv[i, 0].Value);
            return t;
        }
        public void LoadIn(Byte[,] p)
        {
            for (int i = 0; i < p.Length; i++)
            {
                trComp[i, 0].Value = p[0, i];
                AssignColor(i);
            }
        }