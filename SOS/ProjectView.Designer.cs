namespace SOS
{
    partial class ProjectView
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
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instrumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tempoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prestissimoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prestoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allegroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moderatoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.andanteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adagioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.larghettoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(450, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.instrumentsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
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
            this.deleteToolStripMenuItem,
            this.tempoToolStripMenuItem,
            this.playToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // tempoToolStripMenuItem
            // 
            this.tempoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prestissimoToolStripMenuItem,
            this.prestoToolStripMenuItem,
            this.allegroToolStripMenuItem,
            this.moderatoToolStripMenuItem,
            this.andanteToolStripMenuItem,
            this.adagioToolStripMenuItem,
            this.larghettoToolStripMenuItem,
            this.largoToolStripMenuItem});
            this.tempoToolStripMenuItem.Name = "tempoToolStripMenuItem";
            this.tempoToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.tempoToolStripMenuItem.Text = "Tempo";
            // 
            // prestissimoToolStripMenuItem
            // 
            this.prestissimoToolStripMenuItem.Name = "prestissimoToolStripMenuItem";
            this.prestissimoToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.prestissimoToolStripMenuItem.Tag = "0";
            this.prestissimoToolStripMenuItem.Text = "Prestissimo";
            this.prestissimoToolStripMenuItem.Click += new System.EventHandler(this.tempoToolStripMenuItem_Click);
            // 
            // prestoToolStripMenuItem
            // 
            this.prestoToolStripMenuItem.Name = "prestoToolStripMenuItem";
            this.prestoToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.prestoToolStripMenuItem.Tag = "1";
            this.prestoToolStripMenuItem.Text = "Presto";
            this.prestoToolStripMenuItem.Click += new System.EventHandler(this.tempoToolStripMenuItem_Click);
            // 
            // allegroToolStripMenuItem
            // 
            this.allegroToolStripMenuItem.Name = "allegroToolStripMenuItem";
            this.allegroToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.allegroToolStripMenuItem.Tag = "2";
            this.allegroToolStripMenuItem.Text = "Allegro";
            this.allegroToolStripMenuItem.Click += new System.EventHandler(this.tempoToolStripMenuItem_Click);
            // 
            // moderatoToolStripMenuItem
            // 
            this.moderatoToolStripMenuItem.Name = "moderatoToolStripMenuItem";
            this.moderatoToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.moderatoToolStripMenuItem.Tag = "3";
            this.moderatoToolStripMenuItem.Text = "Moderato";
            this.moderatoToolStripMenuItem.Click += new System.EventHandler(this.tempoToolStripMenuItem_Click);
            // 
            // andanteToolStripMenuItem
            // 
            this.andanteToolStripMenuItem.Name = "andanteToolStripMenuItem";
            this.andanteToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.andanteToolStripMenuItem.Tag = "4";
            this.andanteToolStripMenuItem.Text = "Andante";
            this.andanteToolStripMenuItem.Click += new System.EventHandler(this.tempoToolStripMenuItem_Click);
            // 
            // adagioToolStripMenuItem
            // 
            this.adagioToolStripMenuItem.Name = "adagioToolStripMenuItem";
            this.adagioToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.adagioToolStripMenuItem.Tag = "5";
            this.adagioToolStripMenuItem.Text = "Adagio";
            this.adagioToolStripMenuItem.Click += new System.EventHandler(this.tempoToolStripMenuItem_Click);
            // 
            // larghettoToolStripMenuItem
            // 
            this.larghettoToolStripMenuItem.Name = "larghettoToolStripMenuItem";
            this.larghettoToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.larghettoToolStripMenuItem.Tag = "6";
            this.larghettoToolStripMenuItem.Text = "Larghetto";
            this.larghettoToolStripMenuItem.Click += new System.EventHandler(this.tempoToolStripMenuItem_Click);
            // 
            // largoToolStripMenuItem
            // 
            this.largoToolStripMenuItem.Name = "largoToolStripMenuItem";
            this.largoToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.largoToolStripMenuItem.Tag = "7";
            this.largoToolStripMenuItem.Text = "Largo";
            this.largoToolStripMenuItem.Click += new System.EventHandler(this.tempoToolStripMenuItem_Click);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.playToolStripMenuItem.Text = "Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playStopToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(12, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(426, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "wav";
            this.saveFileDialog1.Filter = "IEEE Float WAV|*.wav";
            // 
            // ProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 435);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ProjectView";
            this.Text = "Project View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProjectView_FormClosing);
            this.Load += new System.EventHandler(this.ProjectView_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instrumentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tempoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem prestissimoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prestoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allegroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moderatoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem andanteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adagioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem larghettoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    }
}

