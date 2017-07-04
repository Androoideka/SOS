using System;
using System.Windows.Forms;

namespace SOS
{
    public partial class InputVelocityForm : Form
    {
        public byte velocity;
        public InputVelocityForm(byte curVel)
        {
            velocity = curVel;
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            progressBar1.Value = trackBar1.Value;
            velocity = (byte)trackBar1.Value;
        }
    }
}

