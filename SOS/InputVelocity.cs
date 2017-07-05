using System;
using System.Windows.Forms;

namespace SOS
{
    public partial class InputVelocity : Form
    {
        public byte velocity;
        public InputVelocity(byte curVel)
        {
            velocity = curVel;
            InitializeComponent();
        }
        private void InputVelocity_Load(object sender, EventArgs e)
        {
            MinimumSize = Size;
            MaximumSize = Size;
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            progressBar1.Value = trackBar1.Value;
            velocity = (byte)trackBar1.Value;
        }
        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            Close();
        }
    }
}
