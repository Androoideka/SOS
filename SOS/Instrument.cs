using System.Media;

namespace SOS
{
    class Instrument
    {
        public bool percussion;
        public string ime;
        public SoundPlayer[] note;
        public Instrument(string p, bool yes)
        {
            percussion = yes;
            ime = p;
            if (percussion)
            {
                note = new SoundPlayer[1];
                note[0] = new SoundPlayer(ime + "sample.wav");
            }
            else
            {
                note = new SoundPlayer[128];
                for (int i = 0; i < 128; i++)
                    note[0] = new SoundPlayer(ime + i + "sample.wav");
            }
        }
    }
}
