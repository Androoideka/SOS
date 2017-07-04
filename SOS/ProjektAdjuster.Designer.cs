namespace SOS
{
    partial class ProjektAdjuster
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
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Import Soundbank";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ProjektAdjuster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(189, 266);
            this.Controls.Add(this.button1);
            this.Name = "ProjektAdjuster";
            this.Text = "ProjektAdjuster";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProjektAdjuster_FormClosing);
            this.Load += new System.EventHandler(this.ProjektAdjuster_Load);
            this.ResumeLayout(false);

            lb[0] = new System.Windows.Forms.Label();
            lb[0].Top = button1.Bottom;
            lb[0].Left = button1.Left;
            lb[0].Text = "Instrument 1";
            Controls.Add(lb[0]);
            cb[0] = new System.Windows.Forms.ComboBox();
            cb[0].Top = button1.Bottom + 12;
            cb[0].Left = button1.Left;
            cb[0].Width = ClientRectangle.Width - (2 * cb[0].Left);
            cb[0].AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            cb[0].AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            Controls.Add(cb[0]);
            cb[0].BringToFront();
            for (int i = 1; i < 128; i++)
            {
                lb[i] = new System.Windows.Forms.Label();
                lb[i].Top = cb[i - 1].Bottom;
                lb[i].Left = lb[i - 1].Left;
                lb[i].Text = "Instrument " + (i + 1);
                Controls.Add(lb[i]);
                cb[i] = new System.Windows.Forms.ComboBox();
                cb[i].Top = cb[i - 1].Bottom + 12;
                cb[i].Left = cb[i - 1].Left;
                cb[i].Width = cb[i - 1].Width;
                cb[i].AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                cb[i].AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
                Controls.Add(cb[i]);
                cb[i].BringToFront();
            }

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label[] lb = new System.Windows.Forms.Label[128];
        private System.Windows.Forms.ComboBox[] cb = new System.Windows.Forms.ComboBox[128];
    }
}