using System;
using System.Collections.Generic;
using System.IO;
/*private MIDIEvent ReadMIDIEvent(int dt, byte[] file, ref int i)
{
int channel = file[i] - 144;
byte ch;
if (channel > 16)
{
int j = e.Count;
while (e[j].eventType != 9)
j--;
ch = (e[j] as MIDIEvent).channel;
}
else
ch = (byte)channel;
i++;
byte key = file[i];
i++;
byte velocity = file[i];
i++;
return new MIDIEvent(ch, velocity, key, dt);
}

private SysexEvent ReadSysExEvent(int dt, byte[] file, ref int i)
{
byte t = file[i];
i++;
int length = ReadDeltaTimeChunk(file, ref i);
byte[] p = new byte[length];
for (int j = 0; j < length; j++)
{
p[j] = file[i];
i++;
}
return new SysexEvent(t, p, dt);
}

private MetaEvent ReadMetaEvent(int dt, byte[] file, ref int i)
{
i++;
byte type = file[i];
i++;
int length = ReadDeltaTimeChunk(file, ref i);
byte[] p = new byte[length];
for (int j = 0; j < length; j++)
{
p[j] = file[i];
i++;
}
return new MetaEvent(type, p, dt);
}
private Event ReadEvent(byte[] file, ref int i)
{
int dt = ReadDeltaTimeChunk(file, ref i);
if (file[i] == 255)
return ReadMetaEvent(dt, file, ref i);
else if (file[i] == 240 || file[i] == 247)
return ReadSysExEvent(dt, file, ref i);
return ReadMIDIEvent(dt, file, ref i);
}*/
namespace SOS
{
    public class Track
    {
        private List<Event> e;
        private int count, eventNum;
        internal Track()
        {
            e = new List<Event>();
            e.Add(new MetaEvent(16, 47, null));
        }
        internal Track(byte[] file, ref int i)
        {
            e = new List<Event>();
            if (!(file[i] == 77 && file[i + 1] == 84 && file[i + 2] == 114 && file[i + 3] == 107))
                throw new System.ArgumentException("INVALID FILE", "file");
            i += 4;
            int length = ReadNChunks(file, ref i, 4);
            int diff = i;
            while (i - diff < length)
            {
                int dt = ReadDeltaTimeChunk(file, ref i);

                if (file[i] == 255)
                    e.Add(new MetaEvent(dt, file, ref i));

                else if (file[i] == 240 || file[i] == 247)
                    e.Add(new SysexEvent(dt, file, ref i));

                if (e.Count != 0)
                    e.Add(new MIDIEvent(dt, file, e[e.Count], ref i));
                else
                    e.Add(new MIDIEvent(dt, file, new Event(0, 0), ref i));
            }
        }
        private int ReadChunk(byte[] file, ref int i, int val)
        {
            val = val * 10 + file[i];
            i++;
            return val;
        }
        private int ReadDeltaTimeChunk(byte[] file, ref int i)
        {
            int val = 0;
            while (file[i] > 127)
                val = ReadChunk(file, ref i, val);
            return val;
        }
        private int ReadNChunks(byte[] file, ref int i, int length)
        {
            int val = 0;
            for (int j = 0; j < length; j++)
                val = ReadChunk(file, ref i, val);
            return val;
        }
        //Convert to constructor and iron out the GUI
        public void ImportPattern(byte[,] a, int n)
        {
            e.Clear();
            byte[] lastVal = new byte[129];
            for (int i = 0; i < 129; i++)
                lastVal[i] = 0;
            int sixteenthsSinceLastMessage = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = a.GetLength(0) - 1; j >= 0; j--)
                {
                    if (lastVal[j] != a[j, i])
                    {
                        //YES I'M SORRY IT'S HARD-CODED
                        if (j >= 128)
                            e.Add(new MIDIEvent(sixteenthsSinceLastMessage, a[j, i], a[j, i]));
                        else
                            e.Add(new MIDIEvent(sixteenthsSinceLastMessage, a[128, i], a[j, i], (byte)j));
                        lastVal[j] = a[j, i];
                        sixteenthsSinceLastMessage = 0;
                    }
                }
                sixteenthsSinceLastMessage++;
            }
            e.Add(new Event(sixteenthsSinceLastMessage, 255));
        }
        private int lengthToPoint(int n)
        {
            int p = 0;
            for (int i = 0; i < n; i++)
                p += e[i].deltaTime;
            return p;
        }
        public byte[,] ExportPattern()
        {
            int count = 0;
            int n = lengthToPoint(e.Count);
            byte[,] nizSablon = new byte[129, n];
            for (int i = 0; i < 129; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == 0)
                        nizSablon[i, j] = 0;
                    else
                        nizSablon[i, j] = 255;
                }
            }
            for (int i = 0; i < e.Count; i++)
            {
                count += e[i].deltaTime;
                if (e[i].eventType == 9)
                    nizSablon[(e[i] as MIDIEvent).note, count] = (e[i] as MIDIEvent).velocity;
                else if (e[i].eventType == 12)
                    nizSablon[128, count] = (byte)((e[i] as MIDIEvent).velocity);
            }
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < 129; j++)
                {
                    if (nizSablon[j, i] == 255)
                        nizSablon[j, i] = nizSablon[j, i - 1];
                }
            }
            return nizSablon;
        }
        internal List<Event> Read()
        {
            List<Event> t = new List<Event>();
            while (count == e[eventNum].deltaTime)
            {
                t.Add(e[eventNum]);
                eventNum++;
                count = 0;
            }
            count++;
            return t;
        }

        internal void WriteTrack(StreamWriter sw)
        {
            sw.Write('M');
            sw.Write("T");
            sw.Write('r');
            sw.Write('k');
            WriteLength(sw);
            for (int i = 0; i < e.Count; i++)
            {
                int n = 0;
                byte[] write;
                if (i != 0)
                    write = e[i].WriteEvent(e[i - 1], ref n);
                else
                    write = e[i].WriteEvent(null, ref n);
                for (int j = 0; j < write.Length; j++)
                    sw.Write(write[j]);
            }
        }

        private void WriteLength(StreamWriter sw)
        {
            int length = 0;
            for (int i = 0; i < e.Count; i++)
            {
                if (i != 0)
                    length += e[i].CalculateChunks(e[i - 1]);
                else
                    length += e[i].CalculateChunks(null);
            }
            for (int i = 4; i >= 0; i--)
            {
                int p = length / (int)Math.Pow(255, i);
                sw.Write(p);
                length = length - p * (int)Math.Pow(255, i);
            }
        }
    }
}
