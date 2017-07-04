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
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tempoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playStopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundbanksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.soundbanksToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tempoToolStripMenuItem,
            this.playStopToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // tempoToolStripMenuItem
            // 
            this.tempoToolStripMenuItem.Name = "tempoToolStripMenuItem";
            this.tempoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tempoToolStripMenuItem.Text = "Tempo";
            // 
            // playStopToolStripMenuItem
            // 
            this.playStopToolStripMenuItem.Name = "playStopToolStripMenuItem";
            this.playStopToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.playStopToolStripMenuItem.Text = "Play/Stop";
            this.playStopToolStripMenuItem.Click += new System.EventHandler(this.playStopToolStripMenuItem_Click);
            // 
            // soundbanksToolStripMenuItem
            // 
            this.soundbanksToolStripMenuItem.Name = "soundbanksToolStripMenuItem";
            this.soundbanksToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.soundbanksToolStripMenuItem.Text = "Soundbanks";
            // 
            // PianoRoll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 731);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PianoRoll";
            this.Text = "PianoRoll";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PianoRoll_FormClosing);
            this.Load += new System.EventHandler(this.PianoRoll_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playStopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem soundbanksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tempoToolStripMenuItem;
    }
}