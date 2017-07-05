namespace SOS
{
    partial class SoundbankAdjuster
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
            this.lb = new System.Windows.Forms.Label();
            this.nud = new System.Windows.Forms.NumericUpDown();
            this.cb = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.nud)).BeginInit();
            this.SuspendLayout();
            // 
            // lb
            // 
            this.lb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb.AutoSize = true;
            this.lb.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb.Location = new System.Drawing.Point(12, 9);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(107, 24);
            this.lb.TabIndex = 0;
            this.lb.Text = "Instrument";
            // 
            // nud
            // 
            this.nud.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nud.Location = new System.Drawing.Point(125, 11);
            this.nud.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.nud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud.Name = "nud";
            this.nud.Size = new System.Drawing.Size(70, 20);
            this.nud.TabIndex = 1;
            this.nud.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // cb
            // 
            this.cb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb.FormattingEnabled = true;
            this.cb.Location = new System.Drawing.Point(12, 37);
            this.cb.Name = "cb";
            this.cb.Size = new System.Drawing.Size(183, 21);
            this.cb.TabIndex = 2;
            this.cb.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // SoundbankAdjuster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(207, 70);
            this.Controls.Add(this.cb);
            this.Controls.Add(this.nud);
            this.Controls.Add(this.lb);
            this.Name = "SoundbankAdjuster";
            this.Text = "Instruments";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SoundbankAdjuster_FormClosing);
            this.Load += new System.EventHandler(this.SoundbankAdjuster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.NumericUpDown nud;
        private System.Windows.Forms.ComboBox cb;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}