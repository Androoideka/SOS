﻿using System;
using System.Threading.Tasks;
using System.Timers;

namespace SOS
{
    public class Projekt
    {
        private System.Timers.Timer tmr;
        public Track[] tr;
        public static Soundbank[] sb;
        public Projekt()
        {
            tr = new Track[16];
            tmr = new System.Timers.Timer(125);
            tmr.Elapsed += TmrTick;
        }
        private void TmrTick(object sender, EventArgs e)
        {
            int j = 0;
            for (int i = 0; i < 16; i++)
            {
                if (tr[i] != null && !tr[i].ended)
                    Parallel.Invoke(tr[i].Play);
                else
                    j++;
            }
            if (j == 16)
                tmr.Stop();
        }
        public void Reset()
        {
            for (int i = 0; i < 16; i++)
            {
                if (tr[i] != null)
                    tr[i].ResetTrackPosition();
            }
            tmr.Start();
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
        }
        public void DeleteTrack(int i)
        {
            for (int j = i + 1; j < 16; j++)
                tr[j - 1] = tr[j];
        }
        public void SetTempo(int p)
        {
            int bpm = p == 0 ? 208 : (p == 1 ? 200 : (p == 2 ? 168 : (p == 3 ? 120 : (p == 4 ? 108 : (p == 5 ? 76 : (p == 6 ? 66 : 40))))));
            tmr.Interval = 60000d / bpm / 4d;
        }
    }
}