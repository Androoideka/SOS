using NAudio.Wave;
using System.Collections.Generic;

namespace SOS
{
    public class Projekt
    {
        private double intervale;
        private int j;
        private WaveMixerStream32 mix;
        public Track[] tr;
        public int trLength;
        public static Soundbank[] sb;
        public Projekt()
        {
            tr = new Track[16];
            intervale = 125;
        }
        private void FormMix()
        {
            mix = new WaveMixerStream32();
            for (int i = 0; i < trLength; i++)
                tr[i].ResetTrackPosition();
            while (j < trLength)
                CombineTrackMixers();
        }
        private void CombineTrackMixers()
        {
            j = 0;
            int offset = -1;
            List<WaveStream> t = new List<WaveStream>();
            for (int i = 0; i < trLength; i++)
            {
                if (tr[i].Prepare())
                {
                    List<WaveStream> k = tr[i].GenerateMix();
                    t.AddRange(k);
                    if (k.Count > 0 && offset == -1)
                        offset = tr[i].GetOffset();
                }
                else j++;
            }
            if (offset != -1)
            {
                PrototypeWaveOffsetStream32 stream = new PrototypeWaveOffsetStream32(new WaveMixerStream32(t, true), System.TimeSpan.FromMilliseconds(intervale * offset));
                mix.AddInputStream(stream);
            }
        }
        public static void SetSoundbanks()
        {
            SoundbankAdjuster sa = new SoundbankAdjuster();
            sa.ShowDialog();
        }
        public static void CreateAllInstruments()
        {
            sb = new Soundbank[128];
            for (int i = 0; i < 128; i++)
                sb[i] = new Soundbank("WDS", new string[] { @"C:\Windows\Media\tada.wav" });
        }
        public int GetTempo()
        {
            double tempo = 60000d / intervale / 4d;
            return tempo > 200 ? 0 : (tempo > 168 ? 1 : (tempo > 120 ? 2 : (tempo > 108 ? 3 : (tempo > 76 ? 4 : (tempo > 66 ? 5 : (tempo > 60 ? 6 : 7))))));
        }
        public void SaveToTrack(int i, byte[,] p, int n)
        {
            tr[i].ImportPattern(p, n);
        }
        public void DeleteTrack(int i)
        {
            for (int j = i + 1; j < trLength; j++)
                tr[j - 1] = tr[j];
            trLength--;
        }
        public void SetTempo(int p)
        {
            int bpm = p == 0 ? 208 : (p == 1 ? 200 : (p == 2 ? 168 : (p == 3 ? 120 : (p == 4 ? 108 : (p == 5 ? 76 : (p == 6 ? 66 : 40))))));
            intervale = 60000d / bpm / 4d;
        }
        public void Play()
        {
            FormMix();
            AudioPlaybackEngine.Instance.Play(mix);
        }
        public void Save(string p)
        {
            FormMix();
            WaveFileWriter.CreateWaveFile(p, mix);
        }
    }
}