using System.Windows.Media;

namespace SOS
{
    public class Soundbank
    {
        public string ime;
        public MediaPlayer[] note; 
        public Soundbank(string p, string[] files)
        {
            ime = p;
            note = new MediaPlayer[files.Length];
            for (int i = 0; i < note.Length; i++)
            {
                note[i] = new MediaPlayer();
                //will probably import managed directx and use that
                //note[i].Open(new System.Uri(files[i]));
            }
        }
    }
}
