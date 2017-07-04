using System.Collections.Generic;

namespace SOS
{
    public class Track
    {
        private List<Event> e;
        private int count, eventNum;
        private float[] vel;
        private string[] cas;
        internal bool ended;
        public Track()
        {
            e = new List<Event>();
            vel = new float[128];
            cas = new string[128];
        }
        internal void ImportPattern(byte[,] a, int n)
        {
            e.Clear();
            byte[] lastVal = new byte[129];
            for (int i = 0; i < 129; i++)
                lastVal[i] = 0;
            byte sixteenthsSinceLastMessage = 0;
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
            e.Add(new PCEvent(255, sixteenthsSinceLastMessage));
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
                else if ((e[i] as PCEvent).patch != 255)
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
            ended = false;
            count = 0;
            eventNum = 0;
        }
        private void PlaySound()
        {
            for (int i = 0; i < 128; i++)
            {
                if(vel[i] != 0)
                    AudioPlaybackEngine.Instance.PlaySound(cas[i], vel[i]);
            }
        }
        public void Play()
        {
            while (!(e[eventNum].eventType == 255 && (e[eventNum] as PCEvent).patch == 255) && count == e[eventNum].getDT(1))
            {
                //IS CORRECT BUT NEEDS TO BE OPTIMISED! TIMER IS MULTITHREADED
                if (e[eventNum].eventType == 0)
                    vel[(e[eventNum] as MIDIEvent).note] = (e[eventNum] as MIDIEvent).GetVolume();
                else
                    Load((e[eventNum] as PCEvent).patch);
                eventNum++;
                count = 0;
            }
            PlaySound();
            if (e[eventNum].eventType == 255 && (e[eventNum] as PCEvent).patch == 255 && e[eventNum].getDT(1) == count)
                ended = true;
            count++;
        }
        private void Load(int i)
        {
            for (int j = 0; j < 128; j++)
                cas[j] = Projekt.sb[i].note[j];
        }
    }
}

