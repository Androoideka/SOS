using System.Media;

namespace SOS
{
    class Soundbank
    {
        public bool percussion;
        public int n;
        public string ime;
        public SoundPlayer[] note;
        public Soundbank(string p, bool yes)
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
                note = new SoundPlayer[n];
                for (int i = 0; i < n; i++)
                    note[i] = new SoundPlayer(ime + i + "sample.wav");
            }
        }
    }
}
