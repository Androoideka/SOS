using System.Windows.Media;

namespace SOS
{
    class Soundbank
    {
        public bool percussion;
        public string ime;
        public MediaPlayer[] note; 
        public int n;
        public Soundbank(string p, bool yes)
        {
            percussion = yes;
            ime = p;
            if (percussion)
            {
                note = new MediaPlayer[1];
                note[0] = new MediaPlayer();
            }
            else
            {
                note = new MediaPlayer[n];
                for (int i = 0; i < n; i++)
                    note[i] = new MediaPlayer();
            }
        }
    }
}
