using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Collections.Generic;
using System.IO;

/*
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
*/

namespace SOS
{
    public class Projekt
    {
        private int ticks;
        private int trAmount;
        public Track[] tr { get; set; }
        public static Soundbank[] sb { get; set; }
        private Soundbank[] ch { get; set; }
        public Projekt()
        {
            ch = new Soundbank[16];
        }
        /// <summary>
        /// Form full project
        /// </summary>
        /// Needs professional fixing pls 
        /*private WaveMixerStream32 FormMix()
        {
            MixingSampleProvider mix = new MixingSampleProvider();
            for (int i = 0; i < tr.Length; i++)
                tr[i].ResetTrackPosition();
            bool t = true;
            while (t)
            {
                List<ISampleProvider> stream = CombineTrackMixers();
                if (stream != null && stream.Count != 0)
                    mix.AddMixerInput(stream);
                else if (stream == null)
                    t = false;
            }
            return mix;
        }
        /// <summary>
        /// Generate all sounds for moment in time
        /// </summary>
        private List<ISampleProvider> CombineTrackMixers()
        {
            int completedTracks = 0;
            List<ISampleProvider> mixedSoundsInPos = new List<ISampleProvider>();
            for (int i = 0; i < tr.Length; i++)
            {
                List<ISampleProvider> mixedSoundsInTrackPos = tr[i].Read();
                if (mixedSoundsInTrackPos != null && mixedSoundsInTrackPos.Count > 0)
                    mixedSoundsInPos.AddRange(mixedSoundsInTrackPos);
                else if (mixedSoundsInTrackPos == null) completedTracks++;
            }
            return mixedSoundsInPos;
        }*/
        /// <summary>
        /// Save track after modifying in data grid
        /// </summary>
        /// <param name="i">Index of track modified</param>
        /// <param name="p">Data from data grid</param>
        /// <param name="n">Length of track</param>
        public void SaveToTrack(int i, byte[,] p, int n)
        {
            tr[i].ImportPattern(p, n);
        }
        /// <summary>
        /// Overwrite track on given index with other tracks in array
        /// </summary>
        /// <param name="i">Track to delete</param>
        public void DeleteTrack(int i)
        {
            for (int j = i + 1; j < trAmount; j++)
                tr[j - 1] = tr[j];
            trAmount--;
        }
        /// <summary>
        /// Plays everything in project until end
        /// </summary>
        public void Play()
        {
            throw new System.NotImplementedException();
            //fix length
            //AudioPlaybackEngine.Instance.Play(FormMix(), intervale);
        }
        /// <summary>
        /// Saves project to sound file
        /// </summary>
        /// <param name="p"></param>
        public void Save(string p)
        {
            throw new System.NotImplementedException();
            //SaveInstruments(Path.Combine(Path.GetDirectoryName(p), "instruments.ini"));
            //WaveFileWriter.CreateWaveFile(p, FormMix());
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
                            sb[i] = new Soundbank(instrumentFl);
                        else
                            throw new DirectoryNotFoundException(@"Cannot find directory mentioned in instruments.ini");
                        i++;
                    }
                }
                else
                {
                    for (int i = 0; i < 128; i++)
                        sb[i] = new Soundbank("HMDK");
                }
            }
        }
        /// <summary>
        /// Save set instruments for project to text file
        /// </summary>
        /// <param name="dest">Where to save text file</param>
        private void SaveInstruments(string dest)
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
        /// Section for reading MIDI Files
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="i"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private int ReadChunk(byte[] file, ref int i, int val)
        {
            val *= 10 + file[i];
            i++;
            return val;
        }
        private int ReadNChunks(byte[] file, ref int i, int length)
        {
            int val = 0;
            for (int j = 0; j < length; j++)
                val = ReadChunk(file, ref i, val);
            return val;
        }
        private void ReadMIDIFile(string loc)
        {
            int i = 0;
            byte[] file = File.ReadAllBytes(loc);
            int[] h = ReadHeader(file, ref i);
            if (i > file.Length || h == null)
                throw new System.Exception("FAILED TO READ FILE");
            if (h[0] == 0)
            {
                tr[trAmount] = new Track(file, ref i);
                trAmount++;
            }
            else if (h[0] == 1)
            {
                trAmount = h[1];
                for (int j = 0; j < trAmount; j++)
                    tr[j] = new Track(file, ref i);
            }
            else
                throw new System.Exception("FAILED TO READ FILE");
        }
        private int[] ReadHeader(byte[] file, ref int i)
        {
            if (!(file[i] == 77 && file[i + 1] == 84 && file[i + 2] == 104 && file[i + 3] == 100))
                return null;
            i += 4;
            int chunkLength = ReadNChunks(file, ref i, 4);
            int[] h = new int[3];
            //First format, then tracks, then ticks
            for (int j = 0; j < h.Length; j++)
                h[j] = ReadNChunks(file, ref i, 2);
            i += chunkLength - 6;
            return h;
        }
        private void WriteMIDIFile(string loc)
        {
            StreamWriter sw = new StreamWriter(loc);
            WriteHeader(sw);
            for (int i = 0; i < trAmount; i++)
                tr[i].WriteTrack(sw);
        }
        private void WriteHeader(StreamWriter sw)
        {
            //Header
            sw.Write('M');
            sw.Write('T');
            sw.Write('h');
            sw.Write('d');
            //Length
            sw.Write(0);
            sw.Write(0);
            sw.Write(0);
            sw.Write(6);
            //Formats
            sw.Write(0);
            sw.Write(1);
            //Tracks
            byte calc = (byte)(trAmount / 255);
            sw.Write(calc);
            sw.Write(trAmount - calc * 255);
            //Ticks
            calc = (byte)(ticks / 255);
            sw.Write(calc);
            sw.Write(ticks - calc * 255);
        }
    }
}