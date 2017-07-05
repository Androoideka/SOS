namespace SOS
{
    partial class PianoRoll
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instrumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.velocityBrushToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trLng = new System.Windows.Forms.NumericUpDown();
            this.paintInst = new System.Windows.Forms.ComboBox();
            this.trInst = new System.Windows.Forms.DataGridView();
            this.trComp = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trLng)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trInst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trComp)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.instrumentsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // instrumentsToolStripMenuItem
            // 
            this.instrumentsToolStripMenuItem.Name = "instrumentsToolStripMenuItem";
            this.instrumentsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.instrumentsToolStripMenuItem.Text = "Instruments";
            this.instrumentsToolStripMenuItem.Click += new System.EventHandler(this.instrumentsToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.velocityBrushToolStripMenuItem,
            this.playToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // velocityBrushToolStripMenuItem
            // 
            this.velocityBrushToolStripMenuItem.Name = "velocityBrushToolStripMenuItem";
            this.velocityBrushToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.velocityBrushToolStripMenuItem.Text = "Velocity Brush";
            this.velocityBrushToolStripMenuItem.Click += new System.EventHandler(this.velocityBrushToolStripMenuItem_Click);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.playToolStripMenuItem.Text = "Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playStopToolStripMenuItem_Click);
            // 
            // trLng
            // 
            this.trLng.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trLng.Font = new System.Drawing.Font("Arial", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trLng.Location = new System.Drawing.Point(12, 27);
            this.trLng.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.trLng.Name = "trLng";
            this.trLng.Size = new System.Drawing.Size(120, 57);
            this.trLng.TabIndex = 1;
            this.trLng.ValueChanged += new System.EventHandler(this.ValChange);
            // 
            // paintInst
            // 
            this.paintInst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paintInst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.paintInst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.paintInst.Font = new System.Drawing.Font("Arial", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paintInst.FormattingEnabled = true;
            this.paintInst.Location = new System.Drawing.Point(139, 27);
            this.paintInst.Name = "paintInst";
            this.paintInst.Size = new System.Drawing.Size(262, 57);
            this.paintInst.TabIndex = 2;
            // 
            // trInst
            // 
            this.trInst.AllowUserToAddRows = false;
            this.trInst.AllowUserToDeleteRows = false;
            this.trInst.AllowUserToResizeColumns = false;
            this.trInst.AllowUserToResizeRows = false;
            this.trInst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trInst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trInst.ColumnHeadersVisible = false;
            this.trInst.Location = new System.Drawing.Point(12, 91);
            this.trInst.Name = "trInst";
            this.trInst.ReadOnly = true;
            this.trInst.RowHeadersVisible = false;
            this.trInst.Size = new System.Drawing.Size(1240, 50);
            this.trInst.TabIndex = 3;
            this.trInst.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.InstrumentPaint);
            // 
            // trComp
            // 
            this.trComp.AllowUserToAddRows = false;
            this.trComp.AllowUserToDeleteRows = false;
            this.trComp.AllowUserToResizeColumns = false;
            this.trComp.AllowUserToResizeRows = false;
            this.trComp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trComp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trComp.ColumnHeadersVisible = false;
            this.trComp.Location = new System.Drawing.Point(12, 147);
            this.trComp.MultiSelect = false;
            this.trComp.Name = "trComp";
            this.trComp.ReadOnly = true;
            this.trComp.RowHeadersVisible = false;
            this.trComp.Size = new System.Drawing.Size(1240, 826);
            this.trComp.TabIndex = 4;
            this.trComp.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.trComp_CellMouseDown);
            this.trComp.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.trComp_CellMouseMove);
            // 
            // PianoRoll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Controls.Add(this.trComp);
            this.Controls.Add(this.trInst);
            this.Controls.Add(this.paintInst);
            this.Controls.Add(this.trLng);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PianoRoll";
            this.Text = "Piano Roll";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PianoRoll_FormClosing);
            this.Load += new System.EventHandler(this.PianoRoll_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trLng)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trInst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trComp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instrumentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem velocityBrushToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown trLng;
        private System.Windows.Forms.ComboBox paintInst;
        private System.Windows.Forms.DataGridView trInst;
        private System.Windows.Forms.DataGridView trComp;
    }
}