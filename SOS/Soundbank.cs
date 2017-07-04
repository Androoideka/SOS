namespace SOS
{
    class Soundbank
    {
        public string ime;
        public string[] note;
        public Soundbank(string p, string[] files)
        {
            ime = p;
            if (files.Length == 128)
                note = files;
            else if (files.Length < 128)
            {
                note = new string[128];
                int j = 0;
                for (int i = 0; i < files.Length; i++)
                    note[j] = files[j];
                for (int i = files.Length; i < 128; i++)
                {
                    note[i] = files[j];
                    j++;
                    if (j >= files.Length)
                        j = 0;
                }
            }
            else
                throw new System.ArgumentOutOfRangeException("files", "HOW THE FUCK DID YOU MAKE THIS HAPPEN?????");
        }
    }
}
