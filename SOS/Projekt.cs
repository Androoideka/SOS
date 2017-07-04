using System;
using System.Windows.Forms;

namespace SOS
{
    class Projekt
    {
        Timer tmr;
        public Track[] tr;
        public Soundbank[] sb = new Soundbank[128];
        public int sbLength;
        public Projekt()
        {
            tr = new Track[16];
            sb = new Soundbank[128];
            tmr = new Timer();
            tmr.Interval = 125;
            tmr.Tick += tmrTick;
            sb[0] = new Soundbank("Drum", true);
            sbLength++;
        }
        private void tmrTick(object sender, EventArgs e)
        {

        }
    }
}
