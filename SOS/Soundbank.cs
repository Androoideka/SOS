namespace SOS
{
    public class Soundbank
    {
        public string ime;
        public string[] note;
        public Soundbank(string p, string[] files)
        {
            ime = p;
            if (files.Length == 128)
                note = files;
            else
            {
                note = new string[128];
                int j = 0;
                for (int i = 0; i < 128; i++)
                {
                    note[i] = files[j];
                    j++;
                    if (j >= files.Length)
                        j = 0;
                }
            }
        }
    }
}