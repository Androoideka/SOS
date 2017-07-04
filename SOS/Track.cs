using System.Collections.Generic;

namespace SOS
{
    public class Track
    {
        public struct MIDIMessage
        {
            private byte deltaTime;
            private byte velocity;
            public readonly byte note;
            public MIDIMessage(byte vel, byte no, byte deltatime)
            {
                deltaTime = deltatime;
                velocity = vel;
                note = no;
            }
            public double getVolume()
            {
                return 1d / 127d * velocity;
            }
            public byte getVelocity()
            {
                return velocity;
            }
            public int getDT(int interval)
            {
                return deltaTime * interval;
            }
        }
        public List<MIDIMessage> e = new List<MIDIMessage>();
        public Soundbank instrument;
        public Track(Soundbank inst)
        {
            instrument = inst;
        }
        public void ImportPattern(byte[,] a, int n)
        {
            e.Clear();
            byte[] lastVal = new byte[instrument.note.Length];
            for (int i = 0; i < instrument.note.Length; i++)
                lastVal[i] = 0;
            byte sixteenthsSinceLastMessage = 0;
            for (int i = 0; i < n; i++)
            {
                for (byte j = 0; j < instrument.note.Length; j++)
                {
                    if (lastVal[j] != a[j, i])
                    {
                        e.Add(new MIDIMessage(a[j, i], j, sixteenthsSinceLastMessage));
                        lastVal[j] = a[j, i];
                        sixteenthsSinceLastMessage = 0;
                    }
                }
                sixteenthsSinceLastMessage++;
            }
            e.Add(new MIDIMessage(0, 255, sixteenthsSinceLastMessage));
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
            byte[,] nizSablon = new byte[instrument.note.Length,n];
            for (int i = 0; i < instrument.note.Length; i++)
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
                if(e[i].note != 255)
                    nizSablon[e[i].note, count] = e[i].getVelocity();
            }
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < instrument.note.Length; j++)
                {
                    if (nizSablon[j, i] == 255)
                        nizSablon[j, i] = nizSablon[j, i - 1];
                }
            }
            return nizSablon;
        }
    }
}
