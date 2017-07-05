using System.Collections.Generic;

namespace SOS
{
    public class Track
    {
        private List<Event> e;
        private int count, eventNum;
        private float[] vel;
        private string[] cas;
        public Track()
        {
            e = new List<Event>();
            vel = new float[128];
        }
        internal void ImportPattern(byte[,] a, int n)
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
                            e.Add(new PCEvent(a[j, i], sixteenthsSinceLastMessage));
                        else
                            e.Add(new MIDIEvent(a[j, i], (byte)j, sixteenthsSinceLastMessage));
                        lastVal[j] = a[j, i];
                        sixteenthsSinceLastMessage = 0;
                    }
                }
                sixteenthsSinceLastMessage++;
            }
            e.Add(new Event(255, sixteenthsSinceLastMessage));
        }
        private int lengthToPoint(int n)
        {
            int p = 0;
            for (int i = 0; i < n; i++)
                p += e[i].getDT(1);
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
                count += e[i].getDT(1);
                if (e[i].eventType == 0)
                    nizSablon[(e[i] as MIDIEvent).note, count] = (e[i] as MIDIEvent).velocity;
                else if (e[i].eventType == 1)
                    nizSablon[128, count] = (byte)((e[i] as PCEvent).patch);
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
        public void ResetTrackPosition()
        {
            for (int i = 0; i < 128; i++)
                vel[i] = 0;
            Load(0);
            count = 0;
            eventNum = 0;
            Play();
        }
        public bool Play()
        {
            while (count == e[eventNum].getDT(1))
            {
                if (e[eventNum].eventType == 255)
                    return true;
                else
                {
                    if (e[eventNum].eventType == 0)
                        vel[(e[eventNum] as MIDIEvent).note] = (e[eventNum] as MIDIEvent).GetVolume();
                    else if (e[eventNum].eventType == 1)
                        Load((e[eventNum] as PCEvent).patch);
                }
                eventNum++;
                count = 0;
            }
            count++;
            for (int i = 0; i < 128; i++)
            {
                if (vel[i] != 0)
                    AudioPlaybackEngine.Instance.PlaySound(cas[i], vel[i]);
            }
            return false;
        }
        private void Load(int i)
        {
            cas = Projekt.sb[i].note;
        }
    }
}
