using NAudio.Wave;
using System.Collections.Generic;
using System.IO;

namespace SOS
{
    public class Projekt
    {
        private double intervale;
        private int completedTracks, offset;
        private WaveMixerStream32 mix;
        public Track[] tr;
        public int trLength;
        public static Soundbank[] sb;
        public Projekt()
        {
            tr = new Track[16];
            intervale = 125;
        }
        /// <summary>
        /// Form full project
        /// </summary>
        private void FormMix()
        {
            offset = -1;
            completedTracks = 0;
            mix = new WaveMixerStream32();
            for (int i = 0; i < trLength; i++)
                tr[i].ResetTrackPosition();
            while (completedTracks < trLength)
                CombineTrackMixers();
        }
        /// <summary>
        /// Generate all sounds for moment in time
        /// </summary>
        private void CombineTrackMixers()
        {
            completedTracks = 0;
            List<WaveStream> mixedSoundsInPos = new List<WaveStream>();
            for (int i = 0; i < trLength; i++)
            {
                List<WaveStream> mixedSoundsInTrackPos = tr[i].Read();
                if (mixedSoundsInTrackPos != null && mixedSoundsInTrackPos.Count > 0)
                {
                    mixedSoundsInPos.AddRange(mixedSoundsInTrackPos);
                    offset = tr[i].GetOffset();
                }
                else if (mixedSoundsInTrackPos == null) completedTracks++;
            }
            if (t.Count > 0)
            if (mixedSoundsInPos.Count > 0)
            {
                PrototypeWaveOffsetStream32 stream = new PrototypeWaveOffsetStream32(new WaveMixerStream32(mixedSoundsInPos, true), System.TimeSpan.FromMilliseconds(intervale * offset));
                mix.AddInputStream(stream);
            }
        }
        /// <summary>
        /// Save track after modifying in data grid
        /// </summary>
        /// <param name="i">Index of track modified</param>
        /// <param name="p">Data from data grid</param>
        /// <param name="n">Length of track</param>
        public void SaveToTrack(int i, byte[,] p, int n)
        {
            tr[i].ImportPattern(p, n);
            FormMix();
        }
        /// <summary>
        /// Overwrite track on given index with other tracks in array
        /// </summary>
        /// <param name="i">Track to delete</param>
        public void DeleteTrack(int i)
        {
            for (int j = i + 1; j < trLength; j++)
                tr[j - 1] = tr[j];
            trLength--;
            FormMix();
        }
        /// <summary>
        /// Get index of menu button corresponding to tempo number
        /// </summary>
        /// <returns>Index of menu button corresponding to tempo</returns>
        public int GetTempo()
        {
            double tempo = 60000d / intervale / 4d;
            return tempo > 200 ? 0 : (tempo > 168 ? 1 : (tempo > 120 ? 2 : (tempo > 108 ? 3 : (tempo > 76 ? 4 : (tempo > 66 ? 5 : (tempo > 60 ? 6 : 7))))));
        }
        /// <summary>
        /// Set amount of time between start of 2 notes
        /// </summary>
        /// <param name="bpm"></param>
        public void SetTempo(int bpm)
        {
            bpm = bpm == 0 ? 208 : (bpm == 1 ? 200 : (bpm == 2 ? 168 : (bpm == 3 ? 120 : (bpm == 4 ? 108 : (bpm == 5 ? 76 : (bpm == 6 ? 66 : 40))))));
            intervale = 60000d / bpm / 4d;
        }
        /// <summary>
        /// Plays everything in project until end
        /// </summary>
        public void Play()
        {
            AudioPlaybackEngine.Instance.Play(mix, intervale * offset);
        }
        /// <summary>
        /// Saves project to sound file
        /// </summary>
        /// <param name="p"></param>
        public void Save(string p)
        {
            WaveFileWriter.CreateWaveFile(p, mix);
        }
        /// <summary>
        /// Summon form for setting soundbanks for different instruments
        /// </summary>
        public static void SetSoundbanks()
        {
            LoadInstruments();
            SoundbankAdjuster sa = new SoundbankAdjuster();
            sa.ShowDialog();
        }
        /// <summary>
        /// Load instrument settings from text file if exists or put default sound
        /// </summary>
        private static void LoadInstruments()
        {
            if (Projekt.sb == null)
            {
                sb = new Soundbank[128];
                if (File.Exists(@"instruments.ini"))
                {
                    StreamReader sr = new StreamReader(@"instruments.ini");
                    int i = 0;
                    while (!sr.EndOfStream)
                    {
                        string instrumentFl = sr.ReadLine();
                        if (Directory.Exists(instrumentFl))
                            sb[i] = new Soundbank(instrumentFl, Directory.GetFiles(instrumentFl));
                        else
                            throw new DirectoryNotFoundException(@"Cannot find directory mentioned in instruments.ini");
                        i++;
                    }
                }
                else
                {
                    for (int i = 0; i < 128; i++)
                        sb[i] = new Soundbank("WDS", new string[] { @"C:\Windows\Media\tada.wav" });
                }
            }
        }
        /// <summary>
        /// Save set instruments for project to text file
        /// </summary>
        /// <param name="dest">Where to save text file</param>
        public static void SaveInstruments(string dest)
        {
            StreamWriter sw = new StreamWriter(dest, false);
            for (int i = 0; i < sb.Length; i++)
                sw.WriteLine(sb[i].ime);
        }
        /// <summary>
        /// Gets soundbank index of instrument with given name
        /// </summary>
        /// <param name="name">Given name</param>
        /// <returns>Soundbank index</returns>
        public static int GetInstrumentWithName(string name)
        {
            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i].ime == name)
                    return i;
            }
            throw new System.Exception("Not found");
        }
    }
}