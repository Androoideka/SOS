using System.Windows.Forms;

namespace SOS
{
    class Projekt
    {
        Soundbank[] sb = new Soundbank[128];
        int sbLength;
        Track[] tr;
        Timer tmr;
        public Projekt()
        {
            tr = new Track[16];
            sb = new Soundbank[128];
            tmr = new Timer();
            tmr.Interval = 500;
        }
        public void Import(string fileLoc)
        {
        }
        public void Export(string fileLoc)
        {
        }
        public string[] FindInstruments(string sbb)
        {
            Soundbank sdb = FindInstrument(sbb);
            string[] st = new string[128];
            if (sdb == null)
            {
                for (int i = 0; i < sbLength; i++)
                {
                    st[i] = sb[i].ime;
                }
            }
            else
            {
                int j = 0;
                for (int i = 0; i < sbLength; i++)
                {
                    if (sb[i].n == sdb.note.Length)
                    {
                        st[j] = sb[i].ime;
                        j++;
                    }
                }
            }
            return st;
        }
        private Soundbank FindInstrument(string name)
        {
            for (int i = 0; i < sbLength; i++)
            {
                if (sb[i].ime == name)
                    return sb[i];
            }
            return sb[0];
        }
        public void InitTrk(int p, string sdb)
        {
            tr[p] = new Track(FindInstruments(sdb));
        }
        public bool Perc(int trN)
        {
            return tr[trN].instrument.percussion;
        }
    }
}
