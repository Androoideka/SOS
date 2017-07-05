using NAudio.Wave;

namespace SOS
{
    public class Soundbank
    {
        public string ime { get; }
        private string[] note;
        public Soundbank(string p)
        {
            ime = p;
            string[] files = System.IO.Directory.GetFiles(ime);
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
        public AudioFileReader CreateSound(int i, byte vel)
        {
            return new AudioFileReader(note[i]) { Volume = 1f / 127f * vel };
        }
    }
}