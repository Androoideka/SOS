using System.Collections.Generic;

namespace SOS
{
    public class Track
    {
        internal List<Event> e = new List<Event>();
        public void ImportPattern(byte[,] a, int n)
        {
            e.Clear();
            byte[] lastVal = new byte[129];
            for (int i = 0; i < 129; i++)
                lastVal[i] = 0;
            byte sixteenthsSinceLastMessage = 0;
            for (int i = 0; i < n; i++)
            {
                for (byte j = 0; j < 129; j++)
                {
                    if (lastVal[j] != a[j, i])
                    {
                        if (j == 128)
                            e.Add(new MetaEvent(a[j, i], sixteenthsSinceLastMessage));
                        else
                            e.Add(new MIDIEvent(a[j, i], j, sixteenthsSinceLastMessage));
                        lastVal[j] = a[j, i];
                        sixteenthsSinceLastMessage = 0;
                    }
                }
                sixteenthsSinceLastMessage++;
            }
            e.Add(new MIDIEvent(0, 255, sixteenthsSinceLastMessage));
        }
        private int lengthOfTrack()
        {
            int p = 0;
            for (int i = 0; i < e.Count; i++)
                p += e[i].getDT(1);
            return p;
        }
        public byte[,] ExportPattern()
        {
            int count = 0;
            int n = lengthOfTrack();
            byte[,] nizSablon = new byte[129,n];
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
                if (e[i].eventType == 255)
                    nizSablon[128, count] = (e[i] as MetaEvent).patch;
                else if((e[i] as MIDIEvent).note != 255)
                    nizSablon[(e[i] as MIDIEvent).note, count] = (e[i] as MIDIEvent).velocity;
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
    }
}
