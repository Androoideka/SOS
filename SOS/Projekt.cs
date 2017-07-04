using System;
using System.Windows.Forms;

namespace SOS
{
    class Projekt
    {
        public Timer tmr;
        public Track[] tr;
        public static Soundbank[] sb;
        int[] count, eventNum;
        public Projekt()
        {
            tr = new Track[16];
            tmr = new Timer();
            tmr.Interval = 500;
            tmr.Tick += TmrTick;
            count = new int[16];
            eventNum = new int[16];
            CreateAllInstruments();
        }
        private void TmrTick(object sender, EventArgs e)
        {
            int j = 0;
            for (int i = 0; i < 16; i++)
            {
                if (tr[i] != null && !tr[i].ended)
                    tr[i].Play();
                else
                    j++;
            }
            if (j == 16)
                tmr.Enabled = false;
        }
        public static void CreateAllInstruments()
        {
            sb = new Soundbank[128];
            for (int i = 0; i < 128; i++)
                sb[i] = new Soundbank("WDS", new string[] { @"C:\Windows\Media\tada.wav"});
        }
        public int GetTempo()
        {
            return 60000 / tmr.Interval / 4;
        }
        public void SetTempo(int bpm)
        {
            tmr.Interval = 60000 / bpm / 4;
        }
    }
}
