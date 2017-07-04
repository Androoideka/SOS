using System;
using System.Windows.Forms;

namespace SOS
{
    class Projekt
    {
        Timer tmr;
        public Track[] tr;
        public static Soundbank[] sb;
        int[] count, eventNum;
        public Projekt()
        {
            tr = new Track[16];
            tmr = new Timer();
            tmr.Interval = 125;
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
                if (tr[i] == null || tr[i].Play())
                    j++;
                if (j == 16)
                    tmr.Enabled = false;
            }
        }
        public static void CreateAllInstruments()
        {
            sb = new Soundbank[128];
            for (int i = 0; i < 128; i++)
                sb[i] = new Soundbank("Drum", new string[] { @"C:\Users\andro\Desktop\BDSOM Experimental\BDSOM\bin\Debug\Drum\t.wav" });
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
