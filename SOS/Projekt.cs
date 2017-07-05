using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Collections.Generic;
using System.Timers;

namespace SOS
{
    public class Projekt
    {
        private Timer tmr;
        private int j;
        private List<ISampleProvider> mix;
        public Track[] tr;
        public int trLength;
        public static Soundbank[] sb;
        bool sev = false;
        string p;
        System.Diagnostics.Stopwatch stp = System.Diagnostics.Stopwatch.StartNew();
        public Projekt()
        {
            tr = new Track[16];
            tmr = new Timer(125);
            tmr.Elapsed += TmrTick;
            mix = new List<ISampleProvider>();
        }
        private void TmrTick(object sender, ElapsedEventArgs e)
        {
            if (mix.Count != 0)
                AudioPlaybackEngine.Instance.mixer.AddMixerInput(new MixingSampleProvider(mix));
            if (j == trLength)
                Reset();
            else
            {
                j = 0;
                CombineTrackMixers();
            }
        }
        private void Reset()
        {
            AudioPlaybackEngine.Instance.mixer.ReadFully = false;
            tmr.Stop();
            if(sev)
                AudioPlaybackEngine.Instance.Save(p);
            j = 0;
            for (int i = 0; i < trLength; i++)
                tr[i].ResetTrackPosition();
            CombineTrackMixers();
        }
        private void CombineTrackMixers()
        {
            mix.Clear();
            for (int i = 0; i < trLength; i++)
            {
                if (tr[i].Prepare())
                    mix.AddRange(tr[i].GenerateMix());
                else j++;
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
            double tempo = 60000d / tmr.Interval / 4d;
            return tempo > 200 ? 0 : (tempo > 168 ? 1 : (tempo > 120 ? 2 : (tempo > 108 ? 3 : (tempo > 76 ? 4 : (tempo > 66 ? 5 : (tempo > 60 ? 6 : 7))))));
        }
        public void SaveToTrack(int i, byte[,] p, int n)
        {
            tr[i].ImportPattern(p, n);
            Reset();
        }
        public void DeleteTrack(int i)
        {
            for (int j = i + 1; j < trLength; j++)
                tr[j - 1] = tr[j];
            trLength--;
            Reset();
        }
        public void SetTempo(int p)
        {
            int bpm = p == 0 ? 208 : (p == 1 ? 200 : (p == 2 ? 168 : (p == 3 ? 120 : (p == 4 ? 108 : (p == 5 ? 76 : (p == 6 ? 66 : 40))))));
            tmr.Interval = 60000d / bpm / 4d;
        }
        public void Play()
        {
            sev = false;
            if (tmr.Enabled)
                Reset();
            AudioPlaybackEngine.Instance.Play();
            tmr.Start();
        }
        public void Save(string p)
        {
            this.p = p;
            Play();
            sev = true;
        }
    }
}