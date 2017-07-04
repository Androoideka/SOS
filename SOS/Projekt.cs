using System;
using System.Windows.Forms;

namespace SOS
{
    class Projekt
    {
        public Timer tmr;
        public Track[] tr;
        public Soundbank[] sb;
        public int sbLength;
        int[] count, eventNum;
        public Projekt()
        {
            tr = new Track[16];
            sb = new Soundbank[1024];
            sb[0] = new Soundbank("Drum", new string[] { @"C:\Users\andro\Desktop\Piano\1.wav" });
            sb[1] = new Soundbank("Drum", new string[] { @"C:\Users\andro\Desktop\Piano\1.wav", @"C:\Users\andro\Desktop\Piano\1.wav" });
            sbLength +=2;
            tmr = new Timer();
            tmr.Interval = 125;
            tmr.Tick += tmrTick;
            count = new int[16];
            eventNum = new int[16];
        }
        private void tmrTick(object sender, EventArgs e)
        {
            for (int i = 0; i < 16; i++)
            {
                if (tr[i] != null)
                {
                    while (tr[i].e[eventNum[i]].note != 255 && count[i] == tr[i].e[eventNum[i]].getDT(tmr.Interval))
                    {
                        tr[i].instrument.note[tr[i].e[eventNum[i]].note].Volume = tr[i].e[eventNum[i]].getVolume();
                        if (tr[i].instrument.note[tr[i].e[eventNum[i]].note].Volume != 0)
                            tr[i].instrument.note[tr[i].e[eventNum[i]].note].Play();
                        else
                            tr[i].instrument.note[tr[i].e[eventNum[i]].note].Stop();
                        eventNum[i]++;
                        count[i] = 0;
                    }
                    count[i] += tmr.Interval;
                }
            }
        }
    }
}
